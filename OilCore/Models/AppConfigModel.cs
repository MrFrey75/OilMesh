using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OilCore.Enumerations;

namespace OilCore.Models
{
    public class AppConfigModel
    {
        [Required]
        [MaxLength(100)]
        public string AppName { get; set; }

        [Range(0, int.MaxValue)]
        public int AppVersionMajor { get; set; }

        [Range(0, int.MaxValue)]
        public int AppVersionMinor { get; set; }

        [Range(0, int.MaxValue)]
        public int AppVersionPatch { get; set; }

        [MaxLength(500)]
        public string AppDescription { get; set; }

        [Required]
        [MaxLength(50)]
        public EnvironmentType Environment { get; set; } // e.g., "Development", "Staging", "Production"

        [MaxLength(100)]
        public string UpdatedBy { get; set; } = "system";

        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }

        [JsonIgnore]
        public string AppVersion => $"{AppVersionMajor}.{AppVersionMinor}.{AppVersionPatch}";

        public AppConfigModel(string appName, EnvironmentType environment = EnvironmentType.Development)
        {
            AppName = appName;
            AppDescription = string.Empty;
            Environment = environment;
            AppVersionMajor = 0;
            AppVersionMinor = 1;
            AppVersionPatch = 0;
            CreatedAt = DateTime.UtcNow;
            LastUpdatedAt = DateTime.UtcNow;
        }

        public AppConfigModel UpdateDescription(string description)
        {
            AppDescription = description;
            Touch();
            return this;
        }

        public AppConfigModel UpdateVersionPatch()
        {
            AppVersionPatch += 1;
            Touch();
            return this;
        }

        public AppConfigModel UpdateVersionMinor()
        {
            AppVersionMinor += 1;
            AppVersionPatch = 0;
            Touch();
            return this;
        }

        public AppConfigModel UpdateVersionMajor()
        {
            AppVersionMajor += 1;
            AppVersionMinor = 0;
            AppVersionPatch = 0;
            Touch();
            return this;
        }

        public AppConfigModel SetEnvironment(EnvironmentType environment)
        {
            Environment = environment;
            Touch();
            return this;
        }

        public AppConfigModel SetUpdatedBy(string user)
        {
            UpdatedBy = user;
            Touch();
            return this;
        }

        private void Touch()
        {
            LastUpdatedAt = DateTime.UtcNow;
        }
    }
}
