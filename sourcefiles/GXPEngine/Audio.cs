using System;

namespace GXPEngine
{
	public class Audio : GameObject
	{
		private SoundChannel _bgAudio;
		public Audio () : base()
		{
			Sound bgSound = new Sound ("tower.mp3", true, false);
			_bgAudio = bgSound.Play();
			_bgAudio.Frequency = 44100f;

		}
	}
}

