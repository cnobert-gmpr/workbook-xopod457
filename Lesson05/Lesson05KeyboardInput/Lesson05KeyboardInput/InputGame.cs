using System.Net.Security;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lesson05KeyboardInput;

public class InputGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private string _message = "Insert Text Here";
    private SpriteFont _font;

    private KeyboardState _kbCurrentState, _kbPreviousState;

    

    

    public InputGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        


        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _font = Content.Load<SpriteFont>("SystemArialFont");

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        _kbCurrentState = Keyboard.GetState();
        _message = "";

        #region Arrow Keys
        if(_kbCurrentState.IsKeyDown(Keys.Up))
        {
            _message += "Up ";
        }
        if(_kbCurrentState.IsKeyDown(Keys.Down))
        {
            _message += "Down ";
        }
        if(_kbCurrentState.IsKeyDown(Keys.Left))
        {
            _message += "Left ";
        }
        if(_kbCurrentState.IsKeyDown(Keys.Right))
        {
            _message += "Right ";
        }

        #endregion

        //if (_kbPreviousState.IsKeyUp(Keys.Space) && _kbCurrentState.IsKeyDown(Keys.Space))
        if(IsNewKeyPress(Keys.Space))
        {
            _message += "\n";
            _message += "Space Bar is pressed \n";
            _message += "___________________________________________\n";
            _message += "___________________________________________\n";
            _message += "___________________________________________\n";
            _message += "___________________________________________\n";
            _message += "___________________________________________\n";
            _message += "___________________________________________\n";
            _message += "___________________________________________\n";
            _message += "___________________________________________\n";
            _message += "___________________________________________\n";
            _message += "___________________________________________\n";
            _message += "___________________________________________\n";
            _message += "___________________________________________\n";

        }
        else if (_kbCurrentState.IsKeyDown(Keys.Space))
        {
            _message += "\n";
            _message += "Space Bar is held down";
        }

        else if(_kbPreviousState.IsKeyDown(Keys.Space))
        {
            _message += "\n";
            _message += "***************************** \n";
            _message += "***************************** \n";
            _message += "***************************** \n";
            _message += "***************************** \n";
            _message += "***************************** \n";
            _message += "***************************** \n";
            _message += "***************************** \n";
            _message += "***************************** \n";
        }



        _kbPreviousState = _kbCurrentState;
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        _spriteBatch.DrawString(_font, _message, new Vector2(200, 200), Color.Gray);
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private bool IsNewKeyPress(Keys key)
    {
        bool returnValue = _kbPreviousState.IsKeyUp(key) && _kbCurrentState.IsKeyDown(key);
        return returnValue;
    }
}
