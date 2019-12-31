using System;
using System.Runtime.Serialization;

namespace MPL.Common.UnitTests.Runtime.Serialization
{
    [DataContract()]
    public class SerialiserTestDummyClassUnnamed
    {
        public static string JsonOutput = "{\"TestA\":\"test value a\",\"TestB\":17}";

        [DataMember()]
        public string TestA { get; set; }

        [DataMember()]
        public int TestB { get; set; }
    }

    [DataContract()]
    public class SerialiserTestDummyClassNamed
    {
        public static string JsonOutput = "{\"test_a\":\"test value a\",\"test_b\":17}";

        [DataMember(Name = "test_a")]
        public string TestA { get; set; }

        [DataMember(Name = "test_b")]
        public int TestB { get; set; }
    }
}