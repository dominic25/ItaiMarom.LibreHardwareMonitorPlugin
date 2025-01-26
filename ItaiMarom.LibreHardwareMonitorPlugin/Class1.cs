using System;
using SuchByte.MacroDeck.Plugins;
using System.Reflection;
using System.Threading.Tasks;
using SuchByte.MacroDeck.Logging;
namespace ItaiMarom.LibreHardwareMonitorPlugin
{
    public class LibreHardwareMonitorPlugin : MacroDeckPlugin
    {
        internal static MacroDeckPlugin Instance { get; set; }
        private int pollingRate = 1000;//should be configurable 
        private bool dllLoaded = false;
        Assembly assembly;
        Type myClassType;
        object myClassInstance;
        MethodInfo myMethod;

        // Optional; If your plugin can be configured, set to "true". It'll make the "Configure" button appear in the package manager.
        public override bool CanConfigure => true;

        // Gets called when the plugin is loaded
        public override void Enable()
        {
            Instance = this;
            MacroDeckLogger.Trace(Instance, "started"); // Log a trace (loglevel 1)
            Task.Run(async () => await DoWork());
        }

        //// Optional; Gets called when the user clicks on the "Configure" button in the package manager; If CanConfigure is not set to true, you don't need to add this
        public override void OpenConfigurator()
        {
            // Open your configuration form here
            using (var configurator = new DialogForm1())
            {
                configurator.ShowDialog();
                pollingRate = configurator.getPollingRate();
            }
        }

        private async Task DoWork()
        {
            LoadDll();
            while (true)
            {
                Monitor();
                await Task.Delay(TimeSpan.FromMilliseconds(pollingRate));
            }
        }

        private void LoadDll()
        {
            try
            {
                if (dllLoaded)
                    return;
                // Path to the DLL
                string dllName = @"liberHardwareMonitorHelper.dll";
                string dllPath = @"%Appdata%\Macro Deck\plugins\ItaiMarom.LibreHardwareMonitorPlugin\";

                // Expand the environment variable
                string expandedPath = Environment.ExpandEnvironmentVariables(dllPath + dllName);
                // Load the DLL
                assembly = Assembly.LoadFrom(expandedPath);

                // Get the type of the class you want to use
                myClassType = assembly.GetType("liberHardwareMonitorHelper.liberHardwareMonitorHelper");

                if (myClassType != null)
                {
                    // Create an instance of the class
                    myClassInstance = Activator.CreateInstance(myClassType);

                    // Get the method you want to invoke
                    myMethod = myClassType.GetMethod("Monitor");
                }
                else
                {
                    MacroDeckLogger.Error(Instance, "Class not found.");
                }
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Error(Instance, $"Error: {ex.Message}");
            }
            dllLoaded = true;
        }
        private void Monitor()
        {
            try
            {
                if (myMethod != null)
                {
                    // Call the method with parameters
                    myMethod.Invoke(myClassInstance, [Instance]);
                }
                else
                {
                    MacroDeckLogger.Error(Instance, "Method not found.");
                }
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Error(Instance, $"Error: {ex.Message}");
            }
        }
    }
}
