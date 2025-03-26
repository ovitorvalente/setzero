using System.IO;
using Newtonsoft.Json;
using SetZero.src.Domain.Entities;

namespace SetZero.src.Infrastructure.Services
{
    public class FileReader
    {
        public static DatabaseConfig ReadConfig(string folderPath)
        {
            string filePath = Path.Combine(folderPath, "ArqID.txt");

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Arquivo não encontrado: {filePath}");

            string json = File.ReadAllText(filePath);
            var config = JsonConvert.DeserializeObject<DatabaseConfig>(json);
            if (config != null) return config;

            throw new InvalidDataException("Erro ao ler as configurações do arquivo.");
        }
    }
}
