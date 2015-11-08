using System;
using GXPEngine;

public class MyGame : Game
{

	/// <summary>
	/// Initializes a new instance of the <see cref="MyGame"/> class.
	/// </summary>
	public MyGame () : base (1280, 720, false)
	{
		MainMenu menu = new MainMenu();
		AddChild (menu);
	}

	/// <summary>
	/// The entry point of the program, where the program control starts and ends.
	/// </summary>
	static void Main ()
	{
		new MyGame ().Start ();

	}
		
}
