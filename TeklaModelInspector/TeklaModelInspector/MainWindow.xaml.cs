using System.Windows;
using TeklaModelInspector.Services;
using TeklaModelInspector.ViewModels;

namespace TeklaModelInspector;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        ITeklaService teklaService = new MockTeklaService();
        DataContext = new MainViewModel(teklaService);
    }
}