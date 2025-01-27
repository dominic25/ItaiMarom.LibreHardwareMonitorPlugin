using System;
using SuchByte.MacroDeck.Plugins;
using System.Reflection;
using System.Threading.Tasks;
using SuchByte.MacroDeck.Logging;
using System.Collections.Generic;
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
        MethodInfo monitorMethod;
        MethodInfo listOfSensorsMethod;
        List<(String hardware, String sensor)> _requestedSensors;
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
            LoadDll();
            List<(String hardware, String sensor)> listOfSensors = null;
            try
            {
                if (listOfSensorsMethod != null)
                {
                    // Call the method with parameters
                    listOfSensors = (List<(string hardware, string sensor)>)listOfSensorsMethod.Invoke(myClassInstance, []);
                }
                else
                {
                    MacroDeckLogger.Error(Instance, "ListOfSensors Method not found.");
                }
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Error(Instance, $"Error: {ex.Message}");
            }
            // Open your configuration form here
            if (listOfSensors != null)
            {
                using (var configurator = new PluginConfig(listOfSensors))
                {

                    configurator.ShowDialog();
                    pollingRate = configurator.getPollingRate();
                    _requestedSensors = configurator.getRequestedSensors();
                }
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
                    monitorMethod = myClassType.GetMethod("Monitor");
                    listOfSensorsMethod = myClassType.GetMethod("ListOfSensors");
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
                if (monitorMethod != null)
                {
                    // Call the method with parameters
                    monitorMethod.Invoke(myClassInstance, [Instance, _requestedSensors]);
                }
                else
                {
                    MacroDeckLogger.Error(Instance, "Monitor Method not found.");
                }
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Error(Instance, $"Error: {ex.Message}");
            }
        }
    }
}
