using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeklaModelInspector.Models;
using TeklaModelInspector.Services;

namespace TeklaModelInspector.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly ITeklaService _teklaService;

    [ObservableProperty]
    private string connectionStatus = "Nepřipojeno";

    [ObservableProperty]
    private string modelName = "–";

    [ObservableProperty]
    private bool isBusy;

    public ObservableCollection<TeklaPart> Parts { get; } = new();

    public MainViewModel(ITeklaService teklaService)
    {
        _teklaService = teklaService;
        RefreshConnectionStatus();
    }

    private void RefreshConnectionStatus()
    {
        ConnectionStatus = _teklaService.IsConnected
            ? "Připojeno"
            : "Nepřipojeno";

        ModelName = _teklaService.IsConnected
            ? _teklaService.ModelName
            : "–";
    }

    [RelayCommand]
    private async Task LoadPartsAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;
            Parts.Clear();

            var parts = await _teklaService.GetPartsAsync();

            foreach (var part in parts)
                Parts.Add(part);
        }
        finally
        {
            IsBusy = false;
        }
    }
}
