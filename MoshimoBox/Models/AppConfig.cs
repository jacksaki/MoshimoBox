using System.Text.Json;
using System.Text.Json.Serialization;
using Livet;
using Wpf.Ui.Appearance;

namespace MoshimoBox.Models
{
    public class AppConfig : NotificationObject
    {
        public static string Path => System.IO.Path.ChangeExtension(
            System.Reflection.Assembly.GetExecutingAssembly().Location, ".conf");
        public void Save()
        {
            System.IO.File.WriteAllText(Path, JsonSerializer.Serialize(
                this, new JsonSerializerOptions() { WriteIndented = true }));
        }
        public static AppConfig Load()
        {
            AppConfig conf;
            if (System.IO.File.Exists(Path))
            {
                conf = JsonSerializer.Deserialize<AppConfig>(System.IO.File.ReadAllText(Path));
            }
            else
            {
                conf = new AppConfig();
            }
            if (string.IsNullOrEmpty(conf.ThemeMode))
            {
                conf.ThemeMode = "light";
            }
            return conf;
        }
        private string _ThemeMode;
        [JsonPropertyName("theme_mode")]
        public string ThemeMode
        {
            get
            {
                return _ThemeMode;
            }
            set
            {
                if (_ThemeMode == value)
                {
                    return;
                }
                _ThemeMode = value;
                ApplyTheme();
                RaisePropertyChanged();
            }
        }

        public ApplicationTheme GetCurrentTheme()
        {
            if ("dark".Equals(this.ThemeMode))
            {
                return ApplicationTheme.Dark;
            }
            else
            {
                return ApplicationTheme.Light;
            }
        }

        public void ApplyTheme()
        {
            if ("dark".Equals(this.ThemeMode))
            {
                ApplicationThemeManager.Apply(ApplicationTheme.Dark);
            }
            else
            {
                ApplicationThemeManager.Apply(ApplicationTheme.Light);
            }
        }
    }
}
