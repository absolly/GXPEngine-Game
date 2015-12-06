using System;
using System.Drawing;
using System.Drawing.Text;

namespace GXPEngine
{
	public class Menu : GameObject
	{

		protected Font _font;
		protected SolidBrush _defaultColor;
		protected bool _paused;


		public Menu () : base ()
		{
			//make a fontcollection and store the font from file in here
			PrivateFontCollection pfc = new PrivateFontCollection ();
			pfc.AddFontFile ("Fonts/Dolce Vita.ttf");
			_font = new Font (pfc.Families [0], 25, FontStyle.Regular);

			//default text color
			_defaultColor = new SolidBrush (Color.White);
			_paused = true;

		}

		void Update ()
		{
			//disable buttons while game is active
			if (!_paused) {
				this.visible = false;
				return;
			}
		}

		/// <summary>
		/// Pause/unpause the game.
		/// </summary>
		/// <param name="paused">If set to <c>true</c> paused.</param>
		public void setPaused(bool paused){
			_paused = paused;
		}

		/// <summary>
		/// Creates the specified button.
		/// </summary>
		/// <param name="button">Button.</param>
		protected bool button (DrawString button)
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
		/// Checks if the mouse is over the button.
		/// </summary>
		/// <returns><c>true</c>, if mouse over, <c>false</c> otherwise.</returns>
		/// <param name="buttonX">Button x.</param>
		/// <param name="buttonY">Button y.</param>
		/// <param name="buttonWidth">Button width.</param>
		/// <param name="buttonHeight">Button height.</param>
		protected bool checkMouseOver (float buttonX, float buttonY, float buttonWidth, float buttonHeight)
		{
			if ((Input.mouseX <= buttonX + (buttonWidth / 2) && Input.mouseX >= buttonX - (buttonWidth / 2)) && (Input.mouseY <= buttonY + (buttonHeight / 2) && Input.mouseY >= buttonY - (buttonHeight / 2))) {
				return true;
			}
			return false;
		}

		/// <summary>
		/// Loads the selected level.
		/// </summary>
		/// <param name="levelName">Level name.</param>
		protected void playLevel (string levelName)
		{
			_paused = false;
			//Check if a level exists, if it does destroy it
			foreach (GameObject child in game.GetChildren()) {
				if (child is Menu) {
					Menu menu = child as Menu;
					menu.visible = false;
					menu.setPaused (false);
				}
				if (child is Level) {
					child.Destroy ();
					break;
				}
			}

			//turns levelname to a number for easy parsing
			Level level = new Level (int.Parse (levelName.Replace ("Level", "")));

			//adds the level to game
			game.AddChild (level);

			//hides the menu
			this.visible = false;
		}

		/// <summary>
		/// Play/Continue the level.
		/// </summary>
		protected void play ()
		{
			_paused = false;
			//Check if a level exists, if it does make it active
			foreach (GameObject child in game.GetChildren()) {
				if (child is Menu) {
					child.visible = false;
				}
				if (child is Level) {
					child.visible = true;
					this.visible = false;
					return;
				}
			}
			//If no level exists play the first level
			playLevel ("Level0");
		}
	}
}

