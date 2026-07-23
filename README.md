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

---

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
   
Open the Solution:
Open FrameDataApp.sln in Visual Studio.

Build & Run:
Press F5 or click Start in Visual Studio to compile and run the application.
---

  Roadmap

    [ ] JSON serialization for loading and saving custom character frame data files.
    [ ] Visual frame timeline component showing Startup vs. Active vs. Recovery phases.
    [ ] Side-by-side character matchup comparison screen.
