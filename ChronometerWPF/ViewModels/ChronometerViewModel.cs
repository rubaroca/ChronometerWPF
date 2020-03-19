using ChronometerWPF.Commands;
using ChronometerWPF.Models;
using System;
using System.Diagnostics;
using System.Windows.Threading;

namespace ChronometerWPF.ViewModels
{
    public class ChronometerViewModel : BaseViewModel
    {
        #region Fields

        private readonly Stopwatch stopwatch;

        private readonly DelegateCommand startTimer;
        private readonly DelegateCommand pauseTimer;
        private readonly DelegateCommand stopTimer;

        #endregion

        #region Properties

        public Chronometer Chronometer { get; set; }
    
        public TimeSpan Time
        {
            get
            {
                return Chronometer.Time;
            }
            set
            {
                if (Chronometer.Time != value)
                {
                    Chronometer.Time = value;
                    OnPropertyChanged();
                    RaiseCanExecutes();
                }
            }
        }

        #endregion

        #region Contructor

        public ChronometerViewModel(Chronometer chronometer)
        {
            Chronometer = chronometer;

            stopwatch = new Stopwatch();

            var interval = new TimeSpan(0, 0, 0, 1);
            var dispatcherTimer = new DispatcherTimer(interval, DispatcherPriority.Background, UpdateTotalElapsedTime, Dispatcher.CurrentDispatcher);

            startTimer = new DelegateCommand(x => StartTimeCounter(), x => !stopwatch.IsRunning);
            pauseTimer = new DelegateCommand(x => PauseTimeCounter(), x => stopwatch.IsRunning);
            stopTimer = new DelegateCommand(x => StopTimeCounter(), x => stopwatch.Elapsed.TotalSeconds > 0);
        }

        #endregion       

        #region Private methods

        private void UpdateTotalElapsedTime(object sender, EventArgs e)
        {
            Time = stopwatch.Elapsed;
        }

        private void StartTimeCounter()
        {
            stopwatch.Start();
            startTimer.CanExecute(null);
        }

        private void PauseTimeCounter()
        {
            stopwatch.Stop();
            pauseTimer.CanExecute(null);
        }

        private void StopTimeCounter()
        {
            stopwatch.Restart();
            stopwatch.Stop();
            stopTimer.CanExecute(null);
        }

        private void RaiseCanExecutes()
        {
            startTimer.CanExecute(null);
            pauseTimer.CanExecute(null); 
            stopTimer.CanExecute(null);
        }

        #endregion
    }
}
