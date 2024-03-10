using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DemoGame;

public class Game1 : Game
{

    private enum GameState
    {
        Playing,
        Draw,
        XWon,
        OWon,
        Won
    }

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private List<SquareComponent> _squares = new();
    private SpriteFont _font;
    private bool _xPlayer = true;
    private MouseState _previousMouseState;
    // Game board
    private int[,] _gameBoard = new int[3, 3];
    // Game over flag
    private bool _gameOver = false;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _graphics.PreferredBackBufferWidth = 64 * 5;  // set this value to the desired width
        _graphics.PreferredBackBufferHeight = 64 * 5; // set this value to the desired height
        _graphics.ApplyChanges();
    }

    protected override void Initialize()
    {

        base.Initialize();

        int _squareWidth = 64;
        int _squareHeight = 64;

        Texture2D _squareTexture = Content.Load<Texture2D>("Square");

        for (int column = 0; column < 3; column++)
        {
            for (int row = 0; row < 3; row++)
            {
                _squares.Add(new SquareComponent(_squareTexture, new Vector2(column, row), _squareWidth, _squareHeight));
            }
        }

        _previousMouseState = Mouse.GetState();

    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _font = Content.Load<SpriteFont>("Base");

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // Get the current state of the mouse
        MouseState mouseState = Mouse.GetState();

        if (!_gameOver)
        {

            if (_previousMouseState.LeftButton == ButtonState.Released && mouseState.LeftButton == ButtonState.Pressed)
            {

                // Check if the left button of the mouse is pressed
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    // Get the position of the mouse click
                    Point mousePosition = mouseState.Position;

                    // Iterate over the list of squares
                    foreach (SquareComponent square in _squares)
                    {
                        // Check if the mouse click occurred within the square
                        if (mousePosition.X >= square.Position.X && mousePosition.X <= square.Position.X + square.Position.Width &&
                            mousePosition.Y >= square.Position.Y && mousePosition.Y <= square.Position.Y + square.Position.Height &&
                            !square.IsClicked
                            )
                        {

                            // Mouse click occurred within the square
                            Console.WriteLine("Mouse clicked within square: " + square.Name + " at position: " + square.GetBoardPosition());
                            square.Clicked(_xPlayer ? Color.Red : Color.Yellow);

                            // Mark the square with the current player's symbol if it's not already marked
                            if (_gameBoard[(int)square.GetBoardPosition().X, (int)square.GetBoardPosition().Y] == 0)
                            {
                                _gameBoard[(int)square.GetBoardPosition().X, (int)square.GetBoardPosition().Y] = _xPlayer ? 1 : 2;

                                var gameState = CheckWin(_xPlayer ? 1 : 2);
                                if (gameState == GameState.Playing)
                                {
                                    _xPlayer = !_xPlayer;
                                }
                                else if (gameState == GameState.Draw)
                                {
                                    _gameOver = true;
                                }
                                else
                                {
                                    _gameOver = true;
                                }
                            }

                            break;
                        }
                    }
                }
            }

            _previousMouseState = mouseState;

        }
        else
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                _gameOver = false;
                _squares.ForEach(square => square.Reset());
                _xPlayer = true;
                _gameBoard = new int[3, 3];
            }
        }

        base.Update(gameTime);
    }

    private GameState CheckWin(int player)
    {
        for (int i = 0; i < 3; i++)
        {
            if ((_gameBoard[i, 0] == player && _gameBoard[i, 1] == player && _gameBoard[i, 2] == player) ||
                (_gameBoard[0, i] == player && _gameBoard[1, i] == player && _gameBoard[2, i] == player))
            {
                return player == 1 ? GameState.XWon : GameState.OWon;
            }
        }

        if ((_gameBoard[0, 0] == player && _gameBoard[1, 1] == player && _gameBoard[2, 2] == player) ||
            (_gameBoard[0, 2] == player && _gameBoard[1, 1] == player && _gameBoard[2, 0] == player))
        {
            return player == 1 ? GameState.XWon : GameState.OWon;
        }

        bool isBoardFull = true;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (_gameBoard[i, j] == 0)
                {
                    isBoardFull = false;
                    break;
                }
            }
        }

        if (isBoardFull)
        {
            return GameState.Draw;
        }

        return GameState.Playing;
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        string text = $"{(_xPlayer ? 'X' : 'O')} player turn";

        if (CheckWin(_xPlayer ? 1 : 2) == GameState.Draw)
            text = "Draw";
        else if (CheckWin(_xPlayer ? 1 : 2) == GameState.XWon || CheckWin(_xPlayer ? 1 : 2) == GameState.OWon)
            text = $"{(_xPlayer ? 'X' : 'O')} Won";
        else
            text =$"{(_xPlayer ? 'X' : 'O')} player turn";;

        Vector2 textMiddlePoint = _font.MeasureString(text) / 2;

        _spriteBatch.DrawString(
            _font,
            text,
            new Vector2(Window.ClientBounds.Width / 2, 30),
            Color.Black,
            0,
            textMiddlePoint,
            1.0f,
            SpriteEffects.None,
            0.5f
            );


        _squares.ForEach(square => square.Draw(_spriteBatch));
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
