using System;
using System.Linq;
using DoneInTime.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace DoneInTime.Tests
{
    [TestClass]
    public class TimeSpanExtensionsTests
    {
        [TestMethod]
        public void TimeSpanExtensionsTests_WhenConvertingBack_VerifyValueIsOk()
        {
            #region Actors

            var ts = new TimeSpan(1, 1, 20, 10);
            string valueToConvert = "25:20:10";

            #endregion

            #region Activities

            var convertedValue = valueToConvert.ToTimeSpan();

            #endregion

            #region Asserts

            Assert.AreEqual(ts, convertedValue, "Convert not correct");

            #endregion

        }
    }
}
