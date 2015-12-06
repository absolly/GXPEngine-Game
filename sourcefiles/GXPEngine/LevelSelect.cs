using System;
using System.Collections.Generic;

namespace GXPEngine
{
	public class LevelSelect : Menu
	{
		private List<DrawString> _levelButtons;
		private DrawString[] _levelButtonsArray;
		private DrawString _backButton;
		private LevelLister _levelList;

		public LevelSelect () : base ()
		{
			//Set the menu background
			Sprite background = new Sprite ("Sprites/bg.png");
			AddChild (background);

			//render the game logo
			Sprite logo = new Sprite ("Sprites/logo.png");
			logo.SetOrigin ((logo.width / 2), (logo.height / 2));
			logo.SetXY (244, 130);
			AddChild (logo);

			//list of the level names
			_levelList = new LevelLister ();

			//list of level buttons
			_levelButtons = new List<DrawString> (); 

			Console.WriteLine ("LevelSelect");

			//add the menubuttons to the list
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

			//draw the back button
			_backButton = new DrawString ("Back", 244, 615, _font, _defaultColor);
			AddChild (_backButton);
		}

		void Update ()
		{
			//stops buttons usage while ingame
			if (!_paused || !visible)
				return;

			if (button (_backButton))
				back ();
			
			//draw the levelbuttons
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
		/// Back to main menu.
		/// </summary>
		private void back(){
			foreach (GameObject child in game.GetChildren()) {
				if (child is MainMenu) {
					MainMenu menu = child as MainMenu;
					menu.setMainMenu ();
					menu.visible = true;
					this.visible = false;
					return;
				}
			}
		}
	}
}

