using System;
using System.Drawing;
using System.Collections.Generic;


namespace GXPEngine
{
	public class Level : GameObject
	{

		Player player;
		HUD scoreBoard;
		private int _score;
		private float _time = 60;

		public Level () : base ()
		{
			
			game.Add (this);

			Canvas canvas = new Canvas (game.width, game.height);
			canvas.graphics.FillRectangle (new SolidBrush (Color.FromArgb (125, 106, 148)), new Rectangle (0, 0, game.width, game.height));
			AddChild (canvas);
		
			player = new Player ();
			scoreBoard = new HUD ();
			LevelImporter levelImporter = new LevelImporter ();
			TileRenderer tileRenderer = new TileRenderer ();
			tileRenderer.GetTiles (levelImporter, player);
			this.AddChild (tileRenderer);
			Console.WriteLine ("Level Loaded");
			Console.WriteLine ("\nLoading Player");

			this.AddChild (player);
			Console.WriteLine ("Player Loaded");

			this.AddChild (scoreBoard);
			new Audio ("Audio/tower.mp3",true,false);
		}

		void Update ()
		{
			_time -= Time.deltaTime;
			_score = player.getScore ();
			scoreBoard.drawHUD (_score, (int)Math.Ceiling(_time));
			if (_time <= 0.0f){
				
			}
			if (Input.GetKeyDown (Key.P)) {
				this.visible = false;
				foreach (GameObject child in parent.GetChildren()) {
					if (child is MainMenu) {
						child.visible = true;
					}
				}
			}
		}

	}
}

