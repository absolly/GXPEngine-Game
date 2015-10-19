using System;
using System.Drawing;

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
		private float _groundHitSpeed;
		private bool _isDead;
		private PointF _spawnPos;
	

		private enum _playerState
		{
			Idle,
			Running,
			Duck,
			Sliding,
			Falling,
			Dying,

		}
		_playerState playerstate;

		//http://opengameart.org/content/mv-platformer-male-32x64
		public Player () : base ("ninja_full.png", 10, 3)
		{	
			_spawnPos = new PointF (x, y);
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
			if (_velocityY > 16.0f)
				_velocityY = 16.0f;

			_grounded = false;
			_frame = _frame + 0.5f;


			if (!_isDead) {
			playerstate = _playerState.Falling;

			if (move (0, _velocityY) == false) {
				_grounded = true;
				_groundHitSpeed = _velocityY;
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
				if (move (-_moveSpeed, 0) == true) {
					if (_grounded) {
						playerstate = _playerState.Running;
					}
					scaleX = -1;
					SetOrigin (width, 0);
				} else if(!_grounded && _velocityY > 1.0f){
					playerstate = _playerState.Sliding;
				}
			}


			if (Input.GetKey (Key.RIGHT) && !_ducking) {
				if (move (_moveSpeed, 0) == true) {
					if (_grounded) {
     						playerstate = _playerState.Running;
					}					scaleX = 1;
					SetOrigin (0, 0);
				} else if(!_grounded && _velocityY > 2.0f){
					playerstate = _playerState.Sliding;
				}

			}
			if ((Input.GetKeyDown (Key.UP) || Input.GetKeyDown (Key.Z) || Input.GetKeyDown (Key.SPACE)) && _grounded && !_ducking) {
				_velocityY -= 9.0f;

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
				case _playerState.Sliding:
					Sliding ();
					break;
				
				}
			} 
			checkFallDamage ();
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
		void Sliding (){
			_velocityY -= 0.8f;
			currentFrame = 20;
		}

		void checkFallDamage(){
			if ((_grounded && _groundHitSpeed >= 16.0f) || _isDead) {
				_isDead = true;


				Console.WriteLine ("dead");
				if (firstFrame) {
					//http://opengameart.org/content/wall-impact
					new Audio ("Audio/Hitting Wall.wav",false,false);
					currentFrame = 21;
					firstFrame = false;
				}else{
					
					_frame = 0.0f;
					NextFrame ();
					if (currentFrame > 25) {
						currentFrame = 25;
					}
				}
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
				if (other is Coin && other.x > (x-16) && other.x < (x+16) && other.y > (y-16) && other.y < (y+32)) {
					Coin coin = other as Coin;
					coin.destoryCoin ();
					pickedUp = true;
				}
			}
			return pickedUp;
		}
	}
}