using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing; // For RenderDebug(), GetBoundingBox()
using System.Runtime.InteropServices;
using Tao.OpenGl;

namespace EarthCoreEngine
{
    public class Renderer
    {
        Batch _batch = new Batch();
        int _currentTextureID = -1;

        //constructor enables relevant openGL texture mode and blend function
        public Renderer()
        {
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glEnable(Gl.GL_BLEND);
            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
        }


        // draw a vertex from vector, color, and point
        public void DrawImmediateModeVertex(Vector position, Color color, Point uvs)
        {
            Gl.glColor4f(color.Red, color.Green, color.Blue, color.Alpha); // set color
            Gl.glTexCoord2f(uvs.X, uvs.Y); // set texture coordinates
            Gl.glVertex3d(position.X, position.Y, position.Z); // draw vertex at position
        }

        // draw a CharacterSprite one character at a time
        public void DrawText(Text text)
        {
            foreach(CharacterSprite c in text.CharacterSprites)
            {
                DrawSprite(c.Sprite);
            }
        }

        public void Draw(IRenderable renderable)
        {
            DrawSprite(renderable.GetSprite());
        }

        public virtual void DrawSprite(Sprite sprite)
        {
            if (sprite.Texture.Id == _currentTextureID)
            {
                _batch.AddSprite(sprite);
            }
            else
            {
                _batch.Draw(); // Draw all with current texture

                // Update texture info
                _currentTextureID = sprite.Texture.Id;
                Gl.glBindTexture(Gl.GL_TEXTURE_2D, _currentTextureID);
                _batch.AddSprite(sprite);
            }
        }

        public void RenderDebug(Sprite sprite)
        {
            Gl.glDisable(Gl.GL_TEXTURE_2D);

            RectangleF bounds = GetBoundingBox(sprite);
            Gl.glBegin(Gl.GL_LINE_LOOP);
            {
                Gl.glColor3f(1, 0, 0);
                Gl.glVertex2f(bounds.Left, bounds.Top);
                Gl.glVertex2f(bounds.Right, bounds.Top);
                Gl.glVertex2f(bounds.Right, bounds.Bottom);
                Gl.glVertex2f(bounds.Left, bounds.Bottom);
            }
            Gl.glEnd();

            Gl.glEnable(Gl.GL_TEXTURE_2D);
        }

        private RectangleF GetBoundingBox(Sprite sprite)
        {
            float width = (float)(sprite.Width * sprite.XScale);
            float height = (float)(sprite.Height * sprite.YScale);
            return new RectangleF((float)sprite.XPosition - width / 2,
                (float)sprite.YPosition - height / 2, width, height);
        }


        // Called at end of each frame to ensure that no undrawn sprites are left in Batch queue
        public void Render()
        {
            _batch.Draw();
        }

    }
}
