using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lesson07Pong;

public class Pong : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private const int _WindowWidth = 750, _WindowHeight = 450, _BallWidthandHeight = 21, _PlayAreaEdgeWidth = 12;
    private const int _PaddleWidth = 8, _PaddleHeight = 124;
    private const float _PaddleSpeed = 240, _BallSpeed = 60;
    private Texture2D _backgroundTexture, _ballTexture, _paddleTexure;
    private Vector2 _ballPosition, _ballDirection;
    private float _ballSpeed;
    
    private Vector2 _paddlePosition, _paddleDirection, _paddleDimensions;
    private Vector2 _paddleLPosition, _paddleLDirection, _paddleLDimensions;
    private float _paddleSpeed;

    internal Rectangle PlayAreaBoundingBox

    {
        get => new Rectangle(0, _PlayAreaEdgeWidth, _WindowWidth, _WindowHeight - 2 * _PlayAreaEdgeWidth);
    }

    public Pong()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = _WindowWidth;
        _graphics.PreferredBackBufferHeight = _WindowHeight;
        _graphics.ApplyChanges();

        _ballPosition.X = 150;
        _ballPosition.Y = 195;

        _ballSpeed = 60;
        _ballDirection = new Vector2(-1, -1);

        _paddlePosition = new Vector2(690, 198);
        _paddleSpeed = _PaddleSpeed;
        _paddleDimensions = new Vector2(_PaddleWidth, _PaddleHeight);
        _paddleDirection = Vector2.Zero;

        _paddleLPosition = new Vector2(52, 198);
        _paddleSpeed = _PaddleSpeed;
        _paddleLDimensions = new Vector2(_PaddleWidth, _PaddleHeight);
        _paddleLDirection = Vector2.Zero;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _backgroundTexture = Content.Load<Texture2D>("Court");
        _ballTexture = Content.Load<Texture2D>("Ball");
        _paddleTexure = Content.Load<Texture2D>("Paddle");
    }

    protected override void Update(GameTime gameTime)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        #region ball movement
        _ballPosition += _ballDirection * _ballSpeed * dt;

        if(_ballPosition.X <= PlayAreaBoundingBox.Left || _ballPosition.X + _BallWidthandHeight >= PlayAreaBoundingBox.Right)
        {
            _ballDirection.X *= -1;
        }
        if (_ballPosition.Y  <= PlayAreaBoundingBox.Top || _ballPosition.Y + _BallWidthandHeight >= PlayAreaBoundingBox.Bottom)
        {
            _ballDirection.Y *= -1;
        }
        #endregion 

        KeyboardState kbState = Keyboard.GetState();

        #region right paddle
        if(kbState.IsKeyDown(Keys.Up))
        {
            _paddleDirection = new Vector2(0, -1);
        }
        else if (kbState.IsKeyDown(Keys.Down))
        {
            _paddleDirection = new Vector2(0, 1);
        }
        else
        {
            _paddleDirection = Vector2.Zero;
        }
        _paddlePosition += _paddleDirection * _paddleSpeed * dt;

        if (_paddlePosition.Y <= PlayAreaBoundingBox.Top)
        {
            _paddlePosition.Y = PlayAreaBoundingBox.Top;
        }


        if ((_paddlePosition.Y + _paddleDimensions.Y) >= PlayAreaBoundingBox.Bottom)
        {
            _paddlePosition.Y = PlayAreaBoundingBox.Bottom - _paddleDimensions.Y;
        }
        #endregion

        #region Left paddle
        if(kbState.IsKeyDown(Keys.W))
        {
            _paddleLDirection = new Vector2(0, -1);
        }
        else if (kbState.IsKeyDown(Keys.S))
        {
            _paddleLDirection = new Vector2(0, 1);
        }
        else
        {
            _paddleLDirection = Vector2.Zero;
        }
        _paddleLPosition += _paddleLDirection * _paddleSpeed * dt;

        if (_paddleLPosition.Y <= PlayAreaBoundingBox.Top)
        {
            _paddleLPosition.Y = PlayAreaBoundingBox.Top;
        }


        if ((_paddleLPosition.Y + _paddleLDimensions.Y) >= PlayAreaBoundingBox.Bottom)
        {
            _paddleLPosition.Y = PlayAreaBoundingBox.Bottom - _paddleLDimensions.Y;
        }
        #endregion

        base.Update(gameTime);
    }
    
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        _spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0, _WindowWidth, _WindowHeight), Color.White);

        Rectangle ballRectangle = new Rectangle((int)_ballPosition.X, (int)_ballPosition.Y, _BallWidthandHeight, _BallWidthandHeight);
        _spriteBatch.Draw(_ballTexture, ballRectangle, Color.White);

        Rectangle paddleRectangle = new Rectangle((int) _paddlePosition.X, (int) _paddlePosition.Y, (int) _paddleDimensions.X, (int) _paddleDimensions.Y);
        _spriteBatch.Draw(_paddleTexure, paddleRectangle, Color.MonoGameOrange);

        Rectangle paddleRectangleL = new Rectangle((int) _paddleLPosition.X, (int) _paddleLPosition.Y, (int) _paddleLDimensions.X, (int) _paddleLDimensions.Y);
        _spriteBatch.Draw(_paddleTexure, paddleRectangleL, Color.DarkBlue);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}