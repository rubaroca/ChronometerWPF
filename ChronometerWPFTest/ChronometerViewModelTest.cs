using ChronometerWPF.Models;
using ChronometerWPF.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace ChronometerWPFTest
{
    [TestClass]
    public class ChronometerViewModelTest
    {
        #region Tests

        [TestMethod]
        public void If_InitializeChrono_Then_TimeIsZero()
        {
            //ARRANGE
            var expectedTime = new TimeSpan();
            var chronometer = new Chronometer();

            //ACT
            var sut = new ChronometerViewModel(chronometer);

            //ASSERT
            Assert.AreEqual(expectedTime, sut.Time);
        }

        [TestMethod]
        public void If_StartChronoWait3segAndPause_Then_TimeIncreases3g()
        {
            //ARRANGE
            var expectedTime = new TimeSpan(0, 0, 3);
            var waitingTime = expectedTime;
            var chronometerViewModel = new ChronometerViewModel(new Chronometer());

            //ACT
            chronometerViewModel.StartTimer.Execute(null);
            Thread.Sleep(waitingTime);
            chronometerViewModel.PauseTimer.Execute(null);
            UpdateTime(chronometerViewModel);

            //ASSERT
            var actualTime = new TimeSpan(chronometerViewModel.Time.Hours, chronometerViewModel.Time.Minutes, chronometerViewModel.Time.Seconds);
            Assert.AreEqual(expectedTime, actualTime);
        }

        [TestMethod]
        public void If_StartChronoAndStopAfter2seg_Then_TimeIsZero()
        {
            //ARRANGE
            var expectedTime = new TimeSpan(0, 0, 0);
            var waitingTime = new TimeSpan(0, 0, 2);
            var chronometerViewModel = new ChronometerViewModel(new Chronometer());

            //ACT
            chronometerViewModel.StartTimer.Execute(null);
            Thread.Sleep(waitingTime);
            chronometerViewModel.StopTimer.Execute(null);
            UpdateTime(chronometerViewModel);

            //ASSERT
            var actualTime = new TimeSpan(chronometerViewModel.Time.Hours, chronometerViewModel.Time.Minutes, chronometerViewModel.Time.Seconds);
            Assert.AreEqual(expectedTime, actualTime);
        }

        #endregion

        #region Private methods

        private void UpdateTime(ChronometerViewModel chronometerViewModel)
        {
            PrivateObject obj = new PrivateObject(chronometerViewModel);
            obj.Invoke("UpdateTime", new object [] { null, null });
        }

        #endregion
    }
}
