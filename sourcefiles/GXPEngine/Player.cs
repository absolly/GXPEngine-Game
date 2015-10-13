using System;

namespace GXPEngine
{
	public class Player : MoveableSprite
	{
		private float _frame;
		private float _velocityY;
		private int _moveSpeed;
		private bool _grounded;
		private int _score = 0;

		public Player () : base ("colors.png", 2, 3)
		{

			_velocityY = 1.0f;
			_moveSpeed = 8;
			_frame = 0.0f;
			_grounded = false;
			SetFrame (1);
			this.x = 0;
			this.y = 0;
		}

		void Update ()
		{
			checkPickup ();
			_velocityY += 1.0f;
			_grounded = false;
			_frame = _frame + 0.5f;
			if (_frame > frameCount) {
				_frame = 0.0f;
				NextFrame ();
				if (currentFrame > 3) {
					currentFrame = 0;
				}
			}
			if (move (0, _velocityY) == false) {
				_grounded = true;
				_velocityY = 0.0f;
			}

			if (Input.GetKey (Key.LEFT)) {
				move (-_moveSpeed, 0);
			}
			if ((Input.GetKeyDown (Key.UP) || Input.GetKeyDown (Key.Z) || Input.GetKeyDown (Key.SPACE)) && _grounded) {
				_velocityY -= 10;
	
			}
			if (Input.GetKey (Key.RIGHT)) {
				move (_moveSpeed, 0);
			}
			if (checkPickup ()) {
				setScore (1);
			}
		}

		/// <summary>
		/// Sets the score to current score + amount.
		/// </summary>
		/// <param name="setScore">Amount to add to score.</param>
		void setScore (int setScore)
		{
			_score += setScore;
		}

		/// <summary>
		/// Gets the score.
		/// </summary>
		/// <returns>The score.</returns>
		public int getScore ()
		{
			return _score;
		}


		/// <summary>
		/// Checks if a coin was picked up.
		/// </summary>
		/// <returns><c>true</c>, if pickup was detected, <c>false</c> otherwise.</returns>
		private bool checkPickup ()
		{
			bool pickedUp = false;
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