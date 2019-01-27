using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace MinCut.Test
{
    public static class TestCaseFactory
    {
        private const string _testDirectory = "testCases";

        public static IEnumerable<TestCaseData> TestCases
        {
            get
            {
                Dictionary<string, (string input, string output)> dict = new Dictionary<string, (string input, string output)>();
                IEnumerable<string> files = Directory.EnumerateFiles(_testDirectory);
                foreach (string entry in files)
                {
                    string key = entry.Substring(entry.IndexOf("random"));
                    if (dict.ContainsKey(key))
                    {
                        dict[key] = (dict[key].input, entry);
                    }
                    else
                    {
                        dict.Add(key, (entry, null));
                    }
                }

                foreach ((string input, string output) value in dict.Values)
                {
                    yield return new TestCaseData(value.input, value.output);
                }
            }
        }
    }
}