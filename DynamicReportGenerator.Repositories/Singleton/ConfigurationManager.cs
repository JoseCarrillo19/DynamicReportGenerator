namespace DynamicReportGenerator.Repositories.Singleton
{
    public class ConfigurationManager
    {
        private static readonly Lazy<ConfigurationManager> _instance = new(() => new ConfigurationManager());

        public static ConfigurationManager Instance => _instance.Value;

        public string ReportFormat { get; private set; }

        private ConfigurationManager()
        {
            // Simulación de carga de configuración
            ReportFormat = "Excel";
        }
    }
}
