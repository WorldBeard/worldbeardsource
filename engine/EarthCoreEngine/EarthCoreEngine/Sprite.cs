using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EarthCoreEngine
{
    public class Sprite : IRenderable
    {
        //--------------------------
        // PROPERTIES
        //--------------------------
        internal const int VertexAmount = 6; // i.e. two triangles to form a quad

        // VERTEX POSITIONS
        private Vector[] _vertexPositions = new Vector[VertexAmount];
        public Vector[] VertexPositions
        {
            get { return _vertexPositions; }
        }
        public Vector TopLeft
        {
            get { return _vertexPositions[0]; }
        }
        public Vector TopRight
        {
            get { return _vertexPositions[1]; }
        }
        public Vector BottomLeft
        {
            get { return _vertexPositions[2]; }
        }
        public Vector BottomRight
        {
            get { return _vertexPositions[4]; }
        }

        // COLOR
        private Color[] _vertexColors = new Color[VertexAmount];
        public Color[] VertexColors
        {
            get { return _vertexColors; }

        }
        public Color Color
        {
            get { return _vertexColors[0]; }
            set
            {
                for (int i = 0; i < Sprite.VertexAmount; i++)
                {
                    _vertexColors[i] = value;
                }
            }
        }

        // VERTEX UVS
        private Point[] _vertexUVs = new Point[VertexAmount];
        public Point[] VertexUVs
        {
            get { return _vertexUVs; }
        }
        public void SetUVs(Point topLeft, Point bottomRight)
        {
            _vertexUVs[0] = topLeft;
            _vertexUVs[1] = new Point(bottomRight.X, topLeft.Y);
            _vertexUVs[2] = new Point(topLeft.X, bottomRight.Y);
            _vertexUVs[3] = new Point(bottomRight.X, topLeft.Y);
            _vertexUVs[4] = bottomRight;
            _vertexUVs[5] = new Point(topLeft.X, bottomRight.Y);
        }

        // TEXTURE
        private Texture _texture;
        public Texture Texture
        {
            get { return _texture; }
            set
            {
                _texture = value;
                InitVertexPositions(GetCenter(), _texture.Width, _texture.Height);
            }
        }

        // SCALE
        private double _xScale = 1;
        public double XScale
        {
            get { return _xScale; }
            set { SetScale(value, _yScale); }
        }
        private double _yScale = 1;
        public double YScale
        {
            get { return _yScale; }
            set { SetScale(_xScale, value); }
        }
        public double Scale { set { SetScale(value, value); } }

        // ROTATION
        private double _rotation = 2 * Math.PI;
        public double Rotation
        {
            get { return _rotation; }
            set { SetRotation(value); }
        }

        // X POSITION
        private double _xPosition
        {
            get { return Center.X; }
            set { SetPosition(value, _yPosition); }
        }
        public double XPosition
        {
            get { return _xPosition; }
            set { SetPosition(value, _yPosition); }
        }

        // Y POSITION
        private double _yPosition
        {
            get { return Center.Y; }
            set { SetPosition(_xPosition, value); }
        }
        public double YPosition
        {
            get { return _yPosition; }
            set { SetPosition(_xPosition, value); }
        }

        // WIDTH
        private double _width = 0;
        public double Width
        {
            get { return _width; }
            set { InitVertexPositions(GetCenter(), value, this.Height); }
        }

        // HEIGHT
        private double _height = 0;
        public double Height
        {
            get { return _height; }
            set { InitVertexPositions(GetCenter(), this.Width, value); }
        }

        // LEFT, RIGHT, TOP, BOTTOM
        public double Left
        {
            get { return _vertexPositions[0].X; }
            set { this.Move(value - this.Left, 0); }
        }
        public double Right
        {
            get { return _vertexPositions[1].X; }
            set { this.Move(value - this.Right, 0); }
        }
        public double Top
        {
            get { return _vertexPositions[0].Y; }
            set { this.Move(0, value - this.Top); }
        }
        public double Bottom
        {
            get { return _vertexPositions[2].Y; }
            set { this.Move(0, value - this.Bottom); }
        }

        // CENTER
        public Vector Center {
            get { return GetCenter(); }
            set { SetPosition(value); }
        }


        //--------------------------
        // CONSTRUCTORS
        //--------------------------
        public Sprite() : this(new Texture(), new Vector(0, 0, 0), 0, 0) { }

        public Sprite(Texture texture) : this(texture, new Vector(0, 0, 0), texture.Width, texture.Height) { }

        public Sprite(String pathName) : this(TextureManager.LoadStaticTexture(pathName)) { }

        public Sprite(String pathName, double width, double height) : this(TextureManager.LoadStaticTexture(pathName), width, height) { }

        public Sprite(Texture texture, Vector startingPosition) : this(texture, startingPosition, texture.Width, texture.Height) { }

        public Sprite(Texture texture, double width, double height) : this(texture, new Vector(0, 0, 0), width, height) { }

        public Sprite(Texture texture, Vector startingPosition, double width, double height)
        {
            if (width <= 0) width = texture.Width;
            if (height <= 0) height = texture.Height;
            
            _xPosition = startingPosition.X;
            _yPosition = startingPosition.Y;
            _texture = texture;

            SetScale(width / texture.Width, height / texture.Height);

            InitVertexPositions(new Vector(_xPosition, _yPosition, 0), width, height);
            this.Color = new Color(1, 1, 1, 1);
            SetUVs(new Point(0, 0), new Point(1, 1));
        }


        //--------------------------
        // PUBLIC METHODS
        //--------------------------
        public void Move(double x, double y)
        {
            Move(new Vector(x, y, 0));
        }

        public void Move(Vector vector)
        {
            double newX = this.Center.X + vector.X;
            double newY = this.Center.Y + vector.Y;
            double newZ = this.Center.Z + vector.Z;

            SetPosition(new Vector(newX, newY, newZ));
        }

        public void SetPosition(Vector position)
        {
            Matrix m = new Matrix();
            m.SetTranslation(new Vector(_xPosition, _yPosition, 0));
            ApplyMatrix(m.Inverse());
            m.SetTranslation(position);
            ApplyMatrix(m);
        }

        public void SetPosition(double x, double y)
        {
            SetPosition(new Vector(x, y, VertexPositions[0].Z));
        }

        // OLD
        /*public void SetPosition(Vector position) // takes vector argument
        {
            InitVertexPositions(position, GetWidth(), GetHeight());
        }*/


        //--------------------------
        // PRIVATE METHODS
        //--------------------------
        private Vector GetCenter()
        {
            return _vertexPositions[0] + (_vertexPositions[4] - _vertexPositions[0]).Multiply(0.5);
        }

        // Set sprite's quad centered at 'position', with dimensions 'width' and 'height'
        private void InitVertexPositions(Vector position, double width, double height)
        {
            double halfWidth = width / 2;
            double halfHeight = height / 2;
            _vertexPositions[0] = new Vector(position.X - halfWidth, position.Y + halfHeight, position.Z);
            _vertexPositions[1] = new Vector(position.X + halfWidth, position.Y + halfHeight, position.Z);
            _vertexPositions[2] = new Vector(position.X - halfWidth, position.Y - halfHeight, position.Z);
            _vertexPositions[3] = new Vector(position.X + halfWidth, position.Y + halfHeight, position.Z);
            _vertexPositions[4] = new Vector(position.X + halfWidth, position.Y - halfHeight, position.Z);
            _vertexPositions[5] = new Vector(position.X - halfWidth, position.Y - halfHeight, position.Z);
            _width = width;
            _height = height;

            // Since this method implicitly un-rotates the matrix, we must re-rotate it.
            double rotation = _rotation;
            _rotation = 2 * Math.PI;
            SetRotation(rotation);
        }

        private void ApplyMatrix(Matrix m)
        {
            for (int i = 0; i < VertexPositions.Length; i++)
            {
                VertexPositions[i] *= m;
            }
        }

        private void SetScale(double x, double y)
        {
            double oldX = _xPosition;
            double oldY = _yPosition;
            SetPosition(0, 0);
            Matrix mScale = new Matrix();
            mScale.SetScale(new Vector(_xScale, _yScale, 1));
            mScale = mScale.Inverse();
            ApplyMatrix(mScale);
            mScale = new Matrix();
            mScale.SetScale(new Vector(x, y, 1));
            ApplyMatrix(mScale);
            SetPosition(oldX, oldY);
            _xScale = x;
            _yScale = y;
        }

        private void SetRotation(double rotation)
        {
            if (Math.Abs(rotation) < 0.00001) { rotation += 2 * Math.PI; } // Hackity hack hack.

            double oldX = _xPosition;
            double oldY = _yPosition;
            SetPosition(0, 0);
            Matrix mRot;
            if (_rotation != 0)
            {
                mRot = new Matrix();
                mRot.Set2DRotate(_rotation);
                ApplyMatrix(mRot.Inverse());
            }
            if (rotation != 0)
            {
                mRot = new Matrix();
                mRot.Set2DRotate(rotation);
                ApplyMatrix(mRot);
            }
            SetPosition(oldX, oldY);
            _rotation = rotation;
        }

        public Sprite GetSprite()
        {
            return this;
        }
    }
}
