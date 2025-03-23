using System.IO;
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
                throw new FileNotFoundException($"Arquivo não encontrado: {filePath}");

            string json = File.ReadAllText(filePath);
            DatabaseConfig config = JsonConvert.DeserializeObject<DatabaseConfig>(json);

            return config;
        }
    }
}
