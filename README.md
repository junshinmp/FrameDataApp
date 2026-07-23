# FrameCraft | Fighting Game Frame Data Analyzer

A desktop application built with C# and .NET (WPF) designed to manage, query, and analyze fighting game character frame data, move properties, and matchup statistics.

FrameCraft bridges the gap between raw frame data and actionable execution analysis, providing tools to inspect startup frames, block advantages, move cancels, and game-specific character rosters.

---

## Features

* **Frame Data Query Engine:** Fast, LINQ-powered filtering for safe-on-block moves, fastest startup options, and specific move properties.
* **Character & Roster Management:** Create, update, and organize characters across different fighting game titles.
* **Move Property Inspector:** Detailed metric tracking including Startup, Active, Recovery, OnBlock advantage, and Cancel types (e.g., Super Cancelable).
* **MVVM Architecture:** Clean separation of business logic, state management, and user interfaces for maintainability and scalability.

---

## Built With

* **Language:** C#
* **Framework:** .NET / WPF (Windows Presentation Foundation)
* **Architecture Pattern:** MVVM (Model-View-ViewModel)
* **Query Language:** LINQ (Language Integrated Query)

---

## Project Structure

```text
FrameDataApp/
├── Models/
│   ├── CancelType.cs            # Move cancel properties (e.g., Super Cancelable)
│   ├── Character.cs             # Character metadata, speeds, and move lists
│   ├── FrameData.cs             # Frame metrics (Startup, Active, Recovery, OnBlock)
│   ├── Game.cs                  # Fighting game title, developer, and roster lists
│   └── Move.cs                  # Move name, command input, stats, and properties
├── Services/
│   └── FrameDataService.cs      # Business logic for querying, filtering, and seeding data
├── ViewModel/
│   ├── BaseViewModel.cs         # Base class implementing INotifyPropertyChanged
│   ├── FrameDataViewModel.cs    # ViewModel for frame data viewing and filtering
│   ├── MainViewModel.cs         # Main UI state and navigation coordinator
│   └── MakeCharacterViewModel.cs # ViewModel for character and move creation forms
└── Views/
    ├── MakeCharacterView.xaml   # Character and move data entry view
    └── MainWindow.xaml          # Primary application window layout
```

## Getting Started

### Prerequisites

* **OS:** Windows 10/11
* **IDE:** Visual Studio 2022 (or higher) with the **.NET Desktop Development** workload installed
* **SDK:** .NET 6.0 SDK or higher

### Installation & Setup

1. **Clone the Repository:**
   ```bash
   git clone [https://github.com/your-username/FrameDataApp.git](https://github.com/your-username/FrameDataApp.git)
   cd FrameDataApp

2. Open the Solution:
3. Open FrameDataApp.sln in Visual Studio.

Build & Run:
*Press F5 or click Start in Visual Studio to compile and run the application.
---

  *Roadmap

   * [ ] JSON serialization for loading and saving custom character frame data files.
   * [ ] Visual frame timeline component showing Startup vs. Active vs. Recovery phases.
   * [ ] Side-by-side character matchup comparison screen.

  *UML Diagram
  ```mermaid
classDiagram
    %% --- BASE & INFRASTRUCTURE ---
    class BaseViewModel {
        <<Abstract>>
        +event PropertyChangedEventHandler PropertyChanged
        #OnPropertyChanged(propertyName: string)
        #SetProperty~T~(storage: ref T, value: T, propertyName: string) bool
    }

    class RelayCommand {
        -Action~object~ _execute
        -Predicate~object~ _canExecute
        +CanExecute(parameter: object) bool
        +Execute(parameter: object)
    }

    %% --- MODELS ---
    class Game {
        +string Title
        +List~Character~ Characters
    }

    class Character {
        +string Name
        +double WalkSpeed
        +double DashSpeed
        +List~Move~ Moves
    }

    class Move {
        +string MoveName
        +string CommandInput
        +FrameData Stats
        +CancelType Cancelable
    }

    class FrameData {
        +int StartUp
        +int Active
        +int Recovery
        +int OnBlock
        +int OnHit
    }

    class CancelType {
        <<Enumeration>>
        Special
        Super
        Chain
        None
    }

    class Combo {
        <<Future>>
        +string ComboName
        +Character Character
        +List~Move~ MoveSequence
        +int TotalDamage
        +bool IsValidSequence()
    }

    %% --- SERVICES ---
    class FrameDataService {
        -List~Game~ _games
        +GetCharacters() List~Character~
        +GetMovesForCharacter(charName: string) List~Move~
        +AddCharacter(name: string, walk: double, dash: double) bool
        +AddMoveToCharacter(charName: string, move: Move) bool
        +ValidateCombo(moves: List~Move~) bool [Future]
    }

    %% --- VIEWMODELS ---
    class MainViewModel {
        +BaseViewModel CurrentView
        +ICommand NavigateMakeCharacterCommand
        +ICommand NavigateFrameDataCommand
        +ICommand NavigateComboMakerCommand [Future]
    }

    class FrameDataViewModel {
        -FrameDataService _service
        +ObservableCollection~string~ CharacterNames
        +ObservableCollection~Move~ DisplayMoves
        +string SelectedCharacter
        +string SearchQuery
        +ICommand FilterMovesCommand
    }

    class MakeCharacterViewModel {
        -FrameDataService _service
        +string Name
        +double WalkSpeed
        +double DashSpeed
        +ICommand SubmitCommand
    }

    class MakeMoveViewModel {
        <<Future>>
        -FrameDataService _service
        +string SelectedCharacter
        +string MoveName
        +string CommandInput
        +int StartUp
        +int Active
        +int Recovery
        +int OnBlock
        +int OnHit
        +CancelType SelectedCancelType
        +ICommand SaveMoveCommand
    }

    class ComboMakerViewModel {
        <<Future>>
        -FrameDataService _service
        +Character SelectedCharacter
        +ObservableCollection~Move~ AvailableMoves
        +ObservableCollection~Move~ CurrentComboSequence
        +int TotalDamage
        +ICommand AddMoveToComboCommand
        +ICommand RemoveMoveFromComboCommand
        +ICommand SaveComboCommand
    }

    %% --- VIEWS ---
    class MainWindow {
        <<View>>
    }

    class MakeCharacterView {
        <<View>>
    }

    class FrameDataView {
        <<Future View>>
    }

    class MakeMoveView {
        <<Future View>>
    }

    class ComboMakerView {
        <<Future View>>
    }

    %% --- RELATIONSHIPS ---
    BaseViewModel <|-- MainViewModel
    BaseViewModel <|-- FrameDataViewModel
    BaseViewModel <|-- MakeCharacterViewModel
    BaseViewModel <|-- MakeMoveViewModel
    BaseViewModel <|-- ComboMakerViewModel

    Game "1" *-- "many" Character
    Character "1" *-- "many" Move
    Move "1" *-- "1" FrameData
    Move "1" *-- "1" CancelType
    Combo "1" o-- "many" Move

    MainViewModel o-- BaseViewModel : Controls Current Active View
    FrameDataViewModel --> FrameDataService
    MakeCharacterViewModel --> FrameDataService
    MakeMoveViewModel --> FrameDataService
    ComboMakerViewModel --> FrameDataService

    MainWindow ..> MainViewModel : DataContext
    MakeCharacterView ..> MakeCharacterViewModel : DataContext
    FrameDataView ..> FrameDataViewModel : DataContext
    MakeMoveView ..> MakeMoveViewModel : DataContext
    ComboMakerView ..> ComboMakerViewModel : DataContext
  ```
