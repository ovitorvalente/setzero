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

    private void HandleConnectionDatabase(string _folderPath)
    {
        var config = FileReader.ReadConfig(_folderPath);
        DatabaseConnection.ConnectToDatabase(config);
    }
}