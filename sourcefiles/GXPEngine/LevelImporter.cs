using System;
using System.IO;
using System.Text;
using System.Linq;

namespace GXPEngine
{
	public class LevelImporter
	{
		public LevelImporter ()
		{

		}

		/// <summary>
		/// Reads the level from the file.
		/// </summary>
		/// <returns>The level array.</returns>
		public string[] GetLevel ()
		{

			//Load the file
			Console.WriteLine ("Loading Level File");
			string csvPath = "Level3.txt";
			string csvContents = File.ReadAllText (csvPath);

			//Split the file into lines
			try { /* UNIX line endings */
				string[] dataArray = csvContents.Split (new[]{ "data=\n" }, StringSplitOptions.None);
				string[] linesArray = dataArray [1].Split (new[]{ ",\n" }, StringSplitOptions.None);
				Console.WriteLine("Mac/Linux Line Endings Detected");
				return linesArray;
			} catch { /* Windows line endings */
				string[] dataArray = csvContents.Split (new[]{ "data=\r\n" }, StringSplitOptions.None);
				string[] linesArray = dataArray [1].Split (new[]{ ",\r\n" }, StringSplitOptions.None);
				Console.WriteLine("Windows Line Endings Detected");
				return linesArray;
			}
				
		 	 
		}

	}
}

