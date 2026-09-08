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
    public bool IsNotBusy => !IsBusy;

    [ObservableProperty]
    private string? errorMessage;

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

    partial void OnIsBusyChanged(bool value)
    {
        OnPropertyChanged(nameof(IsNotBusy));
    }

    [RelayCommand]
    private async Task LoadPartsAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;
            ErrorMessage = null;
            Parts.Clear();

            if (!_teklaService.IsConnected)
            {
                ErrorMessage = "Tekla Structures není spuštěná nebo není otevřený model.";
                return;
            }

            var parts = await _teklaService.GetPartsAsync();

            foreach (var part in parts)
                Parts.Add(part);
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Načtení modelu selhalo: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
        }
    }
}
