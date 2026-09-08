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
using System.ComponentModel;
using System.Windows.Data;

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
    [ObservableProperty]
    private string searchText = string.Empty;

    public ICollectionView PartsView { get; }

    public ObservableCollection<TeklaPart> Parts { get; } = new();

    public MainViewModel(ITeklaService teklaService)
    {
        _teklaService = teklaService;

        PartsView = CollectionViewSource.GetDefaultView(Parts);
        PartsView.Filter = FilterPart;

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
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    partial void OnSearchTextChanged(string value)
    {
        PartsView.Refresh();
    }

    private bool FilterPart(object item)
    {
        if (item is not TeklaPart part)
            return false;

        if (string.IsNullOrWhiteSpace(SearchText))
            return true;

        return part.Id.ToString().Contains(
                   SearchText,
                   StringComparison.OrdinalIgnoreCase)
               || part.Name.Contains(
                   SearchText,
                   StringComparison.OrdinalIgnoreCase)
               || part.Profile.Contains(
                   SearchText,
                   StringComparison.OrdinalIgnoreCase)
               || part.Material.Contains(
                   SearchText,
                   StringComparison.OrdinalIgnoreCase)
               || part.ObjectType.Contains(
                   SearchText,
                   StringComparison.OrdinalIgnoreCase);
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
