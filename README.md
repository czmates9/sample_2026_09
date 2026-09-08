# Tekla Model Inspector

Ukázková WPF aplikace pro načtení a zobrazení konstrukčních prvků
z otevřeného modelu Tekla Structures.

Projekt vznikl jako praktické procvičení WPF, MVVM a integrace
externího engineering API.

## Funkce

- kontrola připojení k Tekla Structures
- zobrazení názvu otevřeného modelu
- načtení nosníků prostřednictvím Tekla Open API
- zobrazení prvků v DataGridu
- filtrování podle ID, názvu, profilu, materiálu a typu
- stav načítání a ošetření chyb
- mock režim pro vývoj bez instalace Tekla Structures

## Použité technologie

- C#
- .NET 8
- WPF a XAML
- MVVM
- CommunityToolkit.Mvvm
- Microsoft.Extensions.DependencyInjection
- Microsoft.Extensions.Configuration
- Tekla Structures Open API
- Git

## Architektura

Aplikace odděluje uživatelské rozhraní, prezentační logiku
a komunikaci s Tekla Structures.

- `MainWindow` obsahuje uživatelské rozhraní.
- `MainViewModel` obsahuje stav a příkazy aplikace.
- `ITeklaService` definuje rozhraní pro komunikaci s Teklou.
- `MockTeklaService` poskytuje demonstrační data.
- `TeklaService` používá skutečné Tekla Structures Open API.
- Dependency injection vybírá konkrétní implementaci služby.

## Konfigurace

Použití mock nebo skutečné Tekla služby se nastavuje
v souboru `appsettings.json`:

```json
{
  "Tekla": {
    "UseMock": true
  }
}