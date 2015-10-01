using System;

namespace GXPEngine
{
	public class MoveableSprite : AnimationSprite
	{
		const bool NOTLANDED = true;
		const bool LANDED = false;
		private bool pickedUp = false;


		public MoveableSprite (string filename, int cols, int rows) : base (filename, cols, rows)
		{
		}

		/*
		 *  move moves a character, return true if successful, false if not
		 *
		 */
		protected bool move (float moveX, float moveY)
		{
			x = x + moveX;
			y = y + moveY;

			bool canMove = NOTLANDED;
				
			foreach (GameObject other in GetCollisions()) {
				canMove = canMove && handleCollision (other, moveX, moveY);
			}

			return canMove;
		}

		/*
		 * 
		 *  responds to a collision when it's encountered, returns true if player should be allowed through
		 *  later on, replace with 'virtual' so enemies don't eat coins ie
		 */
		bool handleCollision (GameObject other, float moveX, float moveY)
		{
			if (other is Tile) {
				Tile tile = other as Tile;
				if (tile.visible != false) {
					return resolveCollision (other as Sprite, moveX, moveY);
				}
			}
			return NOTLANDED;
		}

		/* 
		 *    resolves a collision
		 */
		bool resolveCollision (Sprite collisionObject, float moveX, float moveY)
		{
			if (moveX > 0) {
				x = collisionObject.x - width;
				return LANDED;
			}
			if (moveX < 0) {
				x = collisionObject.x + collisionObject.width;
				return LANDED;
			}
			if ((moveY) > 0) {
				y = collisionObject.y - height;
				return LANDED;
			}
			if ((moveY) < 0) {
				y = collisionObject.y + collisionObject.height;
			}
			return NOTLANDED;
		}

	   /*
	   	*	checks if a coin is picked up, returns true and destroys coin if picked up
		*/
		protected bool checkPickup ()
		{
			foreach (GameObject other in GetCollisions()) {
				if (other is Coin) {
					Coin coin = other as Coin;
					coin.destoryCoin ();
					pickedUp = true;
				}
			}
			return pickedUp;
		}

	}
}

