using System;
using System.IO;

namespace GXPEngine
{
	public class LevelImporter : GameObject
	{
		private string[] _linesArray;
		private string _csvContents;

		/// <summary>
		/// Initializes a new instance of the <see cref="GXPEngine.LevelImporter"/> class.
		/// </summary>
		/// <param name="level">Level.</param>
		public LevelImporter (int level = 0) : base ()
		{
			game.Add (this);

			//Load the file
			Console.WriteLine ("Loading Level File");
			string csvPath = "Levels/Level" + level + ".txt";
			_csvContents = File.ReadAllText (csvPath);
			

			//Split the file into lines
			try { /* UNIX line endings */
				string[] dataArray = _csvContents.Split (new[]{ "data=\n" }, StringSplitOptions.None);
				_linesArray = dataArray [1].Split (new[]{ ",\n" }, StringSplitOptions.None);
				Console.WriteLine ("Mac/Linux Line Endings Detected");
			} catch { /* Windows line endings */
				string[] dataArray = _csvContents.Split (new[]{ "data=\r\n" }, StringSplitOptions.None);
				_linesArray = dataArray [1].Split (new[]{ ",\r\n" }, StringSplitOptions.None);
				Console.WriteLine ("Windows Line Endings Detected");

			}
			
		}

		/// <summary>
		/// Returns a string array containing the lines of the leveldata.
		/// </summary>
		/// <returns>The level array.</returns>
		public string[] GetLevel ()
		{
			return _linesArray;
		}

//		public string[] GetLevels ()
//		{
//
//			string[] fileNames = Directory.GetFiles (Directory.GetCurrentDirectory (), "Levels/Level*.txt");
//			for (int i = 0; i < fileNames.Length; i++) {
//				fileNames [i] = Path.GetFileName (fileNames [i]).Replace (".txt", "");
//			}
//			return fileNames;
//		}

	}
}

