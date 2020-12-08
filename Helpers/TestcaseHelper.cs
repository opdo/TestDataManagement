using System;
using System.Collections.Generic;
using System.IO;
using TestDataManagement.Models;

namespace TestDataManagement.Helpers
{
    public class TestcaseHelper
    {
        public static List<TestSuite> LoadTestFile(string filepath)
        {
            if (!File.Exists(filepath)) return null;
            // Read content
            var content = File.ReadAllText(filepath);
            // Try read test suite
            var list = new List<TestSuite>();
                
            try
            {
                // New format
                list = FileHelper.ReadJson<List<TestSuite>>(content);
            }
            catch
            {
                // Old Format
                var testsuite = FileHelper.ReadJson<TestSuite>(content);
                list.Add(testsuite);
            }

            return list;
        }
    }
}
