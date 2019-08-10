//Author: Michael Li
////Course: CPSC223N
//Assignment number: 4
//Due date: November 6, 2017

//Source files in this program: main.cs ,frame.cs
//Purpose of this entire program:  Demonstrate how to create an UI that animates traveling ball based on user's desired speed and directoin
//Purpose of this file: Create Traveling Ball Object to run the application
//
//1. frame.cs - compiles into frame.dll
//2. main.cs - compiles with frame.cs to create TravelingBall.exe

using System;
using System.Windows.Forms;            //Needed for "Application" near the end of Main function.
public class Movingball
{  public static void Main()
   {  System.Console.WriteLine("The animated ball moving program will begin now.");
      TravelingBall motionapplication = new TravelingBall();
      Application.Run(motionapplication);
      System.Console.WriteLine("This animated program has ended.  Bye.");
   }//End of Main function
}//End of class

