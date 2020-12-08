using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDataManagement.Models
{
    public partial class TestSuite
    {
        public List<string> GetListMethod()
        {
            var list = new List<string>();
            try
            {
                list = testsuite
                    .SelectMany(x => x.testcases)
                    .Select(x => x.Action)
                    .ToList();
            }
            catch
            {

            }
            return list;
        }

        public bool IsExistMethod(string method)
        {
            try
            {
                var listMethod = GetListMethod();
                return listMethod.Any(x => x.ToLower().Equals(method.ToLower()));
            }
            catch
            {
                return false;
            }
        }

        public void AddTestcase(Testcase testcase)
        {
            VerifyTestcaseData(testcase);
            if (IsExistMethod(testcase.Action)) throw new Exception($"Action '{testcase.Action}' already exist");

            if (testsuite is null)
            {
                testsuite = new List<Testchild>();
            }

            if (testsuite.Count < 1)
            {
                testsuite.Add(new Testchild());
            }

            if (testsuite[0].testcases is null)
            {
                testsuite[0].testcases = new List<Testcase>();
            }

            testsuite[0].testcases.Add(testcase);
        }

        public void RemoveTestcase(Testcase testcase)
        {
            if (!IsExistMethod(testcase.Action)) throw new Exception($"Action '{testcase.Action}' doesnt exist");


            testsuite.ForEach((x) =>
            {
                x.testcases.ForEach((y) =>
                {
                    if (y.Action.Equals(testcase.Action))
                    {
                        // Remove and break
                        x.testcases.Remove(y);
                        return;
                    }
                });
            });
        }

        public void EditTestcase(Testcase newObj, Testcase oldObj)
        {
            testsuite.ForEach((x) =>
            {
                x.testcases.ForEach((y) =>
                {
                    if (y.Action.Equals(oldObj.Action))
                    {
                        // Assgine new data
                        y = newObj;
                    }
                });
            });
        }

        private void VerifyTestcaseData(Testcase testcase)
        {
            if (string.IsNullOrEmpty(testcase.Action)) throw new Exception("Please input method name");
            if (string.IsNullOrEmpty(testcase.ExecFlag)) testcase.ExecFlag = "false";
        }

        public void AddTestcase()
        {
            var defaultName = "Method";
            var newName = string.Empty;

            int i = 0;
            do
            {
                i++;
                newName = defaultName + i;
            } while (IsExistMethod(newName));

            AddTestcase(new Testcase
            {
                Action = newName
            });
        }
    }
}
