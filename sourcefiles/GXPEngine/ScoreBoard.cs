using System;
using System.Drawing;

namespace GXPEngine
{
	public class ScoreBoard : Canvas
	{
		private Font _font;
		private Brush _brush;
		private PointF _position;

		public ScoreBoard () : base(120,50)
		{
			_font = new Font ("Arial", 20, FontStyle.Regular);
			_brush = new SolidBrush (Color.White);
			_position = new PointF (0, 0);

		}
		public void drawScore(int score){
			string message = "Score: " + score;
			graphics.Clear (Color.Empty);
			graphics.DrawString (message, _font, _brush, _position);
		}
	}
}

