using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DemoGame;

public class Game1 : Game
{
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
    }

    protected override void Initialize()
    {

        base.Initialize();

        int _squareWidth = 100;
        int _squareHeight = 100;

        Texture2D _squareTexture = new Texture2D(GraphicsDevice, _squareWidth, _squareHeight);
        Color[] data = new Color[_squareWidth * _squareHeight];
        for (int i = 0; i < data.Length; ++i) data[i] = Color.White;
        _squareTexture.SetData(data);


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
                            square.Clicked();
                            


                            // Mark the square with the current player's symbol if it's not already marked
                            if (_gameBoard[(int)square.GetBoardPosition().X, (int)square.GetBoardPosition().Y] == 0)
                            {
                                _gameBoard[(int)square.GetBoardPosition().X, (int)square.GetBoardPosition().Y] = _xPlayer ? 1 : 2;

                                // Check if the current player has won
                                if (CheckWin(_xPlayer ? 1 : 2))
                                {
                                    _gameOver = true;
                                }
                                else
                                {
                                   _xPlayer = !_xPlayer;
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

    private bool CheckWin(int player)
    {
        for (int i = 0; i < 3; i++)
        {
            if ((_gameBoard[i, 0] == player && _gameBoard[i, 1] == player && _gameBoard[i, 2] == player) ||
                (_gameBoard[0, i] == player && _gameBoard[1, i] == player && _gameBoard[2, i] == player))
            {
                return true;
            }
        }

        if ((_gameBoard[0, 0] == player && _gameBoard[1, 1] == player && _gameBoard[2, 2] == player) ||
            (_gameBoard[0, 2] == player && _gameBoard[1, 1] == player && _gameBoard[2, 0] == player))
        {
            return true;
        }

        return false;
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        if(!_gameOver)
            _spriteBatch.DrawString(_font, $"{(_xPlayer ? 'X' : 'O')} player turn", new Vector2(10, 10), Color.Black);
        else
            _spriteBatch.DrawString(_font, $"Player {(_xPlayer ? 'X' : 'O')} Won", new Vector2(10, 10), Color.Black);
        
        _squares.ForEach(square => square.Draw(_spriteBatch));
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
