using System;
using System.Drawing;
using System.Collections.Generic;


namespace GXPEngine
{
	public class Level : GameObject
	{

		Player player;
		HUD scoreBoard;
		TileRenderer tileRenderer;
		LevelImporter levelImporter;
		private int _score;
		private float _time = 60;
		private string _currentLevel;

		public Level (string level) : base ()
		{
			
			game.Add (this);
			_currentLevel = level;
			Canvas canvas = new Canvas (game.width, game.height);
			canvas.graphics.FillRectangle (new SolidBrush (Color.FromArgb (125, 106, 148)), new Rectangle (0, 0, game.width, game.height));
			AddChild (canvas);
		
			player = new Player ();
			scoreBoard = new HUD ();
			levelImporter = new LevelImporter (level);
			tileRenderer = new TileRenderer ();
			tileRenderer.GetTiles (levelImporter, player);
			this.AddChild (tileRenderer);
			Console.WriteLine ("Level Loaded");
			Console.WriteLine ("\nLoading Player");

			this.AddChild (player);
			Console.WriteLine ("Player Loaded");

			this.AddChild (scoreBoard);
			//new Audio ("Audio/tower.mp3",true,false);
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
		public void ResetLevel(){
			Level level = new Level (_currentLevel);
			game.AddChild (level);
			Console.WriteLine ("reset");
			this.Destroy ();
		}

	}
}

