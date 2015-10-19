using System;

namespace GXPEngine
{
	public class Audio : Sound
	{
		private SoundChannel _bgAudio;
		public Audio (string source, bool looping, bool streaming) : base(source, looping, streaming)
		{
			
			_bgAudio = Play();
			_bgAudio.Frequency = 44100f;

		}
	}
}

