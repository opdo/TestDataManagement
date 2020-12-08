using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestDataManagement.Helpers;
using TestDataManagement.Models;

namespace TestDataManagement.Forms
{
    public partial class ManageTestcase : Form
    {
        private List<TestSuite> listTest;
        private string filePathSelected;

        public ManageTestcase()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            // Get list history
            LoadListHistory();
        }

        private void LoadListHistory()
        {
            txtFile.Items.Clear();
            var listHistory = ConfigHelper.GetFileHistory();
            txtFile.Items.AddRange(listHistory.ToArray());
        }

        private void LoadListTest()
        {
            listTestcase.Items.Clear();
            if (listTest != null)
            {
                var listTestMethod = listTest
                    .SelectMany(x => x.testsuite)
                    .SelectMany(x => x.testcases)
                    .Select(x => x.Action)
                    .ToArray();
                listTestcase.Items.AddRange(listTestMethod);
            }
        }

        /// <summary>
        /// Load json from file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoad_Click(object sender, EventArgs e)
        {
            var filePath = string.IsNullOrEmpty(txtFile.Text) ? null : txtFile.Text.Trim();
            if (filePath is null)
            {
                MessageBox.Show("Please enter file path", "Load test file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!File.Exists(filePath))
            {
                var result = MessageBox.Show($"File {filePath} does not exist, do you want to create new test suite file?", "Load test file", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (!result.Equals(DialogResult.Yes)) return;
                // Save new file

                try
                {
                    FileHelper.SaveJson(filePath, new List<TestSuite>());
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Something went wrong, exception message: {ex.Message}", "Create new test file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Add to history
            ConfigHelper.AddNewFileHistory(filePath);

            // Load json
            try
            {
                listTest = TestcaseHelper.LoadTestFile(filePath);
                filePathSelected = filePath;
                LoadListTest();
            }
            catch (Exception ex)
            {
                filePathSelected = null;
                listTest = null;
                MessageBox.Show($"Something went wrong, exception message: {ex.Message}", "Load test file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void listTestcase_MouseUp(object sender, MouseEventArgs e)
        {
            int location = listTestcase.IndexFromPoint(e.Location);
            if (e.Button == MouseButtons.Right)
            {
                LoadContextMenu();
                listTestcase.SelectedIndex = location;             //Index selected
                contextMenuStrip.Show(PointToScreen(e.Location));   //Show Menu
            }
        }

        private void LoadContextMenu()
        {
            var flag = false;
            if (listTestcase.SelectedIndex != -1)
            {
                flag = true;
            }
            menuDelete.Enabled = flag;
            menuDisable.Enabled = flag;
            menuEnable.Enabled = flag;
        }

        private void menuNew_Click(object sender, EventArgs e)
        {
            if (this.listTest is null)
            {
                this.listTest = new List<TestSuite>();
            }

            if (this.listTest.Count < 1)
            {
                this.listTest.Add(new TestSuite());
            }

            var testSuite = this.listTest[0];
            testSuite.AddTestcase();
            LoadListTest();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (listTest != null && listTest.Count > 0)
            {
                try
                {
                    FileHelper.SaveJson(filePathSelected, listTest);
                    MessageBox.Show("Success!", "Save test file", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Something went wrong, exception message: {ex.Message}", "Save test file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Can't save data because list test is empty", "Save test file", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
