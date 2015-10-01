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
		List<AnimationSprite> tiles;



		public TileRenderer () : base ()
		{
		}

		public List<AnimationSprite> GetTiles (LevelImporter levelimporter)
		{
			
			tiles = new List<AnimationSprite> ();
			game.Add (this);

			//Tile render
			int y = 0;
			Console.WriteLine ("Loading Level");
			//Go trough the lines
			foreach (string s in levelimporter.GetLevel()) {
				int[] csvArray = s.Split (',').Select (int.Parse).ToArray ();
//				Console.WriteLine (s);
				int x = 0;

				//Go trought the cells
				foreach (int i in csvArray) {
					Tile tile = new Tile ("colors.png", 2, 3);
					if (i == 0) {
						tile.visible = false;
					} else {
						tile.SetFrame (i-1);
					}
						
					tile.y = y;
					tile.x = x;

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

