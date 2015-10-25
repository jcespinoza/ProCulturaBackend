using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ProCultura.CrossCutting.Settings;

namespace ProCultura.CrossCutting.L10N
{
    public class DatabaseLocalizationService: ILocalizationService
    {
        public string GetLocalizedString(string resourceKey, string languageId = "en")
        {
            var resource = string.Empty;
            var connectionString = ConfigurationManager.ConnectionStrings[AppStrings.DatabaseConnectionName].ToString();
            using (var connection = new SqlConnection(connectionString))
            {
                var sqlCommand = @"SELECT EntryKey, LanguageId, Value FROM Localization.Dictionary "
                                + @"WHERE EntryKey = '" + resourceKey + "' AND '" + languageId + "' LIKE LanguageId";
                connection.Open();
                var command = new SqlCommand(sqlCommand, connection);
                command.CommandType = CommandType.Text;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        resource = reader["Value"].ToString();
                        break;
                    }
                }
            }
            if (string.IsNullOrEmpty(resource))
            {
                resource = resourceKey;
            }
            return resource;
        }
    }
}
