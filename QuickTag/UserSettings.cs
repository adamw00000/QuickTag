using FluentValidation;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace QuickTag
{
    public interface IUserSettings: INotifyPropertyChanged
    {
        string MusicLibraryPath { get; set; }
        int TrackListCoverMiniatureSize { get; set; }
    }

    public class UserSettings: ReactiveObject, IUserSettings
    {
        private readonly UserSettingsValidator _validator = new();

        [Reactive]
        public string MusicLibraryPath { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
        [Reactive]
        public int TrackListCoverMiniatureSize { get; set; } = 30;

        private const string _filename = "userSettings.json";
        private const string _appDirName = "Quick Tag";
        private static readonly string _settingsPath = Path.Join(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            _appDirName,
            _filename
        );

        public static UserSettings Load()
        {
            UserSettings? settings = null;
            try
            {
                using FileStream file = File.OpenRead(_settingsPath);
                settings = JsonSerializer.Deserialize<UserSettings>(file);
            }
            catch (Exception e) when (
                e is DirectoryNotFoundException ||
                e is FileNotFoundException)
            {
                settings = GetDefault();
            }
            catch
            {
                // TODO: MVVM error handling
                throw;
            }

            if (settings == null)
                settings = GetDefault();

            settings.PropertyChanged += (sender, e) =>
                Observable.Start(() => settings.Save(), RxApp.TaskpoolScheduler);

            return settings;
        }

        private void Save()
        {
            if (!IsValid)
            {
                // TODO: MVVM error handling
                return;
            }

            Directory.CreateDirectory(Path.GetDirectoryName(_settingsPath)!);

            var options = new JsonSerializerOptions
            {
                IgnoreReadOnlyProperties = true,
                WriteIndented = true,
                IncludeFields = false,
            };
            try
            {
                using FileStream file = File.Open(_settingsPath, FileMode.Create);
                JsonSerializer.Serialize(file, this, options);
            }
            catch (Exception e) when (
                e is DirectoryNotFoundException ||
                e is FileNotFoundException)
            {
                // TODO: MVVM error handling
                throw;
            }
        }

        public static UserSettings GetDefault() => new();

        public bool IsValid => _validator.Validate(this).IsValid;
        [JsonIgnore]
        public IEnumerable<string> Errors => _validator.Validate(this).Errors.Select(e => e.ErrorMessage);
    }

    public class UserSettingsValidator: AbstractValidator<IUserSettings>
    {
        public UserSettingsValidator()
        {
            RuleFor(settings => settings.MusicLibraryPath)
                .Must(BeValidDirectory);

            RuleFor(settings => settings.TrackListCoverMiniatureSize)
                .GreaterThanOrEqualTo(10)
                .LessThanOrEqualTo(100);
        }

        private bool BeValidDirectory(string path)
        {
            try
            {
                _ = new DirectoryInfo(path);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
