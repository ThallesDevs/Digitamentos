using System;
using System.IO;
using Newtonsoft.Json;
using Digitamentos.Models;

namespace Digitamentos.Services
{
    public class SettingsManager
    {
        private static readonly string SettingsFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.json");

        public static UserSettings LoadSettings()
        {
            if (File.Exists(SettingsFilePath))
            {
                try
                {
                    string json = File.ReadAllText(SettingsFilePath);
                    return JsonConvert.DeserializeObject<UserSettings>(json) ?? new UserSettings();
                }
                catch
                {
                    return new UserSettings();
                }
            }
            return new UserSettings();
        }

        public static void SaveSettings(UserSettings settings)
        {
            try
            {
                string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
                File.WriteAllText(SettingsFilePath, json);
            }
            catch { }
        }
    }
}
