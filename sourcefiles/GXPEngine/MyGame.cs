/*
                                                made by Tiemen van Egmond
                                                
                     `ss/  y////////+-     :+///////+/`      -/////////:      +/           /+      o/         `s:         
                    /s`++  d`        /y   y:         `    .o+-         .+o.   o+           /o       /s`      :y.          
                  .y:  ++  d`         h. `d              +s`              +o  o+           /o        `s/   `o+            
                 oo`   ++  d`       .o+   -o+-          :y                 ++ o+           /o          /s`-y.             
               -y-     ++  d+//////oy+-      -/////:`   s:                 .h o+           /o           `hs               
             `s+       ++  d`         :y.          `:o/ :y                 o+ o+           /o            y:               
            /y+////////y+  d`          :s             s/ +s`              oo  o+           /o            y:               
          .s:          ++  d`         .y-`s-         :y`  .o+-         .+o.   o+           /o            y:               
         :o`           //  y/////////+/    -+///////+-       -/////////:      +s/////////- :s/////////:  o-                

													www.absolly.me
*/

using System;
using GXPEngine;

public class MyGame : Game
{

	/// <summary>
	/// Initializes a new instance of the <see cref="MyGame"/> class.
	/// </summary>
	public MyGame () : base (1280, 720, false)
	{
		//load the main menu
		MainMenu menu = new MainMenu ();
		AddChild (menu);

		//http://chrishurn.com/mega-music-giveaway.html
		SoundChannel music = new Sound ("Audio/music.ogg", true).Play ();
		music.Volume = 0.2f;
	}

	/// <summary>
	/// The entry point of the program, where the program control starts and ends.
	/// </summary>
	static void Main ()
	{
		new MyGame ().Start ();

	}
		
}
