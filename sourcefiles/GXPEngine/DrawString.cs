using System;
using System.Drawing;
using System.Drawing.Text;

namespace GXPEngine
{
	public class DrawString : Canvas
	{
		private Font _font;
		private Brush _brush;
		private PointF _position;

		public DrawString (string text, int posX, int posY) : base (text.Length * 17, 40)
		{
			graphics.Clear (Color.Empty);
			_position = new PointF (0, 0);

			PrivateFontCollection pfc = new PrivateFontCollection ();
			pfc.AddFontFile ("Dolce Vita.ttf");
			_font = new Font (pfc.Families [0], 20, FontStyle.Regular);

			_brush = new SolidBrush (Color.White);

			SetOrigin (width / 2, height / 2);
			this.x = posX;
			this.y = posY;

			graphics.Clear (Color.Empty);
			graphics.DrawString (text, _font, _brush, _position);
		}

		public DrawString (string text, int posX, int posY, Font font, SolidBrush brush) : base (text.Length * 17, (int)font.GetHeight())
		{
			graphics.Clear (Color.Empty);
			_position = new PointF (0, 0);

			SetOrigin (width / 2, height / 2);
			this.x = posX;
			this.y = posY;

			graphics.Clear (Color.Empty);

			graphics.DrawString (text, font, brush, _position);
		}
		public void Clear(){
			graphics.Clear (Color.Empty);
			this.Destroy();
		}
			
	}
}
