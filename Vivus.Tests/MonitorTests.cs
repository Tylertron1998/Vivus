using System.Threading.Tasks;
using NUnit.Framework;
using Vivus.Monitoring;

namespace Vivus.Tests
{
    [TestFixture]
    public class MonitorTests
    {
        [Test]
        public async Task Assert_MaxRestarts_Handled()
        {
            var monitor = new Monitor("node", "./js/fastexit.js");
            await monitor.RunAsync();
            Assert.AreEqual(MonitorState.Stopped, monitor.State);
        }
    }
}