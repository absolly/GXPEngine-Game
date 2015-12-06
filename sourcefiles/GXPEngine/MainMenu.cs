using System;
using System.Drawing;
using System.Drawing.Text;
using System.Collections.Generic;

namespace GXPEngine
{
	public class MainMenu : Menu
	{

		private DrawString _playButton;
		private DrawString _continueButton;
		private DrawString _quitButton;
		private DrawString _selectLevelButton;
		private LevelSelect _levelSelect;

		protected enum menuState
		{
			Main,
			LevelSelect,
		}

		protected menuState _menuState;

		/// <summary>
		/// Initializes a new instance of the <see cref="GXPEngine.MainMenu"/> class.
		/// </summary>
		public MainMenu () : base ()
		{
			game.Add (this);

			//Set the menu background
			Sprite background = new Sprite ("Sprites/bg.png");
			AddChild (background);

			//render the game logo
			Sprite logo = new Sprite ("Sprites/logo.png");
			logo.SetOrigin ((logo.width / 2), (logo.height / 2));
			logo.SetXY (244, 130);
			AddChild (logo);

			//the main menu buttons
			_playButton = new DrawString ("Play", 244, 445, _font, _defaultColor);
			AddChild (_playButton);

			_continueButton = new DrawString ("Continue", 244, 445, _font, _defaultColor);
			AddChild (_continueButton);

			_selectLevelButton = new DrawString ("Level Select", 244, 550, _font, _defaultColor);
			AddChild (_selectLevelButton);

			_quitButton = new DrawString ("Quit", 244, 615, _font, _defaultColor);
			AddChild (_quitButton);

			_levelSelect = new LevelSelect ();
			game.AddChild (_levelSelect);


		}

		void Update ()
		{
			//stops buttons being used while ingame
			if (!_paused){
				this.visible = false;
				return;
			}
			
			switch (_menuState) {
			case menuState.Main:
				mainMenu ();
				this.visible = true;
				_levelSelect.visible = false;
				break;
			case menuState.LevelSelect:
				this.visible = false;
				_levelSelect.visible = true;
				break;
			default:
				mainMenu ();
				this.visible = true;
				_levelSelect.visible = false;
				break;

			}	
		}

		/// <summary>
		/// The main menu state.
		/// </summary>
		private void mainMenu ()
		{
			//Switch top button between "play" & "continue" based on if the level is loaded
			if (game.GetChildren ().Count > 2) {
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

			if (button (_selectLevelButton)) {
				_menuState = menuState.LevelSelect;
			}
			if (button (_quitButton))
				quit ();

		}

		/// <summary>
		/// Make the main menu active.
		/// </summary>
		public void setMainMenu(){
			_menuState = menuState.Main;
		}
			
		/// <summary>
		/// Quit the game.
		/// </summary>
		private void quit ()
		{
			Console.WriteLine ("Quit");
			Environment.Exit (0);
		}


	}
}
