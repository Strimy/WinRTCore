using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WinRTCore.Utilities.Tasks;

namespace WinRTCore.UnitTests.Utilities.Tasks
{
    [TestClass]
    public class NamedTasksTests
    {
        [TestMethod]
        public void TestAddNamedTask()
        {
            int modifiedValue = 0;

            Action a = new Action(delegate
                {
                    modifiedValue = 20;
                });

            // Simply create a new named task
            NamedTask.AddNamedTask("TestTask", 1000, a);

            ManualResetEventSlim mre = new ManualResetEventSlim();
            mre.Wait(200);
            // Ensure the timeout is working
            Assert.AreEqual(0, modifiedValue);

            mre.Wait(1000);
            // After 1 seconds, and a little more the task should be invoked
            Assert.AreEqual(20, modifiedValue);
        }

        [TestMethod]
        public void TestCancelTask()
        {
            int modifiedValue = 0;
            ManualResetEventSlim mre = new ManualResetEventSlim();

            Action a = new Action(delegate
            {
                modifiedValue = 20;
            });

            // Add a new task
            NamedTask.AddNamedTask("TestTask", 1000, a);

            
            mre.Wait(200);
            Assert.AreEqual(0, modifiedValue);

            // Cancel the current task
            NamedTask.CancelNamedTask("TestTask");

            mre.Wait(1000);
            // The value should NOT have been modified
            Assert.AreEqual(0, modifiedValue);
        }

        [TestMethod]
        public void TestRestartTask()
        {
            int modifiedValue = 0;
            ManualResetEventSlim mre = new ManualResetEventSlim();

            Action a = new Action(delegate
            {
                modifiedValue = 20;
            });


            // Create a new named task
            NamedTask.AddNamedTask("TestTask", 1000, a);

            // Wait a few moment to ensure the task is running, we should still be at 0
            mre.Wait(600);
            Assert.AreEqual(0, modifiedValue);
            NamedTask.AddNamedTask("TestTask", 1000, a);

            mre.Wait(600);

            NamedTask.AddNamedTask("TestTask", 1000, a);
            Assert.AreEqual(0, modifiedValue);

            mre.Wait(600);
            Assert.AreEqual(0, modifiedValue);

            mre.Wait(1200);
            Assert.AreEqual(20, modifiedValue);
        }

        [TestMethod]
        public void TestReplaceTask()
        {
            int modifiedValue = 0;
            ManualResetEventSlim mre = new ManualResetEventSlim();

            Action a = new Action(delegate
            {
                modifiedValue = 20;
            });


            Action b = new Action(delegate
            {
                modifiedValue = 100;
            });

            // Create a new named task
            NamedTask.AddNamedTask("TestTask", 1000, a);

            // Wait a few moment to ensure the task is running, we should still be at 0
            mre.Wait(600);
            Assert.AreEqual(0, modifiedValue);
            NamedTask.AddNamedTask("TestTask", 1000, a);

            mre.Wait(600);

            NamedTask.AddNamedTask("TestTask", 1000, b);
            Assert.AreEqual(0, modifiedValue);

            mre.Wait(600);
            Assert.AreEqual(0, modifiedValue);

            mre.Wait(1200);
            Assert.AreEqual(100, modifiedValue);
        }
    }
}
