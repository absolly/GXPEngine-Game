using System;

namespace GXPEngine
{
	public class Coin : AnimationSprite
	{
		public Coin () : base("colors.png", 2, 3)
		{
			SetFrame (0);
		}

		/// <summary>
		/// Destroys the coin.
		/// </summary>
		public void destoryCoin(){
			this.Destroy();
		}
	}
}

