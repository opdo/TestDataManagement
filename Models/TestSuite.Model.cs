using System.Collections.Generic;

namespace TestDataManagement.Models
{
    public partial class TestSuite
    {
        public string Env { get; set; }
        public string Vendor { get; set; }
        public string Type { get; set; }
        public List<Testchild> testsuite { get; set; }

        public TestSuite()
        {
            testsuite = new List<Testchild>();
        }
    }

    public class Testchild
    {
        public List<Testcase> testcases { get; set; }

        public Testchild()
        {
            testcases = new List<Testcase>();
        }
    }
}
