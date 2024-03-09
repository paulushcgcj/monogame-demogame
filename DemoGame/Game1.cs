using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DemoGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;


    private Texture2D _squareTexture;
    private int _squareWidth = 100;
    private int _squareHeight = 100;

    private SquareComponent _square;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {        

        base.Initialize();

        _squareTexture = new Texture2D(GraphicsDevice, _squareWidth, _squareHeight);
        Color[] data = new Color[_squareWidth * _squareHeight];
        for (int i = 0; i < data.Length; ++i) data[i] = Color.White;
        _squareTexture.SetData(data);

        _square = new SquareComponent(_squareTexture, _squareWidth, _squareHeight);

    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here

    
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);


        _spriteBatch.Begin();

        for(int column = 0; column < 3; column++)
        {
            for(int row = 0; row < 3; row++)
            {                
                _square.Draw(_spriteBatch, new Vector2(column, row),Color.Black);
            }
        }

        _spriteBatch.End();


        base.Draw(gameTime);
    }
}
