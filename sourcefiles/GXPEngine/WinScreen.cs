using System;
using System.Drawing;
using System.Drawing.Text;


namespace GXPEngine
{
	public class WinScreen :Menu
	{
		private DrawString _playButton;
		private DrawString _menuButton;
		private DrawString _nextButton;

		private DrawString _levelString;
		private DrawString _scoreString;
		private DrawString _timeString;
		private DrawString _livesString;

		private LevelLister _levelList;


		private int _level;
		private Font _titlefont;

		/// <summary>
		/// Initializes a new instance of the <see cref="GXPEngine.WinScreen"/> class.
		/// </summary>
		/// <param name="level">Level.</param>
		/// <param name="score">Score.</param>
		/// <param name="time">Time.</param>
		/// <param name="lives">Lives.</param>
		public WinScreen (int level, int score, int time, int lives) : base ()
		{
			_level = level;

			//add custom font with bigger size for title text
			PrivateFontCollection pfc = new PrivateFontCollection ();
			pfc.AddFontFile ("Fonts/Dolce Vita.ttf");
			_titlefont = new Font (pfc.Families [0], 50, FontStyle.Regular);

			//draw the gameover text
			DrawString gameover = new DrawString ("level complete", game.width / 2, 170, _titlefont, _defaultColor);
			this.AddChild (gameover);


			//the menu buttons
			_playButton = new DrawString ("try again", 244, 550, _font, _defaultColor);
			AddChild (_playButton);

			_menuButton = new DrawString ("back to main menu", 640, 550, _font, _defaultColor);
			AddChild (_menuButton);

			_levelList = new LevelLister();

			_nextButton = new DrawString ("next level", 1036, 550, _font, _defaultColor);
			AddChild (_nextButton);


			//level stats
			_levelString = new DrawString("Level: " + level, 640, 275, _font, _defaultColor);
			AddChild (_levelString);

			_timeString = new DrawString("time left: " + time, 640, 325, _font, _defaultColor);
			if (time >= 10)
				_timeString.SetColor (0, 255, 0);
			AddChild (_timeString);

			_scoreString = new DrawString("score: " + score, 640, 365, _font, _defaultColor);
			if (score >= 15)
				_scoreString.SetColor (0, 255, 0);
			AddChild (_scoreString);

			_livesString = new DrawString("lives left: " + lives, 640, 405, _font, _defaultColor);
			if (lives >= 3)
				_livesString.SetColor (0, 255, 0);
			AddChild (_livesString);


		}

		void Update ()
		{
			if (button (_playButton)) {
				playLevel ("Level" + _level);
				Destroy ();
			}
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
			if (button (_nextButton)) {
				foreach (string filename in _levelList.GetLevels()) {
					if (filename.Replace ("Level", "") == (_level+1).ToString ()) {
						playLevel ("Level" + (_level + 1));
						Destroy ();
					}
				} 
			}
		}
	}
}
