using Microsoft.Xna.Framework.Graphics;
using NUnit.Framework;
using Moq;
using DemoGame;
using Microsoft.Xna.Framework;

[TestFixture]
public class SquareComponentTests
{
    [Test]
    public void DrawWhiteSquare_DrawsSquareAtCorrectPosition()
    {
        // Arrange
        var spriteBatch = new Mock<SpriteBatch>();
        var squareTexture = new Mock<Texture2D>();
        var squareComponent = new SquareComponent(squareTexture.Object);
        var position = new Vector2(10, 10);

        // Act
        squareComponent.Draw(spriteBatch.Object, position, Color.White);

        // Assert
        spriteBatch.Verify(sb => sb.Draw(squareTexture.Object, 
            new Rectangle(
                (((int)position.X) * squareComponent.SquareWidth) + squareComponent.SquareWidth, 
                (((int)position.Y) * squareComponent.SquareHeight) + squareComponent.SquareHeight, 
                squareComponent.SquareWidth, 
                squareComponent.SquareHeight
            ), 
            Color.White), Times.Once);
    }

    [Test]
    public void DrawWhiteSquare_DrawsSquareWithCorrectBorder()
    {
        // Arrange
        var spriteBatch = new Mock<SpriteBatch>();
        var squareTexture = new Mock<Texture2D>();
        var squareComponent = new SquareComponent(squareTexture.Object);
        var position = new Vector2(10, 10);
        var expectedBorder = squareComponent.SquareBorder;

        // Act
        squareComponent.Draw(spriteBatch.Object, position, Color.White);

        // Assert
        spriteBatch.Verify(sb => sb.Draw(squareTexture.Object, 
            new Rectangle(
                (((int)position.X) * squareComponent.SquareWidth) + squareComponent.SquareWidth + expectedBorder, 
                (((int)position.Y) * squareComponent.SquareHeight) + squareComponent.SquareHeight + expectedBorder, 
                squareComponent.SquareWidth - (expectedBorder * 2), 
                squareComponent.SquareHeight - (expectedBorder * 2)
            ), 
            Color.CornflowerBlue), Times.Once);
    }
}