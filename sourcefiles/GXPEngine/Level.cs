using System;
using System.Collections.Generic;



namespace GXPEngine
{
	public class Level : GameObject
	{

		Player player;
		Coin coin;

		public Level () : base ()
		{
			
			game.Add (this);

			coin = new Coin ();
			player = new Player ();
			LevelImporter levelImporter = new LevelImporter ();
			TileRenderer tileRenderer = new TileRenderer ();
			tileRenderer.GetTiles (levelImporter);
			this.AddChild (tileRenderer);
			this.AddChild (coin);

			Console.WriteLine ("Level Loaded");
			Console.WriteLine ("\nLoading Player");

			this.AddChild (player);
			Console.WriteLine ("Player Loaded");

		}

		void Update ()
		{
//			TestCollision ();

		}
//		void TestCollision(){
//			if (player.HitTest (coin)) {
//				coin.Destroy ();
//			}
//			foreach (AnimationSprite tile in tiles) {
//
//
//				if (player.HitTest (tile)) {
//					if (tile.currentFrame != 0) {
//				    	player.MoveBack (true);
//
//
//					}
//					else {
//						player.MoveBack (false);
//					}
//				} 
//
//			}
//		}


	}
}

