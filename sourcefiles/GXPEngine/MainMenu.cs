using System;
using System.Drawing;


namespace GXPEngine
{
	public class MainMenu : GameObject
	{
		private Font _font;
		private SolidBrush _brush;
		private int _selected = 0;

		public MainMenu () : base()
		{

			game.Add (this);
			Canvas canvas = new Canvas (game.width, game.height);
			canvas.graphics.FillRectangle (new SolidBrush (Color.FromArgb (125, 106, 148)), new Rectangle (0, 0, game.width, game.height));
			AddChild (canvas);

			_font = new Font ("Arial", 20, FontStyle.Regular);
			_brush = new SolidBrush (Color.White);



		}
		void Update(){
			if (!visible)
				return;
			getKey ();
			drawString ();
			selectOption ();


		}
		public void getKey(){
			if (Input.GetKey(Key.DOWN) && _selected != 1) {
				_selected++;
			}
			if (Input.GetKey(Key.UP) && _selected != 0) {
				_selected--;
			}
		}
		public void drawString(){
			if(_selected == 0){ 
				DrawString drawString = new DrawString ("Play", game.width/2, 20, _font, new SolidBrush (Color.Red));
				AddChild (drawString);
				drawString = new DrawString ("Quit", game.width/2, 50, _font, _brush);
				AddChild (drawString);
			}
			if(_selected == 1){ 
				DrawString drawString = new DrawString ("Play", game.width/2, 20, _font, _brush);
				AddChild (drawString);
				drawString = new DrawString ("Quit", game.width/2, 50, _font, new SolidBrush (Color.Red));
				AddChild (drawString);
			}
		}

		/// <summary>
		/// Selects the option.
		/// </summary>
		public void selectOption(){
			if(Input.GetKeyDown(Key.ENTER)){
				switch(_selected){
				case 0:
					Console.WriteLine ("play");
					Level level = new Level ();
					game.AddChild (level);
					this.visible = false;

					break;
				case 1:
					Console.WriteLine ("Quit");

					break;
				}
			}
		}
	}
}

