using System.Collections.Generic;
using Damas.Core.Serialization;
using NUnit.Framework;

namespace Damas.Test.Serialization
{
    public class JsonSerializerExtensionTest
    {
        private class Test
        {
            public bool Boolean { get; set; }
            public string String { get; set; }
            public IList<int> Array { get; set; }

            public Test(bool boolean, string @string, IList<int> array)
            {
                Boolean = boolean;
                String = @string;
                Array = array;
            }
        }

        [Test]
        public void TestSerializeShouldTransformObjectIntoJsonString()
        {
            var obj = new Test(boolean: true, @string: "string", array: new List<int>() { 1, 2, 3, 4, 5 });

            var json = obj.Serialize();

            Assert.AreEqual("{\"boolean\":true,\"string\":\"string\",\"array\":[1,2,3,4,5]}", json);
        }

        [Test]
        public void TestDeserializeShouldTransformJsonStringIntoTypedObject()
        {
            var json = "{\"boolean\":true,\"string\":\"string\",\"array\":[1,2,3,4,5]}";

            var obj = json.Deserialize<Test>();

            var expect = new Test(boolean: true, @string: "string", array: new List<int>() { 1, 2, 3, 4, 5 });

            Assert.AreEqual(expect.Boolean, obj!.Boolean);
            Assert.AreEqual(expect.String, obj!.String);
            Assert.AreEqual(expect.Array, obj!.Array);
        }

        [Test]
        public void TestDeserializeShouldTransformJsonStringIntoObject()
        {
            var json = "{\"boolean\":true,\"string\":\"string\",\"array\":[1,2,3,4,5]}";

            var obj = json.Deserialize();

            var expect = new Test(boolean: true, @string: "string", array: new List<int>() { 1, 2, 3, 4, 5 });

            Assert.AreEqual(expect.Serialize(), obj!.Serialize());
        }
    }
}