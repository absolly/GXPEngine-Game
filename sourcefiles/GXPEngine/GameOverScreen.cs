using System;
using System.Drawing;
using System.Drawing.Text;


namespace GXPEngine
{
	public class GameOverScreen :Menu
	{
		private DrawString _playButton;
		private DrawString _menuButton;
		private DrawString _levelString;

		private int _level;
		private Font _titlefont;

		/// <summary>
		/// Initializes a new instance of the <see cref="GXPEngine.GameOverScreen"/> class.
		/// </summary>
		/// <param name="cause">Cause of defeat.</param>
		/// <param name="level">Level.</param>
		public GameOverScreen (string cause, int level) : base ()
		{
			_level = level;

			//create font with larger size for the title text
			PrivateFontCollection pfc = new PrivateFontCollection ();
			pfc.AddFontFile ("Fonts/Dolce Vita.ttf");
			_titlefont = new Font (pfc.Families [0], 50, FontStyle.Regular);

			//draw the gameover text
			DrawString gameover = new DrawString (cause, game.width / 2, 170, _titlefont, _defaultColor);
			this.AddChild (gameover);

			//level stats
			_levelString = new DrawString ("Level: " + level, 640, 275, _font, _defaultColor);
			AddChild (_levelString);


			//the main menu buttons
			_playButton = new DrawString ("try again", 244, 550, _font, _defaultColor);
			AddChild (_playButton);

			//the main menu buttons
			_menuButton = new DrawString ("back to main menu", 640, 550, _font, _defaultColor);
			AddChild (_menuButton);
		}

		void Update ()
		{
			//retry button
			if (button (_playButton)) {
				playLevel ("Level" + _level);
				Destroy ();
			}

			//back to menu button
			if (button (_menuButton)) {
				foreach (GameObject child in parent.GetChildren()) {
					if (child is Menu) {
						Menu menu = child as Menu;
						menu.visible = true;
						menu.setPaused (true);
					} 
				}
				Destroy ();
			}
		}
	}
}
