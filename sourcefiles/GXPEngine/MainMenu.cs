using System;
using System.Drawing;
using System.Drawing.Text;
using System.Collections.Generic;



namespace GXPEngine
{
	public class MainMenu : GameObject
	{
		private Font _font;
		private SolidBrush _defaultColor;
		private DrawString _playButton;
		private DrawString _quitButton;
		private List<DrawString> _levelButtons;
		private DrawString[] _levelButtonsArray;
		private LevelImporter _levelImporter;


		private enum menuState
		{
			Main,
			LevelSelect,
		}

		menuState _menuState;

		public MainMenu () : base ()
		{
			
			game.Add (this);
			Canvas canvas = new Canvas (game.width, game.height);
			canvas.graphics.FillRectangle (new SolidBrush (Color.FromArgb (125, 106, 148)), new Rectangle (0, 0, game.width, game.height));
			AddChild (canvas);

			PrivateFontCollection pfc = new PrivateFontCollection ();
			pfc.AddFontFile ("Dolce Vita.ttf");
			_font = new Font (pfc.Families [0], 25, FontStyle.Regular);
			_defaultColor = new SolidBrush (Color.White);

			_playButton = new DrawString ("Play", game.width / 2, 20, _font, _defaultColor);
			AddChild (_playButton);

			_quitButton = new DrawString ("Quit", game.width / 2, 50, _font, _defaultColor);
			AddChild (_quitButton);
			_levelImporter = new LevelImporter ();
			_levelButtons = new List<DrawString> (); 
		}

		void Update ()
		{
			//Disables the menu while invisible to improve preformance
			if (!visible)
				return;

			switch (_menuState) {
			case menuState.Main:
				mainMenu ();
				break;
			case menuState.LevelSelect:
				levelSelectMenu ();
				break;
			default:
				mainMenu ();
				break;

			}




		}

		private void mainMenu ()
		{
			

			if (checkMouseOver (_playButton.x, _playButton.y, _playButton.width, _playButton.height)) {

				_playButton.SetColor (255, 0, 0);
				if (Input.GetMouseButtonDown (0)) {
					play ();
				}
			} else {
				_playButton.SetColor (255, 255, 255);
			}



			if (checkMouseOver (_quitButton.x, _quitButton.y, _quitButton.width, _quitButton.height)) {

				_quitButton.SetColor (255, 0, 0);
				if (Input.GetMouseButtonDown (0)) {
					quit ();
				}
			} else {
				_quitButton.SetColor (255, 255, 255);
			}

		}

		private void play ()
		{
			Console.WriteLine ("play");

//			foreach (GameObject child in game.GetChildren()) {
//				if (child is Level) {
//					child.visible = true;
//					this.visible = false;
//					return;
//				}
//			}
			_menuState = menuState.LevelSelect;
			foreach (GameObject child in GetChildren()) {
				if (child is DrawString) {
					child.visible = false;
				}
			}
			int i = 20;
			foreach (string levelName in _levelImporter.GetLevels()) {
				_levelButtons.Add (new DrawString (levelName, game.width / 2, i, _font, _defaultColor));
				i += 60;
			}

			_levelButtonsArray = _levelButtons.ToArray ();
			for (int k = 0; k < _levelButtonsArray.Length; k++) {
				AddChild (_levelButtonsArray [k]);
			}

		}

		private void quit ()
		{
			Console.WriteLine ("Quit");
			Environment.Exit (0);
		}

		private void levelSelectMenu ()
		{
			for (int k = 0; k < _levelButtonsArray.Length; k++) {
				if (checkMouseOver (_levelButtonsArray [k].x, _levelButtonsArray [k].y, _levelButtonsArray [k].width, _levelButtonsArray [k].height)) {

					_levelButtonsArray [k].SetColor (255, 0, 0);
					if (Input.GetMouseButtonDown (0)) {
						playLevel (_levelImporter.GetLevels()[k]);
					}
				} else {
					_levelButtonsArray [k].SetColor (255, 255, 255);
				}
			}


		

		}

		private void playLevel (string levelName)
		{
			Level level = new Level (levelName);
			game.AddChild (level);
			this.visible = false;
		}


		private bool checkMouseOver (float buttonX, float buttonY, float buttonWidth, float buttonHeight)
		{
			if ((Input.mouseX <= buttonX + (buttonWidth / 2) && Input.mouseX >= buttonX - (buttonWidth / 2)) && (Input.mouseY <= buttonY + (buttonHeight / 2) && Input.mouseY >= buttonY - (buttonHeight / 2))) {
				return true;
			}
			return false;
		}

	}
}
