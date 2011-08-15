using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DoneInTime.Model;

namespace DoneInTime.Tests
{
    [TestClass]
    public class TasksTests
    {
        //[TestMethod]
        //public void WhenTaskIsRun2Seconds_VerifyTimeCountEquals2Seconds()
        //{
        //    #region Actors
        //    Task task = new Task("test");
        //    #endregion

        //    #region Activities
        //    task.Start();
        //    //Stopwatch sw = new Stopwatch();
        //    //sw.Start();
        //    //while (sw.Elapsed < new TimeSpan(0, 0, 2)) {};
        //    //sw.Stop();

        //    System.Threading.Thread.Sleep(2000);
        //    task.Stop();
        //    #endregion

        //    #region Asserts
        //    Assert.AreEqual(new TimeSpan(0, 0, 2), task.TimeCount);
        //    #endregion
        //}

        [TestMethod]
        public void WhenTaskIsNotRun_VerifyTimeCountEquals0Seconds()
        {
            #region Actors
            Task task = new Task("test", new TimeCounter(""));
            #endregion

            #region Activities
            #endregion

            #region Asserts
            Assert.AreEqual(new TimeSpan(0, 0, 0), task.TimeCount);
            #endregion
        }
    }
}
