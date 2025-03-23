using System.IO;
using System.Windows;
using SetZero.Infrastructure.Services;

namespace SetZero;

public partial class MainWindow : Window
{
    private readonly string _folderPath = AppDomain.CurrentDomain.BaseDirectory;
    public MainWindow()
    {
        InitializeComponent();
        HandleConnectionDatabase(_folderPath);
    }

    private void HandleConnectionDatabase(string folderPath)
    {
        try
        {
            var config = FileReader.ReadConfig(folderPath);
            DatabaseConnection.ConnectToDatabase(config);
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

    private void ResetMovimentValue(object sender, RoutedEventArgs e)
    {
        string filial = inputFilial.Text;
        string sequencia = inputSequencia.Text;
        string nItem = inputNItem.Text;

        statusText.Text = "Procedimento realizado com sucesso!";
    }
}