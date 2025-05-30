﻿using Newtonsoft.Json;
using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Plugins;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
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
        MethodInfo DeleteAllVariablesMethod;
        List<(String hardware, String type, String sensor)> _requestedSensors;
        // Optional; If your plugin can be configured, set to "true". It'll make the "Configure" button appear in the package manager.
        public override bool CanConfigure => true;

        // Gets called when the plugin is loaded
        public override void Enable()
        {
            Instance = this;

            String serialized = PluginConfiguration.GetValue(this, "requestedSensors");
            if (serialized != "")
                _requestedSensors = JsonConvert.DeserializeObject<List<(String hardware, String type, String sensor)>>(serialized);
            String strPollingRate = PluginConfiguration.GetValue(this, "pollingRate");
            if (strPollingRate != "")
                pollingRate = int.Parse(strPollingRate);

            pollingRate = int.Parse(PluginConfiguration.GetValue(this, "pollingRate"));
            Task.Run(async () => await DoWork());
        }

        //// Optional; Gets called when the user clicks on the "Configure" button in the package manager; If CanConfigure is not set to true, you don't need to add this
        public override void OpenConfigurator()
        {
            LoadDll();
            List<(String hardware, String type, String sensor)> listOfSensors = null;
            try
            {
                if (listOfSensorsMethod != null)
                {
                    // Call the method with parameters
                    listOfSensors = (List<(String hardware, String type, String sensor)>)listOfSensorsMethod.Invoke(myClassInstance, []);
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
                using var configurator = new PluginConfig(this, listOfSensors);
                configurator.ShowDialog();
                pollingRate = configurator.GetPollingRate();
                _requestedSensors = configurator.GetRequestedSensors();
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
                String dllName = @"liberHardwareMonitorHelper.dll";
                String dllPath = @"%Appdata%\Macro Deck\plugins\ItaiMarom.LibreHardwareMonitorPlugin\";

                // Expand the environment variable
                String expandedPath = Environment.ExpandEnvironmentVariables(dllPath + dllName);
                // Load the DLL
                assembly = Assembly.LoadFrom(expandedPath);

                // Get the type of the class you want to use
                myClassType = assembly.GetType("liberHardwareMonitorHelper.LiberHardwareMonitorHelper");

                if (myClassType != null)
                {
                    // Create an instance of the class
                    myClassInstance = Activator.CreateInstance(myClassType);

                    // Get the method you want to invoke
                    monitorMethod = myClassType.GetMethod("Monitor");
                    listOfSensorsMethod = myClassType.GetMethod("ListOfSensors");
                    DeleteAllVariablesMethod = myClassType.GetMethod("DeleteAllVariables");
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

        public void DeleteAllVariables()
        {
            try
            {
                if (monitorMethod != null)
                {
                    // Call the method with parameters
                    DeleteAllVariablesMethod.Invoke(myClassInstance, []);
                }
                else
                {
                    MacroDeckLogger.Error(Instance, "DeleteAllVariables Method not found.");
                }
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Error(Instance, $"Error: {ex.Message}");
            }
        }
    }
}
