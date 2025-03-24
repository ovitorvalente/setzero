using System.IO;
using System.Windows;
using SetZero.Infrastructure.Services;
using SetZero.Models.Entities;

namespace SetZero;

public partial class MainWindow : Window
{
    private readonly string _folderPath = AppDomain.CurrentDomain.BaseDirectory;
    private DatabaseConfig _config;
    public MainWindow()
    {
        InitializeComponent();
        HandleConnectionDatabase(_folderPath);
    }

    private void HandleConnectionDatabase(string folderPath)
    {
        try
        {
            _config = FileReader.ReadConfig(folderPath);

            if (_config == null)
            {
                MessageBox.Show("Falha ao ler o arquivo de configuração.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
                return;
            }

            var connection = DatabaseConnection.ConnectToDatabase(_config);
            MessageBox.Show("Conexão bem-sucedida com o banco de dados!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (FileNotFoundException e)
        {
            MessageBox.Show(e.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            Application.Current.Shutdown();
        }
        catch (Exception e)
        {
            MessageBox.Show($"Erro inesperado: {e.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            Application.Current.Shutdown();
        }
    }


    private async void ResetMovimentValue(object sender, RoutedEventArgs e)
    {

        var data = new MovimentData
        {
            Filial__Codigo = int.Parse(inputFilial.Text),
            Sequencia = int.Parse(inputSequencia.Text),
            Linha = int.Parse(inputNItem.Text),
        };

        try
        {
            var service = new DatabaseService();
            await DatabaseService.UpdateMoviments(data, _config);
            statusText.Text = "Aguarde um momento...";
            statusText.Text = "Procedimento realizado com sucesso!";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao realizar operação: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);
    }
}