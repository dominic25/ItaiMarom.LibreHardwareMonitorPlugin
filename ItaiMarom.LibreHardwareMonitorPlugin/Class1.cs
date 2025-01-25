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
        private const int UpdateInterval = 1000;//should be configurable 

        // Optional; If your plugin can be configured, set to "true". It'll make the "Configure" button appear in the package manager.
        public override bool CanConfigure => false;

        // Gets called when the plugin is loaded
        public override void Enable()
        {
            Instance = this;
            MacroDeckLogger.Trace(Instance, "started"); // Log a trace (loglevel 1)
            Task.Run(async () => await DoWork());
        }

        //// Optional; Gets called when the user clicks on the "Configure" button in the package manager; If CanConfigure is not set to true, you don't need to add this
        //public override void OpenConfigurator()
        //{
        //    // Open your configuration form here
        //    using (var configurator = new Configurator())
        //    {
        //        configurator.ShowDialog();
        //    }
        //}

        private static async Task DoWork()
        {
            while (true)
            {
                MacroDeckLogger.Trace(Instance, "DoWork"); // Log a trace (loglevel 1)
                LoadDllAndMonitor(Instance);
                await Task.Delay(TimeSpan.FromMilliseconds(UpdateInterval));
            }
        }

        private static void LoadDllAndMonitor(MacroDeckPlugin Instance)
        {
            try
            {
                // Path to the DLL
                string dllName = @"liberHardwareMonitorHelper.dll";
                string dllPath = @"%Appdata%\Macro Deck\plugins\ItaiMarom.LibreHardwareMonitorPlugin\";

                // Expand the environment variable
                string expandedPath = Environment.ExpandEnvironmentVariables(dllPath + dllName);
                // Load the DLL
                Assembly assembly = Assembly.LoadFrom(expandedPath);

                // Get the type of the class you want to use
                Type myClassType = assembly.GetType("liberHardwareMonitorHelper.liberHardwareMonitorHelper");

                if (myClassType != null)
                {
                    // Create an instance of the class
                    object myClassInstance = Activator.CreateInstance(myClassType);

                    // Get the method you want to invoke
                    MethodInfo myMethod = myClassType.GetMethod("Monitor");

                    if (myMethod != null)
                    {
                        // Call the method with parameters
                        myMethod.Invoke(myClassInstance, new object[] { Instance });
                    }
                    else
                    {
                        MacroDeckLogger.Error(Instance, "Method not found.");

                    }
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
        }
    }
}
