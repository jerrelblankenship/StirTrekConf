namespace StirTrekWPDomain
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO.IsolatedStorage;

    public class AppSettingsStorage
    {
        // Our isolated storage settings
        private IsolatedStorageSettings isolatedStore;

        /// Constructor that gets the application settings.
        public AppSettingsStorage()
        {
            try
            {
                // Get the settings for this application.
                isolatedStore = IsolatedStorageSettings.ApplicationSettings;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception while using IsolatedStorageSettings: " + e);
            }
        }

        /// Update a setting value for our application. If the setting does not
        /// exist, then add the setting.
        public bool AddOrUpdateValue(string Key, object value)
        {
            bool valueChanged = false;
            try
            {
                // if new value is different, set the new value.
                if (!ReferenceEquals(isolatedStore[Key], value))
                {
                    isolatedStore[Key] = value;
                    valueChanged = true;
                }
            }
            catch (KeyNotFoundException)
            {
                isolatedStore.Add(Key, value);
                valueChanged = true;
            }
            catch (ArgumentException)
            {
                isolatedStore.Add(Key, value);
                valueChanged = true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception while using IsolatedStorageSettings: " + e);
            }
            return valueChanged;
        }


        /// Get the current value of the setting, or if it is not found, set the
        /// setting to the default setting.
        public T GetValueOrDefault<T>(string Key, T defaultValue)
        {
            T value;
            try
            {
                value = (T)isolatedStore[Key];
            }
            catch (KeyNotFoundException)
            {
                value = defaultValue;
            }
            catch (ArgumentException)
            {
                value = defaultValue;
            }
            return value;
        }

        /// Save the settings.
        public void Save()
        {
            isolatedStore.Save();
        }

        public DateTime LastUpdated
        {
            get { return GetValueOrDefault("LastUpdated", DateTime.MinValue); }
            set
            {
                AddOrUpdateValue("LastUpdated", value);
                Save();
            } 
        }

        public string DefaultJsonData
        {
            get { return GetValueOrDefault("DefaultJsonData", string.Empty); }
            set
            {
                AddOrUpdateValue("DefaultJsonData", value);
                Save();
            }
        }
    }
}
