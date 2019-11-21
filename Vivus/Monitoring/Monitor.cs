using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivus.Logging;

namespace Vivus.Monitoring
{
    public class Monitor
    {
        public string Name { get; }
        public string[] Arguments { get; }
        public Logger StandardOutput { get; }
        public Logger StandardError { get; }
        
        public int MaxRestarts { get; set; }
        public TimeSpan MaxRestartTime { get; set; }
        public MonitorState State { get; private set; }
        public int Restarts { get; private set; }
        private string _arguments;
        private Process _process;
        

        private Monitor()
        {
            StandardOutput = new Logger();
            StandardError = new Logger();
        }
        
        public Monitor(string name) : this()
        {
            Name = name;
        }

        public Monitor(string name, params string[] arguments) : this(name)
        {
            Arguments = arguments;
            _arguments = arguments.Aggregate("", (builder, argument) => builder += argument);
        }

        public async Task RunAsync()
        {
            Run();
            while (true)
            {
                if (Restarts > MaxRestarts)
                {
                    State = MonitorState.Stopped;
                    return;
                }

                Restarts = 0;
                await Task.Delay(MaxRestartTime);
            }
        }

        private void Run()
        {

            var process = new Process();
            var psi = new ProcessStartInfo
            {
                FileName = Name,
                Arguments = _arguments,
                RedirectStandardError = true,
                RedirectStandardOutput = true
            };

            process.StartInfo = psi;
            
            process.EnableRaisingEvents = true;
            process.OutputDataReceived += (_, args) => { StandardOutput.Log(args.Data); };
            process.ErrorDataReceived += (_, args) => { StandardError.Log(args.Data, LogLevel.Error); };
            process.Exited += ProcessOnExited;
            
            process.Start();
            process.BeginErrorReadLine();
            process.BeginOutputReadLine();
            
            
        }

        private void ProcessOnExited(object? _, EventArgs e)
        {
            Restarts += 1;
        }
    }
}