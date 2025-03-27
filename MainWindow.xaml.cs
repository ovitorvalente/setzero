using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using SetZero.Application.Services;
using SetZero.Infrastructure.Data;
using SetZero.src.Domain.Entities;
using SetZero.src.Infrastructure.Services;

namespace SetZero;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
        this.KeyDown += ShortcutKeyToReset;
    }

    private void ShortcutKeyToReset(object sendder, KeyEventArgs e)
    {
        if (e.Key == Key.F2)
            ResetMovimentValue();
    }

    private async void ResetMovimentValue(object? sender = null, RoutedEventArgs? e = null)
    {
        if (string.IsNullOrEmpty(inputFilial.Text) || string.IsNullOrEmpty(inputSequencia.Text) || string.IsNullOrEmpty(inputLinha.Text))
        {
            ShowStatus("Preencha todos os campos!", "#ef4444", "#FFF");
            return;
        }

        var data = new MovimentData
        {
            Filial__Codigo = int.Parse(inputFilial.Text),
            Sequencia = int.Parse(inputSequencia.Text),
            Linha = int.Parse(inputLinha.Text),
        };

        try
        {
            string folderPath = AppDomain.CurrentDomain.BaseDirectory;
            var databaseConfig = FileReader.ReadConfig(folderPath);

            var databaseConnection = new DatabaseConnection();
            var databaseService = new DatabaseService();

            using (var connection = databaseConnection.ConnectToDatabase(databaseConfig))
            {
                await databaseService.UpdateMovements(data, connection);
            }

            MessageBox.Show("Procedimento realizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        inputSequencia.Clear();
        inputLinha.Clear();
        inputSequencia.Focus();
    }

    private bool IsTextNumeric(string text)
    {
        return int.TryParse(text, out _);
    }

    private async void ShowStatus(string message, string backgroundColor, string foregroundColor)
    {
        statusBar.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(backgroundColor));
        statusBar.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(foregroundColor));

        statusText.Inlines.Clear();
        statusText.Inlines.Add(message);

        await Task.Delay(3000);

        statusBar.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000"));
        statusBar.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF1EDED"));
        statusText.Inlines.Clear();
        statusText.Inlines.Add("© 2025 Vitor Valente. Todos os direitos reservados");
    }

    private void OnPreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
    {
        e.Handled = !IsTextNumeric(e.Text);
    }

    private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
        {
            FileName = e.Uri.ToString(),
            UseShellExecute = true
        });
        e.Handled = true;
    }

    private void inputSequenceFocus(object sender, RoutedEventArgs e)
    {
        inputSequencia.Focus();
    }
}