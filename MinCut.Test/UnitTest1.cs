using MinCut.ConsoleApp;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace MinCut.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CanLoadTestCases()
        {
            foreach (var entry in TestCaseFactory.TestCases)
            {
                Assert.AreEqual(2, entry.OriginalArguments.Length);
                Assert.NotNull(entry.OriginalArguments[0]);
                Assert.NotNull(entry.OriginalArguments[1]);
            }
        }

        [Test, TestCaseSource(typeof(TestCaseFactory), "TestCases")]
        public void CanLoadGraphs(string inputFile, string outputFile)
        {
            var graph = Program.ParseGraph(inputFile);
            var thing = File.ReadAllLines(inputFile);
            Assert.AreEqual(thing.Length, graph.Count);
        }

        [Test, TestCaseSource(typeof(TestCaseFactory), "TestCases")]
        public void CanRunContraction(string inputFile, string outputFile)
        {
            var graph = Program.ParseGraph(inputFile);
            var cut = Program.CalculateRandomCut(graph);
        }

        [Test, TestCaseSource(typeof(TestCaseFactory), "TestCases")]
        public void CorrectMinCuts(string inputFile, string outputFile)
        {
            var minCut = Program.CalculateMinCut(inputFile);
            var expected = int.Parse(File.ReadLines(outputFile).First());
            Assert.AreEqual(expected, minCut);
        }
    }
}