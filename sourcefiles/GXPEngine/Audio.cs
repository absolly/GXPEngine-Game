using System;

namespace GXPEngine
{
	public class Audio : Sound
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="GXPEngine.Audio"/> class.
		/// </summary>
		/// <param name="source">Audio source file.</param>
		/// <param name="looping">If set to <c>true</c> looping.</param>
		/// <param name="streaming">If set to <c>true</c> streaming.</param>
		public Audio (string source, bool looping, bool streaming) : base(source, looping, streaming)
		{
			// yes, i'm accually this lazy :p
			Play();
		}
	}
}

