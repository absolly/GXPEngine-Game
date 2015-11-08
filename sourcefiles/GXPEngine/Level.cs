using System;
using System.Drawing;
using System.Collections.Generic;


namespace GXPEngine
{
	public class Level : GameObject
	{

		private Player _player;
		private HUD _hud;
		private TileRenderer _tileRenderer;
		private LevelImporter _levelImporter;
		private LevelLister _levelList;
		private int _score;
		private float _time = 60;
		private int _currentLevel;
		Level level;

		public Level (int level) : base ()
		{
			game.Add (this);
			_currentLevel = level;

			//set the background color
			Canvas canvas = new Canvas (game.width, game.height);
			canvas.graphics.FillRectangle (new SolidBrush (Color.FromArgb (125, 106, 148)), new Rectangle (0, 0, game.width, game.height));
			AddChild (canvas);

			_levelList = new LevelLister ();

			//import the selected level
			_levelImporter = new LevelImporter (level);
			this.AddChild (_levelImporter);

			//create the player
			_player = new Player ();


			//render the tiles of the selected level
			_tileRenderer = new TileRenderer ();
			this.AddChild (_tileRenderer);
			_tileRenderer.GetTiles (_levelImporter, _player);
			Console.WriteLine ("Level Loaded");

			//add the player to the level
			this.AddChild (_player);
			Console.WriteLine ("Player Loaded");

			//add the scoreboard
			_hud = new HUD ();
			this.AddChild (_hud);
			Console.WriteLine ("HUD loaded");


			//new Audio ("Audio/tower.mp3",true,false);
		}

		void Update ()
		{
			//stops the update loop when level is not visible(paused)
			if (!visible)
				return;

			//update & draw the hud
			_time -= Time.deltaTime;
			_score = _player.getScore ();
			_hud.drawHUD (_score, (int)_time);
			if (_time <= 0.0f) {
				
			}

			//pause the game
			if (Input.GetKeyDown (Key.P)) {
				this.visible = false;
				foreach (GameObject child in parent.GetChildren()) {
					if (child is MainMenu) {
						child.visible = true;
					}
				}
			}

		}

		/// <summary>
		/// Resets the level.
		/// </summary>
		public void ResetLevel ()
		{
			Level level = new Level (_currentLevel);
			game.AddChild (level);
			Console.WriteLine ("reset");
			this.Destroy ();
		}

		/// <summary>
		/// Destroys the current level and creates the next one as child of game.
		/// if there is no next level it will instead go back to the menu
		/// </summary>
		public void NextLevel ()
		{
			
			_currentLevel += 1;

			//check if the level exists
			foreach (string filename in _levelList.GetLevels()) {
				if (filename.Replace ("Level", "") == _currentLevel.ToString ()) {
					level = new Level (_currentLevel);
				}
			} 
			//if the level doesn't exist, load the menu
			if (level == null) {
				foreach (GameObject child in game.GetChildren()) {
					if (child is MainMenu) {
						child.visible = true;
					}
				}
			} else {
				game.AddChild (level);
				Console.WriteLine ("Next Level");
			}

			//remove the current level
			this.Destroy ();
		}

	}
}

