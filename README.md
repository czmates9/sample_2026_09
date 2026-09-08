# Tekla Model Inspector

Ukázková desktopová aplikace pro načtení, zobrazení a filtrování konstrukčních prvků z modelu Tekla Structures.

Projekt vznikl jako praktické procvičení WPF, architektury MVVM, dependency injection a integrace externího engineering API.

## Funkce

- kontrola spojení s Tekla Structures
- zobrazení názvu otevřeného modelu
- načtení nosníků prostřednictvím Tekla Open API
- zobrazení prvků v tabulce
- filtrování podle ID, názvu, profilu, materiálu a typu
- indikace probíhajícího načítání
- ošetření chyb a nedostupného spojení
- mock režim pro vývoj bez instalace Tekla Structures
- výběr implementace služby pomocí konfigurace a dependency injection

## Použité technologie

- C#
- .NET 8
- WPF
- XAML
- MVVM
- CommunityToolkit.Mvvm
- Microsoft.Extensions.DependencyInjection
- Microsoft.Extensions.Configuration
- Tekla Structures Open API
- Git

## Architektura

Aplikace odděluje uživatelské rozhraní, prezentační logiku a komunikaci s externím API.

- `MainWindow` definuje uživatelské rozhraní a binding.
- `MainViewModel` obsahuje stav, příkazy a prezentační logiku.
- `ITeklaService` definuje rozhraní pro komunikaci s Tekla Structures.
- `MockTeklaService` poskytuje demonstrační data pro lokální vývoj.
- `TeklaService` používá skutečné Tekla Structures Open API.
- `TeklaPart` představuje vlastní aplikační model konstrukčního prvku.
- Dependency injection sestavuje závislosti při spuštění aplikace.

Tok závislostí:

```text
App
└── MainWindow
    └── MainViewModel
        └── ITeklaService
            ├── MockTeklaService
            └── TeklaService
```

ViewModel není závislý na konkrétní implementaci Tekla API. Díky rozhraní `ITeklaService` lze skutečnou službu nahradit mockem bez změny uživatelského rozhraní nebo prezentační logiky.

## Konfigurace

Použití mocku nebo skutečné Tekla služby se nastavuje v souboru `appsettings.json`:

```json
{
  "Tekla": {
    "UseMock": true
  }
}
```

Pro lokální spuštění bez instalace Tekla Structures musí být `UseMock` nastaveno na `true`.

V prostředí s nainstalovanou Tekla Structures lze nastavit:

```json
{
  "Tekla": {
    "UseMock": false
  }
}
```

Dependency injection potom zaregistruje `TeklaService` namísto `MockTeklaService`.

## Spuštění

1. Otevřete řešení `TeklaModelInspector/TeklaModelInspector.slnx`.
2. Obnovte NuGet balíčky.
3. Zkontrolujte nastavení v `appsettings.json`.
4. Sestavte řešení.
5. Spusťte aplikaci.

V mock režimu není instalace Tekla Structures vyžadována.

## Tekla Open API

Třída `TeklaService` používá `Tekla.Structures.Model.Model` pro komunikaci s právě spuštěnou Tekla Structures.

Služba:

1. ověří spojení pomocí `GetConnectionStatus()`,
2. získá informace o otevřeném modelu,
3. načte modelové objekty typu `BEAM`,
4. převede objekty Tekly na vlastní model `TeklaPart`,
5. předá výsledky ViewModelu.

Tím zůstává závislost na Tekla Open API uzavřená uvnitř servisní vrstvy.

## Omezení

Skutečný `TeklaService` nebyl lokálně otestován proti spuštěné instalaci Tekla Structures.

Pro reálné připojení je potřeba:

- kompatibilní instalace Tekla Structures,
- platná licence,
- otevřený model,
- odpovídající verze Tekla Open API knihoven,
- 64bitová konfigurace aplikace.

Projekt aktuálně používá balíček `Tekla.Structures.Model` verze `2026.0.3`.

## Účel projektu

Projekt slouží jako demonstrační prototyp a výuková ukázka. Není určen jako hotový produkční nástroj pro zpracování rozsáhlých konstrukčních modelů.