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

		public Level () : base ()
		{
			
			game.Add (this);

			Canvas canvas = new Canvas(game.width, game.height);
			canvas.graphics.FillRectangle(new SolidBrush(Color.FromArgb(125,106,148)), new Rectangle(0, 0, game.width, game.height));
			AddChild(canvas);

		
			player = new Player ();
			scoreBoard = new ScoreBoard ();
			LevelImporter levelImporter = new LevelImporter ();
			TileRenderer tileRenderer = new TileRenderer ();
			tileRenderer.GetTiles (levelImporter);
			this.AddChild (tileRenderer);
			for(int i = 0; i < 100; i++){
				coin = new Coin ();
				this.AddChild (coin);

			}

			Console.WriteLine ("Level Loaded");
			Console.WriteLine ("\nLoading Player");

			this.AddChild (player);
			Console.WriteLine ("Player Loaded");

			this.AddChild (scoreBoard);

		}

		void Update ()
		{
			_score = player.getScore ();
			scoreBoard.drawScore (_score);
		}


	}
}

