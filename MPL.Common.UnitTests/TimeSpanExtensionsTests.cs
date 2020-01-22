using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPL.Common.TestHelpers;
using System;

namespace MPL.Common
{
    [TestClass]
    public class TimeSpanExtensionsTests
    {
        [TestMethod]
        public void TimeSpanExtensions_ToTextualTimestamp_Exists()
        {
            Assert.IsTrue(ExtensionMethodTestHelper.ContainsNamedExtensionMethod(typeof(TimeSpan), typeof(TimeSpanExtensions), nameof(TimeSpanExtensions.ToTextualTimestamp)));
            Assert.IsTrue(ExtensionMethodTestHelper.ContainsNamedExtensionMethod(typeof(TimeSpan), typeof(TimeSpanExtensions), nameof(TimeSpanExtensions.ToTextualTimestamp), new[] { typeof(bool), typeof(bool) }));
        }

        [TestMethod]
        public void ToTextualTimestamp_DefaultDHMSM_OutputCorrect()
        {
            TimeSpan timeSpan = new TimeSpan(20, 19, 18, 17, 16);
            Assert.AreEqual("20 days, 19 hours, 18 minutes and 17 seconds", timeSpan.ToTextualTimestamp());
        }

        [TestMethod]
        public void ToTextualTimestamp_DefaulDHMSMtWithAllZeroes_OutputCorrect()
        {
            TimeSpan timeSpan = new TimeSpan(0, 0, 0, 0, 0);
            Assert.AreEqual(string.Empty, timeSpan.ToTextualTimestamp());
        }

        [TestMethod]
        public void ToTextualTimestamp_DefaultDHMSMWithOnes_OutputCorrect()
        {
            TimeSpan timeSpan = new TimeSpan(1, 1, 1, 1, 1);
            Assert.AreEqual("1 day, 1 hour, 1 minute and 1 second", timeSpan.ToTextualTimestamp());
        }

        [TestMethod]
        public void ToTextualTimestamp_DefaultDHMSMWithZeroes_OutputCorrect()
        {
            TimeSpan timeSpan = new TimeSpan(1, 0, 1, 0, 1);
            Assert.AreEqual("1 day and 1 minute", timeSpan.ToTextualTimestamp());
        }

        [TestMethod]
        public void ToTextualTimestamp_WithMillisecondsDHMSM_OutputCorrect()
        {
            TimeSpan timeSpan = new TimeSpan(20, 19, 18, 17, 16);
            Assert.AreEqual("20 days, 19 hours, 18 minutes, 17 seconds and 16 milliseconds", timeSpan.ToTextualTimestamp(true, false));
        }

        [TestMethod]
        public void ToTextualTimestamp_WithMillisecondsDHMSMWithAllZeroes_OutputCorrect()
        {
            TimeSpan timeSpan = new TimeSpan(0, 0, 0, 0, 0);
            Assert.AreEqual(string.Empty, timeSpan.ToTextualTimestamp(true, false));
        }

        [TestMethod]
        public void ToTextualTimestamp_WithMillisecondsDHMSMWithOnes_OutputCorrect()
        {
            TimeSpan timeSpan = new TimeSpan(1, 1, 1, 1, 1);
            Assert.AreEqual("1 day, 1 hour, 1 minute, 1 second and 1 millisecond", timeSpan.ToTextualTimestamp(true, false));
        }

        [TestMethod]
        public void ToTextualTimestamp_WithMillisecondsDHMSMWithZeroes_OutputCorrect()
        {
            TimeSpan timeSpan = new TimeSpan(1, 0, 1, 0, 1);
            Assert.AreEqual("1 day, 1 minute and 1 millisecond", timeSpan.ToTextualTimestamp(true, false));
        }

        [TestMethod]
        public void ToTextualTimestamp_WithZeroesDHMSM_OutputCorrect()
        {
            TimeSpan timeSpan = new TimeSpan(20, 0, 18, 0, 16);
            Assert.AreEqual("20 days, 0 hours, 18 minutes, 0 seconds and 16 milliseconds", timeSpan.ToTextualTimestamp(true, true));
        }

        [TestMethod]
        public void ToTextualTimestamp_WithZeroesDHMSMtWithAllZeroes_OutputCorrect()
        {
            TimeSpan timeSpan = new TimeSpan(0, 0, 0, 0, 0);
            Assert.AreEqual("0 days, 0 hours, 0 minutes, 0 seconds and 0 milliseconds", timeSpan.ToTextualTimestamp(true, true));
        }

        [TestMethod]
        public void ToTextualTimestamp_WithZeroesDHMSMWithZeroes_OutputCorrect()
        {
            TimeSpan timeSpan = new TimeSpan(1, 0, 1, 0, 1);
            Assert.AreEqual("1 day, 0 hours, 1 minute, 0 seconds and 1 millisecond", timeSpan.ToTextualTimestamp(true, true));
        }
    }
}