# 2048 Game

This is a 2048 game I implemented using the .NET MAUI framework. The game allows you to play the classic 2048 game on multiple platforms such as Android, iOS, Mac Catalyst, and Windows.

## Features

- Classic 2048 gameplay
- Swipe to move tiles
- Undo last move
- Track score and high score
- Restart the game

## Screenshots
![Screenshot 2024-06-15 221437](https://github.com/Brainydaps/2048-game/assets/41041115/5bd604e6-2242-4e7e-a7c7-63a4e9c49ccd)


## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- A development environment with support for .NET MAUI (Visual Studio 2022 recommended)

### Running the Game

1. Clone the repository:

    ```sh
    git clone https://github.com/Brainydaps/Maui2048.git
    ```

2. Open the solution in Visual Studio 2022.

3. Restore the project dependencies:

    ```sh
    dotnet restore
    ```

4. Build and run the project on your preferred platform (Android, iOS, Mac Catalyst, or Windows).

## How to Play

- Swipe (or use arrow keys) to move the tiles.
- When two tiles with the same number touch, they merge into one.
- The goal is to create a tile with the number 2048.

### Controls

- **Swipe/Arrow Keys**: Move tiles in the corresponding direction.
- **Undo Button**: Revert to the previous move.
- **Restart Button**: Start a new game.

## Project Structure

- `MainPage.xaml` and `MainPage.xaml.cs`: Contains the UI and game logic.
- `GameGrid`: The grid layout for the game tiles.
- `ScoreLabel` and `HighScoreLabel`: Displays the current score and high score.
- `Undo` and `Restart` Buttons: Provides functionality to undo the last move and restart the game.

## Contributing

Contributions are welcome! Please fork this repository and submit pull requests.

## License

This project is licensed under the CC by NC 4.0 License. See the [LICENSE](LICENSE) file for details.

## Contact

Brainydaps - [GitHub](https://github.com/Brainydaps)

