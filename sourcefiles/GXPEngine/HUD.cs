using System;
using System.Drawing;
using System.Drawing.Text;


namespace GXPEngine
{
	public class HUD : Canvas
	{
		private Font _font;
		private Brush _brush;
		private PointF _position;

		/// <summary>
		/// Initializes a new instance of the <see cref="GXPEngine.HUD"/> class.
		/// </summary>
		public HUD () : base(400,80)
		{
			//make a fontcollection and store the font from file in here
			PrivateFontCollection pfc = new PrivateFontCollection();
			pfc.AddFontFile("Dolce Vita.ttf");
			_font = new Font (pfc.Families[0], 20, FontStyle.Regular);

			//set the text color
			_brush = new SolidBrush (Color.White);

			//set position of text on relative to the canvas
			_position = new PointF (0, 0);

			//draw the hud
			graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
		}

		/// <summary>
		/// Draws the current score, time and fps.
		/// </summary>
		/// <param name="score">Score.</param>
		/// <param name="time">Time.</param>
		public void drawHUD(int score, int time){
			string message = "Score: " + score + "\nTime: " + time + "\nFPS: " + game.currentFps;
			graphics.Clear (Color.Empty);
			graphics.DrawString (message, _font, _brush, _position);

		}
	}
}

