using System.Windows;
using TeklaModelInspector.ViewModels;

namespace TeklaModelInspector;

public partial class MainWindow : Window
{
    public MainWindow(MainViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}