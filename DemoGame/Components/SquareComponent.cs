using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DemoGame;

/// <summary>
/// Represents a square component that can be drawn on the screen.
/// </summary>
public class SquareComponent
{
  public string Name { get; private set; }
  public int SquareWidth { get; private set; }
  public int SquareHeight { get; private set; }
  public int SquareBorder { get; private set; }
  public Rectangle Position { get; private set; }
  public bool IsClicked { get { return _squareColor == _clickColor; } }
  private Texture2D _squareTexture;
  private Rectangle _borderRectangle;
  private Color _squareColor = Color.Black;
  private Color _clickColor = Color.Red;

  /// <summary>
  /// Represents a square component that can be rendered on the screen.
  /// </summary>
  /// <param name="texture">The texture to use for rendering the square.</param>
  /// <param name="width">The width of the square.</param>
  /// <param name="height">The height of the square.</param>
  /// <param name="border">The thickness of the square's border.</param>
  /// <param name="position">The position of the square on the screen.</param>
  /// <returns>A new instance of the SquareComponent class.</returns>
  /// <exception cref="ArgumentNullException">Thrown when the texture is null.</exception>
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

    _borderRectangle = new Rectangle(
      (((int)position.X) * SquareWidth) + SquareWidth + SquareBorder,
      (((int)position.Y) * SquareHeight) + SquareHeight + SquareBorder,
      SquareWidth - (SquareBorder * 2),
      SquareHeight - (SquareBorder * 2)
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

  /// <summary>
  /// Draws a square on the screen.
  /// </summary>
  /// <param name="spriteBatch">The sprite batch used for drawing.</param>
  public void Draw(SpriteBatch spriteBatch)
  {
    // Draw the square that will be used as border
    spriteBatch.Draw(_squareTexture, Position, _squareColor);
    // Draw the middle square to fake a transparency effect
    spriteBatch.Draw(_squareTexture, _borderRectangle, Color.CornflowerBlue);
  }
}