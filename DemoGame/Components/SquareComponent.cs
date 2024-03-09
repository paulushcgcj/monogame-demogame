using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DemoGame;

/// <summary>
/// Represents a square component that can be drawn on the screen.
/// </summary>
public class SquareComponent
{
  private Texture2D _squareTexture;
  public int SquareWidth {get; private set;}
  public int SquareHeight {get; private set;}
  public int SquareBorder {get; private set;}

  /// <summary>
  /// Represents a square component that can be rendered on the screen.
  /// </summary>
  /// <param name="texture">The texture to use for rendering the square.</param>
  /// <param name="width">The width of the square.</param>
  /// <param name="height">The height of the square.</param>
  /// <param name="border">The thickness of the square's border.</param>
  public SquareComponent(
    Texture2D texture,
    int width = 100,
    int height = 100,
    int border = 3
    )
  {
    _squareTexture = texture;
    SquareWidth = width;
    SquareHeight = height;
    SquareBorder = border;
  }

  /// <summary>
  /// Draws a square on the screen.
  /// </summary>
  /// <param name="spriteBatch">The sprite batch used for drawing.</param>
  /// <param name="position">The position of the square.</param>
  /// <param name="color">The color of the square.</param>
  public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color)
  {
    spriteBatch.Draw(
      _squareTexture, 
      new Rectangle(
        (((int)position.X) * SquareWidth) + SquareWidth, 
        (((int)position.Y) * SquareHeight) + SquareHeight, 
        SquareWidth, 
        SquareHeight
      ),
      color
    );

    spriteBatch.Draw(
      _squareTexture, 
      new Rectangle(
        (((int)position.X) * SquareWidth) + SquareWidth + SquareBorder, 
        (((int)position.Y) * SquareHeight) + SquareHeight + SquareBorder, 
        SquareWidth - (SquareBorder * 2), 
        SquareHeight - (SquareBorder * 2)
      ),
      Color.CornflowerBlue
    );
  }
}