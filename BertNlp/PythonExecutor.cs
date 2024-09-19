using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Python.Included;
using Python.Runtime;

namespace PythonExecutor
{
    public class PythonExecutor
    {
        private bool _initialized;
        private readonly IEnumerable<string> _libraries;

        public PythonExecutor(IEnumerable<string> libraries)
        {
            _libraries = libraries ?? throw new ArgumentNullException(nameof(libraries));
            _initialized = false;
        }

        // Event logging method
        private void LogEvent(string message)
        {
            Console.WriteLine($"[{DateTime.Now}] {message}");
            // Optionally, write to a log file:
            // File.AppendAllText("install_log.txt", $"[{DateTime.Now}] {message}{Environment.NewLine}");
        }
        public async Task Initialize()
        {
            if (!_initialized)
            {
                // Log event: Starting initialization
                LogEvent("Starting Python environment initialization...");

                // Install Python and required libraries
                Installer.InstallPath = Path.GetFullPath(".");
                Installer.LogMessage += LogEvent;

                try
                {
                    LogEvent("Installing Python...");
                    await Installer.SetupPython();

                    LogEvent("Installing pip...");
                    await Installer.TryInstallPip();

                    // Install required Python libraries with progress
                    LogEvent("Installing required Python libraries...");
                    int totalLibraries = _libraries.Count();
                    for (int i = 0; i < totalLibraries; i++)
                    {
                        string lib = _libraries.ElementAt(i);
                        LogEvent($"Installing Python library: {lib}...");

                        await Installer.PipInstallModule(lib);

                        // Calculate progress and log it
                        double progress = ((i + 1) / (double)totalLibraries) * 100;
                        LogEvent($"Installation progress: {progress:F2}% ({i + 1}/{totalLibraries}) libraries installed.");
                    }

                    // Initialize Python engine
                    PythonEngine.Initialize();
                    LogEvent("Python environment initialized successfully.");

                    _initialized = true;
                }
                catch (Exception ex)
                {
                    LogEvent($"Error during initialization: {ex.Message}");
                }
            }
        }

        public void ExecuteScript(string pythonScript)
        {
            if (!_initialized)
            {
                throw new InvalidOperationException("Python engine has not been initialized.");
            }

            PythonEngine.Exec(pythonScript);
        }

        public dynamic ExecuteFunction(string moduleName, string functionName, params object[] args)
        {
            if (!_initialized)
            {
                throw new InvalidOperationException("Python engine has not been initialized.");
            }

            using (Py.GIL())
            {
                dynamic module = Py.Import(moduleName);
                dynamic function = module.GetAttr(functionName);
                return function(args);
            }
        }

        public void Shutdown()
        {
            if (_initialized)
            {
                try
                {
                    PythonEngine.Shutdown();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                _initialized = false;
            }
        }
    }
}