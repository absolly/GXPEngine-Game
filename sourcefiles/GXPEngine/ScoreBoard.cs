using System;
using System.Drawing;

namespace GXPEngine
{
	public class ScoreBoard : Canvas
	{
		private Font _font;
		private Brush _brush;
		private PointF _position;

		public ScoreBoard () : base(400,60)
		{
			_font = new Font ("Arial", 20, FontStyle.Regular);
			_brush = new SolidBrush (Color.White);
			_position = new PointF (0, 0);

		}

		/// <summary>
		/// Draws the current score & time.
		/// </summary>
		/// <param name="score">Score.</param>
		/// <param name="time">Time.</param>
		public void drawScoreTime(int score, int time){
			string message = "Score: " + score + "\nTime: " + time;
			graphics.Clear (Color.Empty);
			graphics.DrawString (message, _font, _brush, _position);

		}
	}
}

