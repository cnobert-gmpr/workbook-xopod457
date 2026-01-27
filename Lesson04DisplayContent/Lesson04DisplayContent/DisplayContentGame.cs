using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lesson04DisplayContent;

public class DisplayContentGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D _ship, _spaceStation;

    private SpriteFont _font;
    private string _message = "In 2.5 weeks, the days will be 1 hour longer.";

    public DisplayContentGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        _graphics.PreferredBackBufferWidth = 640;
        _graphics.PreferredBackBufferHeight = 320;
        _graphics.ApplyChanges();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _spaceStation = Content.Load<Texture2D>("Station");
        _ship = Content.Load<Texture2D>("Beetle");

        _font = Content.Load<SpriteFont>("SystemArialFont");

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

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        _spriteBatch.Draw(_spaceStation,  Vector2.Zero, Color.White);
        _spriteBatch.Draw(_ship, new Vector2(300, 140), Color.White);

        _spriteBatch.DrawString(_font, _message, new Vector2(20, 20), Color.White);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
