using System;

namespace GXPEngine
{
	public class MoveableSprite : AnimationSprite
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="GXPEngine.MoveableSprite"/> class.
		/// </summary>
		/// <param name="filename">Filename.</param>
		/// <param name="cols">Colums.</param>
		/// <param name="rows">Rows.</param>
		public MoveableSprite (string filename, int cols, int rows) : base (filename, cols, rows)
		{
		}

		/// <summary>
		/// Moves a character
		/// </summary>
		/// <param name="moveX">Move x.</param>
		/// <param name="moveY">Move y.</param>
		/// <returns><c>true</c>, If successful, <c>false</c> otherwise.</returns>
		protected bool move (float moveX, float moveY)
		{
			
			x = x + moveX;
			y = y + moveY;

			bool canMove = true;
			foreach (GameObject other in GetCollisions()) {
				canMove = canMove && handleCollision (other, moveX, moveY);
			}

			return canMove;
		}




		/// <summary>
		/// responds to a collision when it's encountered
		/// </summary>
		/// <returns><c>true</c>, If player should be allowed through, <c>false</c> otherwise.</returns>
		/// <param name="other">Other.</param>
		/// <param name="moveX">Move x.</param>
		/// <param name="moveY">Move y.</param>
		bool handleCollision (GameObject other, float moveX, float moveY)
		{
			if (other is Tile) {
				Tile tile = other as Tile;

				if (tile.currentFrame == 2) {
					//Slightly darken ground tiles you touch
					tile.SetColor (0.9f,0.9f,0.9f);

					//Slightly force the tile downward
					if(moveY >= 16)
					tile.y += 3;
					

					return resolveCollision (other as Sprite, moveX, moveY);
				}
			}
			return true;
		}


		/// <summary>
		/// Resolves the collision.
		/// </summary>
		/// <returns><c>true</c>, if collision was resolved, <c>false</c> otherwise.</returns>
		/// <param name="collisionObject">Collision object.</param>
		/// <param name="moveX">Move x.</param>
		/// <param name="moveY">Move y.</param>
		bool resolveCollision (Sprite collisionObject, float moveX, float moveY)
		{
			
			if (moveX > 0) {
				x = collisionObject.x - width;
				return false;
			}
			if (moveX < 0) {
				x = collisionObject.x + collisionObject.width;
				return false;
			}
			if ((moveY) > 0) {
				y = collisionObject.y - height;
				return false;
			}

			if ((moveY) < 0) {
				
				y = collisionObject.y + collisionObject.height;

			}
			return true;
		}



	}
}

