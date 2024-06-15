using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;

namespace Maui2048
{
    public partial class MainPage : ContentPage
    {
        const int Size = 4;
        Label[,] _tiles = new Label[Size, Size];
        int[,] _board = new int[Size, Size];
        Stack<int[,]> _history = new Stack<int[,]>();

        int _score = 0;
        int _highScore = 0;

        public MainPage()
        {
            InitializeComponent();
            InitializeGame();
            StartNewGame();
        }

        void InitializeGame()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    var label = new Label
                    {
                        BackgroundColor = Colors.LightGray,
                        TextColor = Colors.Black,
                        FontSize = 24,
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center,
                        Margin = 2
                    };

                    _tiles[i, j] = label;
                    GameGrid.Children.Add(label);
                    Grid.SetRow(label, i);
                    Grid.SetColumn(label, j);
                }
            }
        }

        void StartNewGame()
        {
            _score = 0;
            ScoreLabel.Text = $"Score: {_score}";
            Array.Clear(_board, 0, _board.Length);
            AddRandomTile();
            AddRandomTile();
            UpdateUI();
        }

        void AddRandomTile()
        {
            var emptyCells = new List<(int, int)>();
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (_board[i, j] == 0)
                    {
                        emptyCells.Add((i, j));
                    }
                }
            }

            if (emptyCells.Count > 0)
            {
                var (row, col) = emptyCells[new Random().Next(emptyCells.Count)];
                _board[row, col] = new Random().Next(10) == 0 ? 4 : 2;
            }
        }

        void UpdateUI()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    _tiles[i, j].Text = _board[i, j] == 0 ? "" : _board[i, j].ToString();
                    _tiles[i, j].BackgroundColor = GetTileColor(_board[i, j]);
                }
            }

            ScoreLabel.Text = $"Score: {_score}";
            HighScoreLabel.Text = $"High Score: {_highScore}";
        }

        Color GetTileColor(int value)
        {
            return value switch
            {
                0 => Colors.LightGray,
                2 => Colors.Beige,
                4 => Colors.Bisque,
                8 => Colors.BurlyWood,
                16 => Colors.Coral,
                32 => Colors.Orange,
                64 => Colors.OrangeRed,
                128 => Colors.Gold,
                256 => Colors.Yellow,
                512 => Colors.YellowGreen,
                1024 => Colors.LightGreen,
                2048 => Colors.Lime,
                _ => Colors.DarkGray
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.Content.GestureRecognizers.Add(new SwipeGestureRecognizer { Direction = SwipeDirection.Left, Command = new Command(OnSwipeLeft) });
            this.Content.GestureRecognizers.Add(new SwipeGestureRecognizer { Direction = SwipeDirection.Right, Command = new Command(OnSwipeRight) });
            this.Content.GestureRecognizers.Add(new SwipeGestureRecognizer { Direction = SwipeDirection.Up, Command = new Command(OnSwipeUp) });
            this.Content.GestureRecognizers.Add(new SwipeGestureRecognizer { Direction = SwipeDirection.Down, Command = new Command(OnSwipeDown) });
        }

        void OnSwipeLeft() => MoveAndMergeTiles((i, j) => (i, j), (i, j) => (i, j - 1));
        void OnSwipeRight() => MoveAndMergeTiles((i, j) => (i, j), (i, j) => (i, j + 1));
        void OnSwipeUp() => MoveAndMergeTiles((i, j) => (i, j), (i, j) => (i - 1, j));
        void OnSwipeDown() => MoveAndMergeTiles((i, j) => (i, j), (i, j) => (i + 1, j));

        void MoveAndMergeTiles(Func<int, int, (int, int)> current, Func<int, int, (int, int)> next)
        {
            _history.Push((int[,])_board.Clone());
            bool moved = false;

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    var (row, col) = current(i, j);
                    if (_board[row, col] != 0)
                    {
                        var (nextRow, nextCol) = next(row, col);

                        while (IsInBounds(nextRow, nextCol) && _board[nextRow, nextCol] == 0)
                        {
                            _board[nextRow, nextCol] = _board[row, col];
                            _board[row, col] = 0;
                            row = nextRow;
                            col = nextCol;
                            (nextRow, nextCol) = next(row, col);
                            moved = true;
                        }

                        if (IsInBounds(nextRow, nextCol) && _board[nextRow, nextCol] == _board[row, col])
                        {
                            _board[nextRow, nextCol] *= 2;
                            _board[row, col] = 0;
                            _score += _board[nextRow, nextCol];
                            if (_score > _highScore)
                            {
                                _highScore = _score;
                            }
                            moved = true;
                        }
                    }
                }
            }

            if (moved)
            {
                AddRandomTile();
                UpdateUI();
            }
        }

        bool IsInBounds(int row, int col) => row >= 0 && row < Size && col >= 0 && col < Size;

        void OnUndoClicked(object sender, EventArgs e)
        {
            if (_history.Count > 0)
            {
                _board = _history.Pop();
                UpdateUI();
            }
        }

        void OnRestartClicked(object sender, EventArgs e)
        {
            StartNewGame();
        }
    }
}
