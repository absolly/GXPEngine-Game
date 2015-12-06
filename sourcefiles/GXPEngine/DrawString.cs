using System;
using System.Drawing;
using System.Drawing.Text;

namespace GXPEngine
{
	public class DrawString : Canvas
	{

		private PointF _position;
		private StringFormat _stringFormat;

		/// <summary>
		/// Initializes a new instance of the <see cref="GXPEngine.DrawString"/> class.
		/// </summary>
		/// <param name="text">Text to be drawn.</param>
		/// <param name="posX">Position x.</param>
		/// <param name="posY">Position y.</param>
		/// <param name="font">Font.</param>
		/// <param name="brush">SolidBrush.</param>
		public DrawString (string text, int posX, int posY, Font font, SolidBrush brush) : base ((int)(text.Length * font.Size * 0.8), (int)font.GetHeight ())
		{
			//set position of text on relative to the canvas
			_position = new PointF (width / 2, 0);
			SetOrigin (width / 2, height / 2);

			//set postion of the canvas
			this.x = posX;
			this.y = posY;

			//center the text horizontally
			_stringFormat = new StringFormat ();
			_stringFormat.Alignment = StringAlignment.Center;

			//clear the canvas
			graphics.Clear (Color.Empty);

			//draw the string
			graphics.DrawString (text, font, brush, _position, _stringFormat);
		}
			
	}
}
