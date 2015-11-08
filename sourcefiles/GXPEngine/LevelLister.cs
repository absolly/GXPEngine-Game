using System;
using System.IO;


namespace GXPEngine
{
	public class LevelLister
	{
		public LevelLister ()
		{
		}

		/// <summary>
		/// Gets the level names.
		/// </summary>
		/// <returns>An string array with the level file names.</returns>
		public string[] GetLevels ()
		{
			//get the file paths of files matching the filter
			string[] fileNames = Directory.GetFiles (Directory.GetCurrentDirectory (), "Levels/Level*.txt");

			//get the file names of the given files and remove the suffix
			for (int i = 0; i < fileNames.Length; i++) {
				fileNames [i] = Path.GetFileName (fileNames [i]).Replace (".txt", "");
			}

			return fileNames;
		}
	}
}

