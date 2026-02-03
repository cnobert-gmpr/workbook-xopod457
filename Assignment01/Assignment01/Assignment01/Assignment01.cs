using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Assignment01;

public class Assignment01 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _spike, _background;
    private SimpleAnimation _walkingAnimation;
    private Vector2 _linkPosition;
    private Vector2 _linkVelocity;
    private Vector2 _spikePosition;
    private const float SpikeSpeed = 200f;
    private SpriteFont _font;
    private SimpleAnimation _Attack;

    public Assignment01()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        _graphics.PreferredBackBufferWidth = 1920;
        _graphics.PreferredBackBufferHeight = 1200; 
        _graphics.ApplyChanges();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        _spike = Content.Load<Texture2D>("Spike");
        _background = Content.Load<Texture2D>("Background");
        _walkingAnimation = new SimpleAnimation(Content.Load<Texture2D>("Link"), 105, 111, 10, 10);
        _Attack = new SimpleAnimation(Content.Load<Texture2D>("MonsterAttack"), 32, 32, 6, 6);
        _font = Content.Load<SpriteFont>("SystemArialFont");

        _linkPosition = new Vector2(100, 100);
        _linkVelocity = new Vector2(200f, 0f);

        _spikePosition = new Vector2(300, 100);

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        _walkingAnimation.Update(gameTime);
        _Attack.Update(gameTime);

        _linkPosition += _linkVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;


       
        var ks = Keyboard.GetState();
        Vector2 spikeMove = Vector2.Zero;
        if (ks.IsKeyDown(Keys.Left) || ks.IsKeyDown(Keys.A)) spikeMove.X -= 1f;
        if (ks.IsKeyDown(Keys.Right) || ks.IsKeyDown(Keys.D)) spikeMove.X += 1f;
        if (ks.IsKeyDown(Keys.Up) || ks.IsKeyDown(Keys.W)) spikeMove.Y -= 1f;
        if (ks.IsKeyDown(Keys.Down) || ks.IsKeyDown(Keys.S)) spikeMove.Y += 1f;

        if (spikeMove != Vector2.Zero)
        {
            spikeMove.Normalize();
            _spikePosition += spikeMove * SpikeSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

    
      

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        _spriteBatch.Draw(_background, Vector2.Zero, Color.White);
        _spriteBatch.Draw(_spike, _spikePosition, Color.White);
        _walkingAnimation.Draw(_spriteBatch, _linkPosition, SpriteEffects.None);
        _Attack.Draw(_spriteBatch, new Vector2(800, 300), SpriteEffects.None);
        _spriteBatch.DrawString(_font, "Hello im learing stuff!", new Vector2(10, 10), Color.Yellow);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
