using System;
using GXPEngine;
using System.Drawing;
using System.IO;
using System.Text;
using System.Linq;

//Code convension
//Config is ALLCAPS
//private var is _variable


public class MyGame : Game
{
 
	public MyGame () : base (1280, 720, false)
	{
		MainMenu menu = new MainMenu();
		AddChild (menu);
	}

	void Update ()
	{	
	}

	static void Main ()
	{
		new MyGame ().Start ();

	}
		
}
