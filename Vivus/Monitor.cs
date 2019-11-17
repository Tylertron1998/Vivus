using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivus
{
    public class Monitor
    {
        public string Name { get; private set; }
        public string[] Arguments { get; private set; }
        public StringBuilder StandardOutput { get; private set; }
        public StringBuilder StandardError { get; private set; }
        
        public int MaxRestarts { get; set; }
        
        private string _arguments;
        private Process _process;
        

        private Monitor()
        {
            StandardOutput = new StringBuilder();
            StandardError = new StringBuilder();
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

        public void Run()
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
            process.OutputDataReceived += (_, args) => { StandardOutput.AppendLine(args.Data); };
            process.ErrorDataReceived += (_, args) => { StandardError.AppendLine(args.Data); };

            process.Start();
            process.BeginErrorReadLine();
            process.BeginOutputReadLine();
            
            
        }

        private bool ShouldRestart()
        {
            return true;
        }
    }
}