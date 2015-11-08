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
		private DrawString _continueButton;
		private DrawString _quitButton;
		private DrawString _selectLevelButton;
		private List<DrawString> _levelButtons;
		private DrawString[] _levelButtonsArray;
		private LevelLister _levelList;
		private Sprite _logo;

		private enum menuState
		{
			Main,
			LevelSelect,
		}

		private menuState _menuState;

		/// <summary>
		/// Initializes a new instance of the <see cref="GXPEngine.MainMenu"/> class.
		/// </summary>
		public MainMenu () : base ()
		{
			
			game.Add (this);

			//Set the menu background
			Sprite background = new Sprite ("Sprites/bg.png");
			AddChild (background);

			//Add custom font
			PrivateFontCollection pfc = new PrivateFontCollection ();
			pfc.AddFontFile ("Dolce Vita.ttf");
			_font = new Font (pfc.Families [0], 25, FontStyle.Regular);

			//default text color
			_defaultColor = new SolidBrush (Color.White);

			//render the game logo
			_logo = new Sprite ("Sprites/logo.png");
			_logo.SetOrigin ((_logo.width / 2), (_logo.height / 2));
			_logo.SetXY (244, 130);
			AddChild (_logo);

			//the main menu buttons
			_playButton = new DrawString ("Play", 244, 445, _font, _defaultColor);
			AddChild (_playButton);

			_continueButton = new DrawString ("Continue", 244, 445, _font, _defaultColor);
			AddChild (_continueButton);

			_selectLevelButton = new DrawString ("Level Select", 244, 550, _font, _defaultColor);
			AddChild (_selectLevelButton);

			_quitButton = new DrawString ("Quit", 244, 615, _font, _defaultColor);
			AddChild (_quitButton);


			_levelList = new LevelLister ();
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

		/// <summary>
		/// The main menu state.
		/// </summary>
		private void mainMenu ()
		{
			//Switch top button between "play" & "continue" based on if the level is loaded
			if (game.GetChildren ().Count > 1) {
				_playButton.visible = false;
				_continueButton.visible = true;
			} else {
				_playButton.visible = true;
				_continueButton.visible = false;
			}

			//Check if the buttons are clicked
			if (button (_playButton))
				play ();

			if (button (_continueButton))
				play ();

			if (button (_selectLevelButton))
				levelSelect ();

			if (button (_quitButton))
				quit ();

		}

		private bool button (DrawString button)
		{
			if (checkMouseOver (button.x, button.y, button.width, button.height)) {

				//make button red
				button.SetColor (255, 0, 0);
				if (Input.GetMouseButtonDown (0)) {
					return true;
				}
			} else {
				//make button white
				button.SetColor (255, 255, 255);
			}
			return false;
		}

		/// <summary>
		/// Play/Continue the level.
		/// </summary>
		private void play ()
		{
			//Check if a level exists, if it does make it active
			foreach (GameObject child in game.GetChildren()) {
				if (child is Level) {
					child.visible = true;
					this.visible = false;
					return;
				}
			}
			//If no level exists play the first level
			playLevel ("Level0");
		}

		/// <summary>
		/// Loads levels and sets the menu state to LevelSelect.
		/// </summary>
		private void levelSelect ()
		{
			Console.WriteLine ("LevelSelect");

			//Set menu state to "LevelSelect", this loads levelSelectMenu
			_menuState = menuState.LevelSelect;

			//hide main menu buttons
			foreach (GameObject child in GetChildren()) {
				if (child is DrawString) {
					child.visible = false;
				}
			}

			//draw the level buttons
			int i = 275;
			foreach (string levelName in _levelList.GetLevels()) {
				_levelButtons.Add (new DrawString (levelName, 244, i, _font, _defaultColor));
				i += 60;
			}

			//add menu buttons to an array
			_levelButtonsArray = _levelButtons.ToArray ();
			for (int k = 0; k < _levelButtonsArray.Length; k++) {
				AddChild (_levelButtonsArray [k]);
			}

		}

		/// <summary>
		/// Quit the game.
		/// </summary>
		private void quit ()
		{
			Console.WriteLine ("Quit");
			Environment.Exit (0);
		}

		/// <summary>
		/// The main menu state.
		/// </summary>
		private void levelSelectMenu ()
		{
			for (int k = 0; k < _levelButtonsArray.Length; k++) {

				//check if the buttons are moused over
				if (checkMouseOver (_levelButtonsArray [k].x, _levelButtonsArray [k].y, _levelButtonsArray [k].width, _levelButtonsArray [k].height)) {

					//set color to red
					_levelButtonsArray [k].SetColor (255, 0, 0);

					if (Input.GetMouseButtonDown (0)) {
						//play selected level
						playLevel (_levelList.GetLevels () [k]);
					}
				} else {
					//set color to white
					_levelButtonsArray [k].SetColor (255, 255, 255);
				}
			}
		}

		/// <summary>
		/// Loads the selected level.
		/// </summary>
		/// <param name="levelName">Level name.</param>
		private void playLevel (string levelName)
		{
			//turns levelname to a number for easy parsing
			Level level = new Level (int.Parse (levelName.Replace ("Level", "")));

			//adds the level to game
			game.AddChild (level);

			//hides the menu
			this.visible = false;
		}

		/// <summary>
		/// Checks if the mouse is over the button.
		/// </summary>
		/// <returns><c>true</c>, if mouse over, <c>false</c> otherwise.</returns>
		/// <param name="buttonX">Button x.</param>
		/// <param name="buttonY">Button y.</param>
		/// <param name="buttonWidth">Button width.</param>
		/// <param name="buttonHeight">Button height.</param>
		private bool checkMouseOver (float buttonX, float buttonY, float buttonWidth, float buttonHeight)
		{
			if ((Input.mouseX <= buttonX + (buttonWidth / 2) && Input.mouseX >= buttonX - (buttonWidth / 2)) && (Input.mouseY <= buttonY + (buttonHeight / 2) && Input.mouseY >= buttonY - (buttonHeight / 2))) {
				return true;
			}
			return false;
		}

	}
}
