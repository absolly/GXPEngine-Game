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
		private float _time = 20;
		private int _lives;
		private int _score;

		private int _currentLevel;
		Level level;

		public Level (int level, int lives = 3) : base ()
		{
			game.Add (this);
			_currentLevel = level;


			//set the background color
			Canvas canvas = new Canvas (game.width, game.height);
			canvas.graphics.FillRectangle (new SolidBrush (Color.FromArgb (125, 106, 148)), new Rectangle (0, 0, game.width, game.height));
			AddChild (canvas);

			//level lister to get the level names
			_levelList = new LevelLister ();

			//import the selected level
			_levelImporter = new LevelImporter (level);
			this.AddChild (_levelImporter);

			//create the player
			_player = new Player (lives);


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

		}

		void Update ()
		{
			//stops the update loop when level is not visible(paused)
			if (!visible)
				return;

			drawHUD ();

			//pause the game
			if (Input.GetKeyDown (Key.P)) {
				this.visible = false;
				foreach (GameObject child in parent.GetChildren()) {
					if (child is Menu) {
						Menu menu = child as Menu;
						menu.visible = true;		
						menu.setPaused (true);
					}
				}
			}
			if (_lives <= 0 || _time <= 0)
				gameOver ();
			DeveloperShortcuts ();
		}

		/// <summary>
		/// shows the gameover screen.
		/// </summary>
		private void gameOver ()
		{
			if (_lives <= 0) {
				GameOverScreen gameOver = new GameOverScreen ("you ran out of lives", _currentLevel);
				game.AddChild (gameOver);
			}
			if (_time <= 0) {
				GameOverScreen gameOver = new GameOverScreen ("you ran out of time", _currentLevel);
				game.AddChild (gameOver);
			}
			Destroy ();
		}

		private void DeveloperShortcuts ()
		{
			//skip level
			if (Input.GetKeyDown (Key.NUMPAD_0)) {
				InstantNextLevel ();
			}

			//reset level
			if (Input.GetKeyDown (Key.R)) {
				ResetLevel ();
			}

			//lives up/down
			if (Input.GetKey (Key.NUMPAD_4)) {
				_player.setLives (_lives + 1);
			} else if (Input.GetKey (Key.NUMPAD_1)) {
				_player.setLives (_lives - 1);
			}

			//time up/down
			if (Input.GetKey (Key.NUMPAD_5)) {
				_time += 1;
			} else if (Input.GetKey (Key.NUMPAD_2)) {
				_time -= 1;
			}

			//score up/down
			if (Input.GetKey (Key.NUMPAD_6)) {
				_player.setScore (_score + 1);
			} else if (Input.GetKey (Key.NUMPAD_3)) {
				_player.setScore (_score - 1);
			}
		}

		/// <summary>
		/// Draws the HUD.
		/// </summary>
		private void drawHUD ()
		{
			_time -= Time.deltaTime;
			_score = _player.getScore ();
			_lives = _player.getLives ();
			_hud.drawHUD (_score, (int)_time, _lives);
		}

		/// <summary>
		/// Resets the level.
		/// </summary>
		public void ResetLevel ()
		{
			//update the lives so the correct number is taken trought the reset
			_lives = _player.getLives ();

			Level level = new Level (_currentLevel, _lives);
			game.AddChild (level);
			Console.WriteLine ("reset");
			this.Destroy ();
		}

		/// <summary>
		/// Destroys the current level and creates the next one as child of game.
		/// if there is no next level it will instead go back to the menu
		/// </summary>
		public void InstantNextLevel ()
		{
			
			_currentLevel += 1;
			//check if the level exists
			foreach (string filename in _levelList.GetLevels()) {
				if (filename.Replace ("Level", "") == _currentLevel.ToString ()) {
					level = new Level (_currentLevel);
					game.AddChild (level);
					Console.WriteLine ("Next Level");
					Destroy ();
					return;
				}
			} 

			//if the level doesn't exist, load the menu
			foreach (GameObject child in game.GetChildren()) {
				if (child is Menu) {
					Menu menu = child as Menu;
					menu.visible = true;
					menu.setPaused (true);
				}
			}
			//remove the current level
			this.Destroy ();
		}

		public void NextLevel ()
		{
			WinScreen winscreen = new WinScreen (_currentLevel, _score, (int)_time, _lives);
			game.AddChild (winscreen);
			Destroy ();
		}

	}
}

