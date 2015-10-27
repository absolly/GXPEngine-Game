using System;
using System.Drawing;
using System.Drawing.Text;


namespace GXPEngine
{
	public class MainMenu : GameObject
	{
		private Font _font;
		private SolidBrush _defaultColor;
		private DrawString _menuStrings;
		private DrawString _quitString;

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
				levelSelect ();
				break;
			default:
				mainMenu ();
				break;
			
			}




		}

		private void mainMenu ()
		{
			_menuStrings = new DrawString ("Play", game.width / 2, 20, _font, _defaultColor);

			if (checkMouseOver (_menuStrings.x, _menuStrings.y, _menuStrings.width, _menuStrings.height)) {
				
				_menuStrings.SetColor (255, 0, 0);
				if (Input.GetMouseButtonDown (0)) {
					play ();
				}
			} else {
				_menuStrings.SetColor (255, 255, 255);
			}
			AddChild (_menuStrings);

			_menuStrings = new DrawString ("Quit", game.width / 2, 50, _font, _defaultColor);

			if (checkMouseOver (_menuStrings.x, _menuStrings.y, _menuStrings.width, _menuStrings.height)) {

				_menuStrings.SetColor (255, 0, 0);
				if (Input.GetMouseButtonDown (0)) {
					quit ();
				}
			} else {
				_menuStrings.SetColor (255, 255, 255);
			}
			AddChild (_menuStrings);

		}

		private void play ()
		{
			Console.WriteLine ("play");
			
			foreach (GameObject child in game.GetChildren()) {
				if (child is Level) {
					child.visible = true;
					this.visible = false;
					return;
				}
			}
			_menuState = menuState.LevelSelect;
//			Level level = new Level ();
//			game.AddChild (level);
//			this.visible = false;
		}

		private void quit ()
		{
			Console.WriteLine ("Quit");
			Environment.Exit (0);
		}

		private void levelSelectMenu ()
		{
		}



		private void levelSelect ()
		{
			
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

