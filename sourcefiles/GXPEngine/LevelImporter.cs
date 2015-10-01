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

		public string[] GetLevel ()
		{

			//Load the file
			Console.WriteLine ("Loading Level File");
			string csvPath = "Level1.txt";
			string csvContents = File.ReadAllText (csvPath);

			//Split the file into lines
			Console.WriteLine ("Parsing LevelData");
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

