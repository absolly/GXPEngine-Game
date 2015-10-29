using System;
using System.Drawing;
using System.Drawing.Text;

namespace GXPEngine
{
	public class Button : Canvas
	{
		private Font _font;
		private Brush _brush;
		private PointF _position;

		public Button () : base (200, 200)
		{
			PrivateFontCollection pfc = new PrivateFontCollection ();
			pfc.AddFontFile ("Dolce Vita.ttf");

			_font = new Font (pfc.Families [0], 20, FontStyle.Regular);
			_brush = new SolidBrush (Color.White);
			_position = new PointF (0, 0);
		
		}

		public void drawButton (string text, int posX, int posY)
		{
			this.width = text.Length * 17;
			this.height = (int)_font.GetHeight ();
			SetOrigin (width / 2, height / 2);
			this.x = posX;
			this.y = posY;

			graphics.Clear (Color.Empty);
			graphics.DrawString (text, _font, _brush, _position);
		}
	}
}

