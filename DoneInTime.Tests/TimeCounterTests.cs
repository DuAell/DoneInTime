using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DoneInTime.Model;
using System.IO;

namespace DoneInTime.Tests
{
    [TestClass]
    public class TimeCounterTests
    {
        [TestMethod]
        public void WhenLoadXmlFile_VerifyTasksAreCorrect()
        {
            #region Actors
            TimeCounter tc;
            #endregion

            #region Activities
            tc = new TimeCounter(@"..\..\..\DoneInTime.Tests\Resources\XmlTestFile.xml");
            #endregion

            #region Asserts
            Assert.AreEqual(2, tc.Tasks.Count, "Wrong Number Of Tasks");
            Assert.AreEqual("Task 1", tc.Tasks[0].Name, "Wrong name for first task");
            Assert.AreEqual(new TimeSpan(0, 4, 47), tc.Tasks[0].TimeCount, "Wrong timecount for first task");
            Assert.AreEqual(true, tc.Tasks[0].IsRunning, "Wrong isRunning state");
            #endregion
        }

        [TestMethod]
        public void WhenSaveXmlFile_VerifyFileIsCorrect()
        {
            #region Actors
            string expectedString = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<?xml-stylesheet type=\"text/xsl\" href=\"TasksStyleSheet.xsl\"?>\r\n<Tasks>\r\n  <Task>\r\n    <Name>Test Save</Name>\r\n    <TimeCount>00:00:00</TimeCount>\r\n    <IsRunning>False</IsRunning>\r\n  </Task>\r\n</Tasks>";
            string actualString;
            TimeCounter tc = new TimeCounter("XmlSaveTestFile.xml");
            tc.Tasks.Add(new Task("Test Save", new TimeCounter("")));
            #endregion

            #region Activities
            tc.SaveToXML();
            StreamReader t = new StreamReader("XmlSaveTestFile.xml");
            actualString = t.ReadToEnd();
            #endregion

            #region Asserts
            Assert.AreEqual(expectedString, actualString, "Xml is wrong");
            #endregion
        }

        [TestMethod]
        public void WhenGetActiveTaskDescriptionWithOneRunningTask_VerifyItIsCorrect()
        {
            #region Actors

            TimeCounter tc = new TimeCounter(@"..\..\..\DoneInTime.Tests\Resources\XmlTestFile.xml");

            #endregion

            #region Asserts

            Assert.AreEqual("Task 1 : 00:04:47", tc.ActiveTaskDescription, "Wrong active task description");

            #endregion
        }

        [TestMethod]
        public void WhenGetActiveTaskDescriptionWithNoRunningTask_VerifyItIsCorrect()
        {
            #region Actors

            TimeCounter tc = new TimeCounter(@"..\..\..\DoneInTime.Tests\Resources\XmlTestFile.xml");
            tc.Tasks[0].Stop();

            #endregion

            #region Asserts

            Assert.AreEqual("DoneInTime", tc.ActiveTaskDescription, "Wrong active task description");

            #endregion
        }
    }
}
