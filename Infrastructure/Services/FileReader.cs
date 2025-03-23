using System.IO;
using System.Windows;
using Newtonsoft.Json;
using SetZero.Models.Entities;

namespace SetZero.Infrastructure.Services
{
    public class FileReader
    {
        public static DatabaseConfig ReadConfig(string folderPath)
        {
            string filePath = Path.Combine(folderPath, "ArqID.txt");

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Arquivo não encontrado: {filePath}");
                Application.Current.Shutdown();
                return null;
            }

            string json = File.ReadAllText(filePath);
            DatabaseConfig config = JsonConvert.DeserializeObject<DatabaseConfig>(json);

            if(config == null)
            {
                throw new InvalidDataException("Erro ao ler as configurações do arquivo.");
                Application.Current.Shutdown();
                return null;
            }

            return config;
        }
    }
}
