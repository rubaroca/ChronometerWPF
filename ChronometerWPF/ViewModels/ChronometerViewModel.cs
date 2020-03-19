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

        #endregion

        #region Properties

        public DelegateCommand StartTimer { get; private set; }
        public DelegateCommand PauseTimer { get; private set; }
        public DelegateCommand StopTimer { get; private set; }

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
            var dispatcherTimer = new DispatcherTimer(interval, DispatcherPriority.Background, UpdateTime, Dispatcher.CurrentDispatcher);

            StartTimer = new DelegateCommand(x => StartTimeCounter(), x => !stopwatch.IsRunning);
            PauseTimer = new DelegateCommand(x => PauseTimeCounter(), x => stopwatch.IsRunning);
            StopTimer = new DelegateCommand(x => StopTimeCounter(), x => stopwatch.Elapsed.TotalSeconds > 0);
        }

        #endregion       

        #region Private methods

        private void UpdateTime(object sender, EventArgs e)
        {
            Time = stopwatch.Elapsed;
        }

        private void StartTimeCounter()
        {
            stopwatch.Start();
            StartTimer.InvokeCanExecuteChanged();
        }

        private void PauseTimeCounter()
        {
            stopwatch.Stop();
            PauseTimer.InvokeCanExecuteChanged();
        }

        private void StopTimeCounter()
        {
            stopwatch.Restart();
            stopwatch.Stop();
            StopTimer.InvokeCanExecuteChanged();
        }

        private void RaiseCanExecutes()
        {
            StartTimer.InvokeCanExecuteChanged();
            PauseTimer.InvokeCanExecuteChanged(); 
            StopTimer.InvokeCanExecuteChanged();
        }

        #endregion
    }
}
