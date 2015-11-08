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

		private enum playerState
		{
			Idle,
			Running,
			Sliding,
			InAir,
			Dying,

		}

		playerState _playerState;

		/// <summary>
		/// Initializes a new instance of the <see cref="GXPEngine.Player"/> class.
		/// Sprite: http://opengameart.org/content/mv-platformer-male-32x64
		/// </summary>
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
			//if level isn't visible stop movement
			if (!parent.visible)
				return;
			
			if (!_isDead) {
				_grounded = false;
				_frame += 0.5f;

				checkSpecialColisions ();

				//fallback playerstate
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


		//Default animation
		private void Idle ()
		{
			currentFrame = 0;
		}

		/// <summary>
		/// Running animation.
		/// </summary>
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

		/// <summary>
		/// Inair animation.
		/// </summary>
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

		/// <summary>
		/// Sliding animation.
		/// </summary>
		private void Sliding ()
		{
			_velocityY -= 2.0f;
			if (_velocityY < 4.0f) {
				_velocityY = 4.0f;

			}
			currentFrame = 20;

			//if sliding and press space , try to walljump
			if (Input.GetKey (Key.SPACE) || Input.GetKeyDown (Key.UP) || Input.GetKeyDown (Key.Z)) {
				wallJumpFrame = 1;
				wallJumpDirection = -scaleX;

			}
		}

		/// <summary>
		/// Walljump animation
		/// </summary>
		private void wallJump ()
		{
			//if walljumped from oposite wall
			if (wallJumpFrame < 8 && wallJumpDirection != previousWallJumpDirection) {
				_velocityY -= 7.0f / wallJumpFrame;
				if (_velocityY < -8.0f) {
					_velocityY = -8.0f;
				}

				wallJumpFrame++;
			} else {
				//disable walljump until both requirements are met again
				previousWallJumpDirection = wallJumpDirection;
				wallJumpFrame = 8;
			}
		}

		/// <summary>
		/// Checks the fall damage, plays the impact sound if died.
		/// </summary>
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

		/// <summary>
		/// Checks if a coin was picked up.
		/// </summary>
		private void checkSpecialColisions ()
		{
			foreach (GameObject other in GetCollisions()) {
				if (other is Coin) {
					Coin coin = other as Coin;
					coin.Destroy ();
					setScore (1);
				}
				if (other is Tile) {
					Tile tile = other as Tile;
					if (tile.currentFrame == 3) {
						_isDead = true;
					}
					if (tile.currentFrame == 1) {
						Console.WriteLine ("Finished Level");
						Level level = parent as Level;
						level.NextLevel ();
					}
				}
			}
		}

		/// <summary>
		/// Applies the gravity, also sets the grounded state.
		/// </summary>
		private void applyGravity ()
		{
			//minimum acceleration, makes grounded state not flip out
			if (_velocityY >= 0.0f && _velocityY < 1.0f)
				_velocityY += 1.0f;
			else
				_velocityY += 0.5f;

			//maximum downward velocity, higher then 16 will make you go trough tiles(they have a 16px height)
			if (_velocityY > 16.0f)
				_velocityY = 16.0f;

			//try to move down, if you can't set grounded
			if (move (0, (int)_velocityY) == false) {
				_grounded = true;
				wallJumpDirection = 0.0f;
				_groundHitSpeed = _velocityY;
				_velocityY = 0.0f;
				_playerState = playerState.Idle;
			}
		}

		/// <summary>
		/// Applies the player movement according to user input.
		/// </summary>
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
				_velocityY -= 8.0f;

			}
		}

	}
}