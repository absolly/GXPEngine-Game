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
		private int _airFrame = 0;
		private bool _firstFrame = true;
		private float _groundHitSpeed;
		private bool _isDead;
		private int wallJumpFrame = 5;
		private float wallJumpDirection;
		private float previousWallJumpDirection;
		private float _gravityTick;

		private enum playerState
		{
			Idle,
			Running,
			Sliding,
			InAir,
			Dying,

		}

		playerState _playerState;

		//http://opengameart.org/content/mv-platformer-male-32x64
		public Player () : base ("ninja_full.png", 10, 3)
		{	
			_velocityY = 2.0f;
			_moveSpeed = 4;
			_frame = 0.0f;
			_grounded = false;
			SetFrame (1);
		}

		void Update ()
		{
			

			_grounded = false;
			_frame += 0.5f;
			_gravityTick += 1;


			if (!_isDead) {
				checkSpecialColisions ();

				_playerState = playerState.InAir;

				applyGravity ();
				applyPlayerMovement ();


				switch (_playerState) {
				case playerState.Idle:
					Idle ();
					break;
				case playerState.Running:
					Running ();
					break;
				case playerState.InAir:
					InAir ();
					break;
				case playerState.Sliding:
					Sliding ();
					break;
				
				}
				wallJump ();

			} 
			checkFallDamage ();
		}

		private void Idle ()
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


		private void InAir ()
		{
			if (_velocityY < 0) {
				currentFrame = 17;
				if (_airFrame >= 2) {
					currentFrame = 18;
				}
				_airFrame++;
			}
			if (_velocityY > 1.0f && !_grounded) {
				currentFrame = 19;
				_airFrame = 0;
			}
		}

		private void Sliding ()
		{
			_velocityY -= 2.0f;
			if (_velocityY < 3.0f) {
				_velocityY = 3.0f;

			}
			currentFrame = 20;
			if (Input.GetKey (Key.SPACE) || Input.GetKeyDown (Key.UP)) {
				wallJumpFrame = 1;
				wallJumpDirection = -scaleX;

			}
		}

		private void wallJump ()
		{
			if (wallJumpFrame < 8 && wallJumpDirection != previousWallJumpDirection) {
				_velocityY -= 7.0f / wallJumpFrame;
				if (_velocityY < -11.0f) {
					_velocityY = -11.0f;
				}
				wallJumpFrame++;
			} else {
				previousWallJumpDirection = wallJumpDirection;
				wallJumpFrame = 6;
			}
		}

		private void checkFallDamage ()
		{
			if ((_grounded && _groundHitSpeed >= 15.5f) || _isDead) {
				_isDead = true;


				Console.WriteLine ("dead");
				if (_firstFrame) {
					//http://opengameart.org/content/wall-impact
					new Audio ("Audio/Hitting Wall.wav", false, false);
					currentFrame = 22;
					_firstFrame = false;
				} else {
					
					_frame = 0.0f;
					NextFrame ();
					if (currentFrame > 25) {
						currentFrame = 25;
						Level level = parent as Level;
						level.ResetLevel ();
					}
				}
			}
		}

		/// <summary>
		/// Sets the score to current score + amount.
		/// </summary>
		/// <param name="setScore">Amount to add to score.</param>
		public void setScore (int setScore)
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

		private void setVelocityY (float velocityY)
		{
			_velocityY = velocityY;
		}

		/// <summary>
		/// Checks if a coin was picked up.
		/// </summary>
		private void checkSpecialColisions ()
		{
			foreach (GameObject other in GetCollisions()) {
				if (other is Coin) {
					Coin coin = other as Coin;
					coin.destoryCoin ();
					setScore (1);
				}
				if (other is Tile){
					Tile tile = other as Tile;
					if (tile.currentFrame == 3) {
						_isDead = true;
					}
				}
			}
		}

		private void applyGravity ()
		{

			_velocityY += 1.0f;

			if (_velocityY > 16.0f)
				_velocityY = 16.0f;
			
			if (move (0, _velocityY) == false) {
				_grounded = true;
				wallJumpDirection = 0.0f;
				_groundHitSpeed = _velocityY;
				_velocityY = 0.0f;
				_playerState = playerState.Idle;
			}
			_gravityTick = 0;


		}

		private void applyPlayerMovement ()
		{
			if (Input.GetKey (Key.LEFT)) {
				if (!Input.GetKey (Key.RIGHT) && move (-_moveSpeed, 0) == true) {
					if (_grounded) {
						_playerState = playerState.Running;
					}
					scaleX = -1;
					SetOrigin (width - 1, 0);
				} else if (!_grounded && (_velocityY > 2.0f || Input.GetKeyDown (Key.SPACE) || Input.GetKeyDown (Key.UP))) {
					_playerState = playerState.Sliding;
				}
			}


			if (Input.GetKey (Key.RIGHT)) {
				if (!Input.GetKey (Key.LEFT) && move (_moveSpeed, 0) == true) {
					if (_grounded) {
						_playerState = playerState.Running;
					}
					scaleX = 1;
					SetOrigin (0, 0);
				} else if (!_grounded && (_velocityY > 2.0f || Input.GetKeyDown (Key.SPACE) || Input.GetKeyDown (Key.UP))) {
					_playerState = playerState.Sliding;
				}

			}

			if ((Input.GetKeyDown (Key.UP) || Input.GetKeyDown (Key.Z) || Input.GetKeyDown (Key.SPACE)) && _grounded) {
				_velocityY -= 11.0f;

			}
		}

	}
}