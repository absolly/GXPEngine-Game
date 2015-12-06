using System;
using System.IO;
using System.Text;
using System.Linq;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;

namespace GXPEngine
{
	public class TileRenderer : GameObject
	{
		//list of tiles
		List<AnimationSprite> tiles;


		/// <summary>
		/// Initializes a new instance of the <see cref="GXPEngine.TileRenderer"/> class.
		/// </summary>
		public TileRenderer () : base ()
		{
		}

		/// <summary>
		/// Takes the tile numbers from the array and renders the tiles.
		/// </summary>
		/// <returns>The tile list.</returns>
		/// <param name="levelimporter">Levelimporter.</param>
		public List<AnimationSprite> GetTiles (LevelImporter levelimporter, Player player)
		{
			
			tiles = new List<AnimationSprite> ();
			game.Add (this);

			//Tile render
			int y = 0;
			Console.WriteLine ("Loading Level");
			//Go trough the lines
			foreach (string s in levelimporter.GetLevel()) {
				int[] csvArray = s.Split (',').Select (int.Parse).ToArray ();
				int x = 0;

				//Go trought the cells
				foreach (int i in csvArray) {
					Tile tile = new Tile ("Sprites/tiles.png", 2, 3);
					if (i == 0) {
						tile.visible = false;
					
					}


					if (i == 1) {
						//make a coin
						Coin coin = new Coin ();
						coin.x = x;
						coin.y = y;
						this.AddChild (coin);
						tile.visible = false;
					} else if (i == 5) {
						//Spawn player here
						player.x = x;
						player.y = y - 8;
						tile.SetFrame (i - 1);
						tile.y = y;
						tile.x = x;
					} else {
						//set the tile to the correct frame
						tile.SetFrame (i - 1);
						tile.y = y;
						tile.x = x;
						tile.SetColor (1, 1, 1);
					}

					//render the tile
					this.AddChild (tile);
					tiles.Add (tile);

					x += 16;
				}
				y += 16;
			}
			return tiles;
		}
	}
}

