using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace baddie_class
{
    public class Ghost
    {
        private List<Texture2D> _textures;
        private Vector2 _speed;
        private Rectangle _location;
        private SpriteEffects _direction;
        private int _textureIndex; // allows us to control which texture is drawn
        private float _animationSpeed;
        private float _seconds;

        public Ghost(List<Texture2D> textures, Rectangle location)
        {
            _textures = textures;
            _textureIndex = 0;
            _speed = Vector2.Zero;
            _location = location;
            _direction = SpriteEffects.None;
            _animationSpeed = 0.2f;
            _seconds = 0;
        }

        public Rectangle Rect
        {
            get { return _location; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_textures[_textureIndex], _location, null, Color.White, 0f, Vector2.Zero, _direction, 1);
        }

        public void Update(GameTime gameTime, MouseState mouseState)
        {
            _speed = Vector2.Zero;
            if (mouseState.X < _location.X)
            {
                _direction = SpriteEffects.FlipHorizontally;
                _speed.X = -1;
            } 
            else if (mouseState.X > _location.X)
            {
                _direction = SpriteEffects.None;
                _speed.X = 1;
            }
            if (mouseState.Y < _location.Y)
            {
                _speed.Y = -1;
            }
            else if (mouseState.Y > _location.Y)
            {
                _speed.Y = 1;
            }

            // only move if mouse button is down
            if (mouseState.LeftButton == ButtonState.Released)
            {
                _speed = Vector2.Zero;
                _textureIndex = 0;
                _seconds = 0f;
            }
            else if (_speed != Vector2.Zero)
            {
                _seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_seconds > _animationSpeed)
                {
                    _seconds = 0;
                    _textureIndex++;
                    if (_textureIndex >= _textures.Count)
                        _textureIndex = 1;
                }
            }
            _location.Offset(_speed);    
        }

        public bool Contains(Point player)
        {
            return _location.Contains(player);
        }

        public bool Intersects(Rectangle player)
        {
            return _location.Intersects(player);
        }
        
    }
}
