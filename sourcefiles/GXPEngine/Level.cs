using System;
using System.Drawing;

namespace GXPEngine
{
	public class Level : GameObject
	{

		Player player;
		Coin coin;
		ScoreBoard scoreBoard;
		private int _score;
		private float _time = 60;

		public Level () : base ()
		{
			
			game.Add (this);

			Canvas canvas = new Canvas (game.width, game.height);
			canvas.graphics.FillRectangle (new SolidBrush (Color.FromArgb (125, 106, 148)), new Rectangle (0, 0, game.width, game.height));
			AddChild (canvas);

		
			player = new Player ();
			scoreBoard = new ScoreBoard ();
			LevelImporter levelImporter = new LevelImporter ();
			TileRenderer tileRenderer = new TileRenderer ();
			tileRenderer.GetTiles (levelImporter);
			this.AddChild (tileRenderer);
			for (int i = 0; i < 100; i++) {
				coin = new Coin ();
				this.AddChild (coin);

			}

			Console.WriteLine ("Level Loaded");
			Console.WriteLine ("\nLoading Player");

			this.AddChild (player);
			Console.WriteLine ("Player Loaded");

			this.AddChild (scoreBoard);
			Audio audio = new Audio ();
			this.AddChild (audio);
		}

		void Update ()
		{
			_time -= Time.deltaTime;
			_score = player.getScore ();
			scoreBoard.drawScoreTime (_score, (int)Math.Ceiling(_time));
			if (_time <= 0.0f){
				
			}

		}


	}
}

