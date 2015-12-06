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
		private int _score;
		private int _lives = 3;
		private int _airFrame;
		private bool _firstFrame = true;
		private float _groundHitSpeed;
		private bool _isDead;
		private int wallJumpFrame = 5;
		private float wallJumpDirection;
		private float previousWallJumpDirection;

		private Sound _footStepSound;
		private Sound _coinPickupSound;
		private Sound _slideSound;
		private SoundChannel _footStepChannel;
		private SoundChannel _slideChannel;

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
		public Player (int lives) : base ("Sprites/ninja_full.png", 10, 3)
		{


			_lives = lives;
			_moveSpeed = 4;
			_frame = 0.0f;
			_grounded = false;
			SetFrame (1);

			//http://opengameart.org/content/platformer-sounds-terminal-interaction-door-shots-bang-and-footsteps
			new Sound ("Audio/start.ogg").Play();

			//http://opengameart.org/content/platformer-sounds-terminal-interaction-door-shots-bang-and-footsteps
			_footStepSound = new Sound ("Audio/steps_platform.ogg", true);
			_footStepChannel = _footStepSound.Play (true);

			//http://opengameart.org/content/level-up-power-up-coin-get-13-sounds
			_coinPickupSound = new Sound ("Audio/Coin01.aif");

			//http://www.freesound.org/people/semccab/sounds/154403/
			_slideSound = new Sound ("Audio/slide.wav", true);
			_slideChannel = _slideSound.Play (true);

		}

		/// <summary>
		/// Raises the destroy event.
		/// also stops sound played by player
		/// </summary>
		protected override void OnDestroy ()
		{
			base.OnDestroy ();
			_footStepChannel.Stop ();
			_slideChannel.Stop ();
		}

		void Update ()
		{
			//if level isn't visible stop movement
			if (parent != null && !parent.visible)
				return;

			//if sliding play sliding sound
			if (_playerState == playerState.Sliding) {
				_slideChannel.IsPaused = false;
			} else {
				_slideChannel.IsPaused = true;
			}

			//if running play running sound
			if (_playerState == playerState.Running) {
				_footStepChannel.IsPaused = false;
			} else {
				_footStepChannel.IsPaused = true;
			}

			//stop player movement if dead
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
			checkDamage ();
		}


		/// <summary>
		/// Default animation
		/// </summary>
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
			_moveSpeed = 4;
			if (_velocityY < 0) {
				currentFrame = 17;

				//bunny hop
				_moveSpeed = 6;
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
				_velocityY -= 9.0f / wallJumpFrame;
				if (_velocityY < -6.0f) {
					_velocityY = -6.0f;
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
		private void checkDamage ()
		{
			
			if ((_grounded && _groundHitSpeed >= 15.5f) || _isDead) {
				_isDead = true;


				Console.WriteLine ("dead");
				if (_firstFrame) {
					//http://opengameart.org/content/wall-impact
					new Sound ("Audio/Hitting Wall.wav", false, false).Play();
					currentFrame = 22;
					_firstFrame = false;
				} else {
					
					_frame = 0.0f;
					NextFrame ();
					if (currentFrame > 25) {
						currentFrame = 25;
						_lives -= 1;
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
			_score = setScore;
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
		/// Sets the score to current score + amount.
		/// </summary>
		/// <param name="setScore">Amount to add to score.</param>
		public void setLives (int setLives)
		{
			_lives = setLives;
		}

		/// <summary>
		/// Gets the score.
		/// </summary>
		/// <returns>The score.</returns>
		public int getLives ()
		{
			return _lives;
		}

		public void resetPlayer (){
			_score = 0;
			_isDead = false;
			_playerState = playerState.Idle;

			_moveSpeed = 4;
			_frame = 0.0f;
			_grounded = false;
			SetFrame (1);

		}

		/// <summary>
		/// Checks if a coin was picked up.
		/// </summary>
		private void checkSpecialColisions ()
		{
			foreach (GameObject other in GetCollisions()) {
				if (other is Coin) {
					_coinPickupSound.Play ();
					Coin coin = other as Coin;
					coin.Destroy ();
					_score += 1;
				}
				if (other is Tile) {
					Tile tile = other as Tile;
					if (tile.currentFrame == 3) {
						_isDead = true;
					}
					if (tile.currentFrame == 1) {
						new Sound ("Audio/end.ogg").Play();
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
					SetOrigin (width-1, 0);
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
				_velocityY -= 6.0f;

			}
		}

	}
}