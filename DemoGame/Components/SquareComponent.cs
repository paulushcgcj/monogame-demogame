using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DemoGame;

public class SquareComponent
{
  public string Name { get; private set; }
  public int SquareWidth { get; private set; }
  public int SquareHeight { get; private set; }
  public int SquareBorder { get; private set; }
  public Rectangle Position { get; private set; }
  public bool IsClicked { get { return _squareColor == _clickColor; } }
  private Texture2D _squareTexture;
  private Color _squareColor = Color.Black;
  private Color _clickColor = Color.Red;


  public SquareComponent(
    Texture2D texture,
    Vector2 position,
    int width = 100,
    int height = 100,
    int border = 3
    )
  {
    _squareTexture = texture;
    SquareWidth = width;
    SquareHeight = height;
    SquareBorder = border;

    Name = $"Square at {position.X}, {position.Y}";

    Position = new Rectangle(
        (((int)position.X) * SquareWidth) + SquareWidth,
        (((int)position.Y) * SquareHeight) + SquareHeight,
        SquareWidth,
        SquareHeight
      );
  }

  public void Clicked(Color color)
  {
    _squareColor = color;
  }

  public void Reset()
  {
    _squareColor = Color.Black;
  }

  public Vector2 GetBoardPosition()
  {
    return new Vector2(Position.X / SquareWidth-1, Position.Y / SquareHeight-1);
  }

  public void Draw(SpriteBatch spriteBatch)
  {
    // Draw the square that will be used as border
    spriteBatch.Draw(_squareTexture, Position, _squareColor);
  }
}