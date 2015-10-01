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
		Canvas canvas = new Canvas(game.width, game.height);
		canvas.graphics.FillRectangle(new SolidBrush(Color.FromArgb(125,106,148)), new Rectangle(0, 0, game.width, game.height));
		AddChild(canvas);

		Level level = new Level ();
		AddChild (level);
	}

	void Update ()
	{	

	}

	static void Main ()
	{
		new MyGame ().Start ();

	}
		
}
