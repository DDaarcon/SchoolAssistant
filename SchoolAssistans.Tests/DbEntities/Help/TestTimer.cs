using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAssistans.Tests.DbEntities
{
    internal class TestTimer : IDisposable
    {
        private Stopwatch _timer;
        public TestTimer()
        {
            _timer = new Stopwatch();
            _timer.Start();
        }

        public void Dispose()
        {
            _timer.Stop();
            Console.WriteLine($"Test took: {_timer.ElapsedMilliseconds} ms ({_timer.ElapsedTicks} ticks)");
        }
    }
}
