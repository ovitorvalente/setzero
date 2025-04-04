using System.Diagnostics;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using SetZero.src.Domain.Entities;

namespace SetZero.src.Infrastructure.Services
{
    public class FileReader
    {
        public static DatabaseConfig ReadConfig()
        {
            string exePath = Process.GetCurrentProcess().MainModule.FileName;
            string trueFolder = Path.GetDirectoryName(exePath);

            if (string.IsNullOrEmpty(trueFolder))
                throw new InvalidOperationException("Não foi possível determinar a pasta do aplicativo.");

            string filePath = Path.Combine(trueFolder, "ArqID.txt");

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Arquivo não encontrado: {filePath}");

            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<DatabaseConfig>(json)
                   ?? throw new InvalidDataException("Formato inválido no arquivo de configuração.");
        }
    }
}
