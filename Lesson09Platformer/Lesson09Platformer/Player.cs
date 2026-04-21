using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lesson09Platformer;

    public class Player
    {
        private const int _Speed = 150, _JumpVelocity = -150;
        private enum State{Idle, Walking, Jumping}
        private State _state;
        private bool _faceingRight = true;
        private SimpleAnimation _animationIdle, _animationWalk, _animationJump, _animationCurrent;

        private Vector2 _position, _velocity, _dimensions;
    
    private Rectangle _gameBoundingBox;

    internal Vector2 Velocity {get => _velocity;}

    internal Rectangle BoundingBox
    {
        get {return new Rectangle((int)_position.X, (int)_position.Y, (int)_dimensions.X, (int)_dimensions.Y);}
    }

    public Player(Vector2 position, Rectangle gameBoundingBox)
    {
        _position = position;
        _gameBoundingBox = gameBoundingBox;
        _dimensions = new Vector2(35, 34);
    }

    internal void Initialize()
    {
        _state = State.Idle;
    }
    internal void LoadContent(ContentManager content)
    {
        // Idle: cells 30 px wide, 1/8 s per frame => 8 fps
        Texture2D idleTexture = content.Load<Texture2D>("Idle");
        int idleFrameWidth = 30;
        int idleFrameHeight = idleTexture.Height;
        int idleFrameCount = idleTexture.Width / idleFrameWidth;
        _animationIdle = new SimpleAnimation(idleTexture, idleFrameWidth, idleFrameHeight, idleFrameCount, 8f);

        // Walk: cells 35 px wide, 1/8 s per frame => 8 fps
        Texture2D walkTexture = content.Load<Texture2D>("Walk");
        int walkFrameWidth = 35;
        int walkFrameHeight = walkTexture.Height;
        int walkFrameCount = walkTexture.Width / walkFrameWidth;
        _animationWalk = new SimpleAnimation(walkTexture, walkFrameWidth, walkFrameHeight, walkFrameCount, 8f);

        // Jump: cells 30 px wide, 1/8 s per frame => 8 fps
        Texture2D jumpTexture = content.Load<Texture2D>("JumpOne");
        int jumpFrameWidth = 30;
        int jumpFrameHeight = jumpTexture.Height;
        int jumpFrameCount = jumpTexture.Width / jumpFrameWidth;
        _animationJump = new SimpleAnimation(jumpTexture, jumpFrameWidth, jumpFrameHeight, jumpFrameCount, 8f);

            // After loading, make sure Initialize will have something to use
            _animationCurrent = _animationIdle;
        }
        
        internal void Update(GameTime gameTime)
        {
            float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;
            _velocity.Y += Platformer._Gravity * dt;
            _position += _velocity * dt;
            if(Math.Abs(_velocity.Y) > Platformer._Gravity * dt)
        {
            _state = State.Jumping;
            _animationCurrent = _animationJump;
            _animationCurrent.Reset();
        }
    
            _animationCurrent?.Update(gameTime);
            switch (_state)
            {
                case State.Jumping:
                    break;
                case State.Idle:
                    break;
                case State.Walking:
                    break;
            }
        }
        internal void Draw(SpriteBatch spriteBatch)
    {
        switch (_state)
            {
                case State.Jumping:
                case State.Idle:
                case State.Walking:
                    SpriteEffects effects = _faceingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
                    _animationCurrent.Draw(spriteBatch, _position, effects);
                    break;

            }
    }
    
    internal void MoveHorizontally(float direction)
    {
        bool originalDirection = _faceingRight;
        _velocity.X = direction * _Speed;
        if(_velocity.X != 0)
            _faceingRight = _velocity.X > 0;

        if(_state == State.Idle)
        {
            _animationCurrent = _animationWalk;
            _state = State.Walking;
            _animationCurrent.Reset();
        }
        if(originalDirection != _faceingRight)
            _animationCurrent.Reset();
    }
internal void MoveVertically(float direction)
    {
        _velocity.Y = direction * _Speed;
    }

    internal void Stop()
    {
        _velocity.X = 0;
        if(_state == State.Walking)
        {
            _animationCurrent = _animationIdle;
            _state = State.Idle;
        }
    }

    internal void Land(Rectangle whatILandedOn)
    {
        if(_state == State.Jumping)
        {
            _position.Y = whatILandedOn.Top - _dimensions.Y + 1;
            _velocity.Y = 0;
            _state = State.Walking;
            _animationCurrent = _animationWalk;
            _animationCurrent.Reset();

        }
    }
    internal void StandOn(Rectangle whatImStandingOn, float dt)
    {
        _velocity.Y -= Platformer._Gravity * dt;
    
    }

    internal void Jump()
    {
        if(_state != State.Jumping)
            _velocity.Y = _JumpVelocity;
    }
}