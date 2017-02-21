using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Printing.NET.Tests
{
    /// <summary>
    /// ������������ �������� ������ ������ <see cref="Monitor"/>.
    /// </summary>
    [TestClass]
    public class MonitorTests
    {
        /// <summary>
        /// ������������ ��������.
        /// </summary>
        protected const string MonitorName = "Test Monitor";

        /// <summary>
        /// ���� � dll ��������.
        /// </summary>
        protected const string MonitorDll = "D:/Printing Tests/mfilemon.dll";

        /// <summary>
        /// ������������ ���� � dll ��������.
        /// </summary>
        protected const string FailedMonitorDll = "noexist.dll";

        /// <summary>
        /// ���� ��������� ��������� ��������.
        /// </summary>
        [TestMethod]
        public void InstallTest()
        {
            Monitor monitor = new Monitor(MonitorName, MonitorDll);
            monitor.Install();

            Assert.IsTrue(Monitor.All.Select(m => m.Name).Contains(MonitorName));
        }

        /// <summary>
        /// ���� ���������� �������� ��������.
        /// </summary>
        [TestMethod]
        public void UninstallTest()
        {
            Monitor monitor = new Monitor(MonitorName, MonitorDll);
            monitor.Uninstall();

            Assert.IsFalse(Monitor.All.Select(m => m.Name).Contains(MonitorName));
        }

        /// <summary>
        /// ���� ��������� ��������� �������� � ���������� ��������� ���������.
        /// </summary>
        [TestMethod]
        public void TryInstallTest()
        {
            Monitor monitor = new Monitor(MonitorName, MonitorDll);
            bool f = monitor.TryInstall();

            Assert.IsTrue(f);
            Assert.IsTrue(Monitor.All.Select(m => m.Name).Contains(MonitorName));
        }

        /// <summary>
        /// ���� ���������� �������� �������� � ���������� ��������� ��������.
        /// </summary>
        [TestMethod]
        public void TryUninstallTest()
        {
            Monitor monitor = new Monitor(MonitorName, MonitorDll);
            bool f = monitor.TryUninstall();

            Assert.IsTrue(f);
            Assert.IsFalse(Monitor.All.Select(m => m.Name).Contains(MonitorName));
        }

        /// <summary>
        /// ���� ������������ ��������� ��������� ��������.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(PrintingException))]
        public void InstallFailedTest()
        {
            Monitor monitor = new Monitor(MonitorName, FailedMonitorDll);
            monitor.Install();

            Assert.IsFalse(Monitor.All.Select(m => m.Name).Contains(MonitorName));
        }
        
        /// <summary>
        /// ���� ������������ ��������� ��������� �������� � ���������� ��������� ���������.
        /// </summary>
        [TestMethod]
        public void TryInstallFailedTest()
        {
            Monitor monitor = new Monitor(MonitorName, FailedMonitorDll);
            bool f = monitor.TryInstall();

            Assert.IsFalse(f);
            Assert.IsFalse(Monitor.All.Select(m => m.Name).Contains(MonitorName));
        }
    }
}