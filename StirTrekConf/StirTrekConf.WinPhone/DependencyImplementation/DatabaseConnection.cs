namespace StirTrekConf.WinPhone.DependencyImplementation
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO.IsolatedStorage;
    using PortableCore.AppSpecificInterfaces;
    using PortableCore.DomainLayer;

    public class DatabaseConnection : IDatabaseConnection
    {
        private readonly IsolatedStorageSettings isolatedStorageSettings;

        public DatabaseConnection()
        {
            try
            {
                // Get the settings for this application.
                isolatedStorageSettings = IsolatedStorageSettings.ApplicationSettings;
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
                if (!ReferenceEquals(isolatedStorageSettings[Key], value))
                {
                    isolatedStorageSettings[Key] = value;
                    valueChanged = true;
                }
            }
            catch (KeyNotFoundException)
            {
                isolatedStorageSettings.Add(Key, value);
                valueChanged = true;
            }
            catch (ArgumentException)
            {
                isolatedStorageSettings.Add(Key, value);
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
                value = (T)isolatedStorageSettings[Key];
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
            isolatedStorageSettings.Save();
        }

        public void SaveFeed(StirTrekFeed feed)
        {
            AddOrUpdateValue("StirTrekFeed", feed);
            Save();
        }

        public DateTime GetLastUpdated()
        {
            return GetValueOrDefault("LastUpdateDate", DateTime.MinValue);
        }

        public void SaveLastUpdatedInformation(bool needsUpdated, DateTime lastUpdated)
        {
            AddOrUpdateValue("LastUpdateDate", lastUpdated);
            AddOrUpdateValue("NeedToBeUpdated", needsUpdated);
            Save();
        }

        public StirTrekFeed StirTrekFeed
        {
            get { return GetValueOrDefault("StirTrekFeed", new StirTrekFeed()); }
        }
    }
}
