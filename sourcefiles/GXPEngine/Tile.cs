using System;
using GXPEngine;

namespace GXPEngine
{
	public class Tile : AnimationSprite
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="GXPEngine.Tile"/> class.
		/// </summary>
		/// <param name="filename">Filename.</param>
		/// <param name="cols">Cols.</param>
		/// <param name="rows">Rows.</param>
		public Tile (string filename, int cols, int rows) : base(filename, cols, rows)
		{
		}

	}
}

