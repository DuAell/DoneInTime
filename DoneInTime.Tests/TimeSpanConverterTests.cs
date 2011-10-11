using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace DoneInTime.Tests
{
    [TestClass]
    public class TimeSpanConverterTests
    {
        [TestMethod]
        public void TimeSpanConverterTests_WhenConverting_VerifyValueIsOk()
        {
            #region Actors

            var ts = new TimeSpan(1, 1, 20, 10);

            #endregion

            #region Activities

            var convertedValue = new DoneInTime.Converters.TimeSpanConverter().Convert(ts, typeof(string), null, null);

            #endregion

            #region Asserts

            Assert.AreEqual("25:20:10", convertedValue, "Convert not correct");

            #endregion

        }

        [TestMethod]
        public void TimeSpanConverterTests_WhenConvertingBack_VerifyValueIsOk()
        {
            #region Actors

            var ts = new TimeSpan(1, 1, 20, 10);
            string valueToConvert = "25:20:10";

            #endregion

            #region Activities

            var convertedValue = new DoneInTime.Converters.TimeSpanConverter().ConvertBack(valueToConvert, typeof(TimeSpan), null, null);

            #endregion

            #region Asserts

            Assert.AreEqual(ts, convertedValue, "Convert not correct");

            #endregion

        }
    }
}
