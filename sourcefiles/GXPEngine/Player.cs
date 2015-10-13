using System;

namespace GXPEngine
{
	public class Player : MoveableSprite
	{
		private float _frame = 1;
		private float _velocityY;
		private int _moveSpeed;
		private bool _grounded;
		private int _score = 0;
		int i = 0;
		bool firstFrame = true;
		private bool _ducking;
		const int originalHeight = 32;

	

		private enum _playerState
		{
			Idle,
			Running,
			Duck,
			Dying,
			Falling,

		}
		_playerState playerstate;

		//http://opengameart.org/content/mv-platformer-male-32x64
		public Player () : base ("ninja_full.png", 10, 2)
		{
			_velocityY = 2.0f;
			_moveSpeed = 4;
			_frame = 0.0f;
			_grounded = false;
			SetFrame (1);
		}

		void Update ()
		{
			
			checkPickup ();
			_velocityY += 1.0f;
			if (_velocityY > 16)
				_velocityY = 16;

			_grounded = false;
			_frame = _frame + 0.5f;


			playerstate = _playerState.Falling;
			if (move (0, _velocityY) == false) {
				_grounded = true;
				_velocityY = 0.0f;
				playerstate = _playerState.Idle;
			}
			if (Input.GetKey (Key.DOWN)) {
				playerstate = _playerState.Duck;
				_ducking = true;
			} else {
				_ducking = false;
				firstFrame = true;

			}
			if (Input.GetKey (Key.LEFT) && !_ducking) {
				move (-_moveSpeed, 0);
				playerstate = _playerState.Running;
				scaleX = -1;
				SetOrigin (width, 0);
			}
			if ((Input.GetKeyDown (Key.UP) || Input.GetKeyDown (Key.Z) || Input.GetKeyDown (Key.SPACE)) && _grounded && !_ducking) {
				_velocityY -= 9.0f;
	
			}
			if (Input.GetKey (Key.RIGHT) && !_ducking) {
				move (_moveSpeed, 0);
				playerstate = _playerState.Running;
				scaleX = 1;
				SetOrigin (0, 0);

			}

			if (checkPickup ()) {
				setScore (1);
			}

			switch (playerstate) {
			case _playerState.Idle:
				Idle ();
				break;
			case _playerState.Running:
				Running ();
				break;
			case _playerState.Duck:
				Duck ();
				break;
			case _playerState.Falling:
				Falling ();
				break;
				
			}
			Falling ();
		}

		void Idle ()
		{
			currentFrame = 0;
		}

		void Running ()
		{
			if (_frame > 1) {
				_frame = 0.0f;
				NextFrame ();
				if (currentFrame > 6) {
					currentFrame = 1;
				}
			}
		}

		void Duck ()
		{
			if (firstFrame) {
				currentFrame = 7;
				firstFrame = false;
			}else if (_frame > 1) {
				_frame = 0.0f;
				NextFrame ();
				if (currentFrame > 9) {
					currentFrame = 9;
				}
			}
		}

		void Falling ()
		{
			if (_velocityY < 0) {
				currentFrame = 17;
				if (i >= 2){
					currentFrame = 18;
				}
				i++;
			}
			if (_velocityY > 1.0f && !_grounded) {
				currentFrame = 19;
				i = 0;
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

		public void setVelocityY (float velocityY)
		{
			_velocityY = velocityY;
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