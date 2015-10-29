using System;
using System.IO;
using System.Text;
using System.Linq;

namespace GXPEngine
{
	public class LevelImporter : GameObject
	{
		private string[] _linesArray;

		public LevelImporter (string level = "Level1") : base ()
		{
			game.Add (this);
			//Load the file
			Console.WriteLine ("Loading Level File");
			string csvPath = level + ".txt";
			string csvContents = File.ReadAllText (csvPath);

			//Split the file into lines
			try { /* UNIX line endings */
				string[] dataArray = csvContents.Split (new[]{ "data=\n" }, StringSplitOptions.None);
				_linesArray = dataArray [1].Split (new[]{ ",\n" }, StringSplitOptions.None);
				Console.WriteLine ("Mac/Linux Line Endings Detected");
			} catch { /* Windows line endings */
				string[] dataArray = csvContents.Split (new[]{ "data=\r\n" }, StringSplitOptions.None);
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

		public string[] GetLevels ()
		{

			string[] fileNames = Directory.GetFiles (Directory.GetCurrentDirectory (), "Level*.txt");
			for (int i = 0; i < fileNames.Length; i++) {
				fileNames [i] = Path.GetFileName (fileNames [i]).Replace (".txt", "");
			}
			return fileNames;
		}

	}
}

