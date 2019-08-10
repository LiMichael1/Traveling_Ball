////Author: Michael Li
////Course: CPSC223N
//Assignment number: 4
//Due date: November 6, 2017

//Source files in this program: main.cs ,frame.cs
//Purpose of this entire program:  Demonstrate how to create an UI that animates traveling ball based on user's desired speed and directoin
//Purpose of this file: Create the UI for the traveling ball
//
//1. frame.cs - compiles into frame.dll
//2. main.cs - compiles with frame.cs to create TravelingBall.exe
//

//mcs -target:library frame.cs -r:System.Windows.Forms.dll -r:System.Drawing.dll -r:Twoanimatedlogic.dll -out:Twoobjectsframe.dll
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Timers;

public class TravelingBall: Form
{
   private const int formwidth = 830;
   private const int formheight = 700;
   private const int adjustment = 8;  
  
   private double ball_real_x = (double)(formwidth/2-(ball_radius-adjustment));
   private double ball_real_y = (double)(formheight/2-band_height/2-(ball_radius-adjustment)); 
   private const int origin_x = (formwidth/2-(ball_radius-adjustment));
   private const int origin_y = (formheight/2-band_height/2-(ball_radius-adjustment));
   private const double distance_moved_per_tic = 1.0;
   private int ball_int_x;
   private int ball_int_y;
   private double horizontal_delta;
   private double vertical_delta;
   //private double ball_angle_delta;

   private double radian = 1;

   private const int ball_radius = 15;
 
   
   //Clocks
   private static System.Timers.Timer graphic_area_refresh_clock = new System.Timers.Timer();
   private double graphic_area_refresh_rate = 60.0;
   private static System.Timers.Timer ball_control_clock = new System.Timers.Timer();
   //private double ball_update_rate = 45.0;
   private const int band_height = 100; 
   //text boxes
   private TextBox speed = new TextBox();
   private TextBox angle = new TextBox();
   //buttons
   private Button exitbutton = new Button();
   private Button startbutton = new Button();
   //title
   private Label title = new Label();
   //location
   private Point location_of_start = new Point(600, formheight -90);
   private Point location_of_exit = new Point(700, formheight - 90);
   private Point location_of_speed = new Point(50, formheight - 75);
   private Point location_of_angle = new Point(150, formheight - 75);
   private Point location_of_title = new Point(formwidth/2 - 125, 10);
   private Point location_of_topic = new Point (325, formheight - 90);
   private Point location_of_CSharp = new Point (325, formheight - 70);
   private Point location_of_Math = new Point(325, formheight - 50);
  

   //formation of messages
   private String Topic = "Coordinates of the center of the ball: ";
   private String CSharpSystem;
   private String MathSystem;
   //textbox input holders
   private double angle_input = 90;//after string is converted to int
   private double speed_input = 1 ;
   private String angle_string;//before string is converted to int
   private String speed_string;
   //Pen Desgin
   private Pen dashpen = new Pen(Color.Black, 5.0f);
   private float [] mydesign = { 4.0f, 2.0f};
   private Brush writing_tool = new SolidBrush(System.Drawing.Color.Black);
   private Font style_of_message = new System.Drawing.Font("Arial",10,FontStyle.Regular);

   //Math 
   private const double scale_factor = 50;
   private double Math_Point_x= 0;
   private double Math_Point_y= 0;

   //start button switch to pause
   private bool click = true;

   //button font
   private Font buttonfont = new System.Drawing.Font("Arial",12,FontStyle.Regular);

   protected double DegreeToRadian(double angle)
   {
       return Math.PI * angle / 180.0;
   }

   protected void CsharpToMath()
   {
	Math_Point_x = (ball_int_x - origin_x)/ scale_factor;
	Math_Point_y = (origin_y - ball_int_y)/ scale_factor;
   }
   public TravelingBall()
   {
      Text = "Traveling Ball";
      System.Console.WriteLine("formwidth = {0}. formheight = {1}.",formwidth,formheight);
      //Set the initial size of this form
      Size = new Size(formwidth,formheight);
      //Set the background color of this form
      BackColor = Color.White;
      //title
      title.Text = "Traveling Ball by Michael Li";
      title.Size = new Size(250, 25);
      title.Location = location_of_title;
      title.Font = new Font("Arial", 15);
      title.BackColor = Color.Yellow;
      //initial coordinates of ball
      ball_int_x = (int)ball_real_x;
      ball_int_y = (int)ball_real_y;
      //Pen
      dashpen.DashStyle = DashStyle.Solid;
      dashpen.DashPattern = mydesign;
      //Text Boxes 
      speed.Text = "Enter Speed";
      angle.Text = "Enter Angle";
      speed.Size = new Size(80,20);
      angle.Size = new Size(80,20);

      speed.Font = new Font("Arial", 9);
      angle.Font = new Font("Arial", 9);

      speed.Location = location_of_speed;
      angle.Location = location_of_angle;

      //start button
      startbutton.Text = "Start";
      startbutton.Size = new Size (80,50);
      startbutton.Location = location_of_start;
      startbutton.Font = buttonfont;

      //exit button
      exitbutton.Text = "Exit";
      exitbutton.Size = new Size (80,50);
      exitbutton.Location = location_of_exit;
      exitbutton.Font = buttonfont;
	//Clock
	graphic_area_refresh_clock.Enabled = false;
	graphic_area_refresh_clock.Elapsed += new ElapsedEventHandler(Update_display);
	ball_control_clock.Enabled = false;
	ball_control_clock.Elapsed += new ElapsedEventHandler(Update_ball);
        //Controls
        Controls.Add(title);
	Controls.Add(startbutton);
        Controls.Add(exitbutton);
	Controls.Add(speed);
	Controls.Add(angle);
	//Event handlers
	startbutton.Click += new EventHandler(Start);
	exitbutton.Click += new EventHandler(Exit);
	
	
       
   }//End of constructor

   protected override void OnPaint(PaintEventArgs ee)
   {  Graphics graph = ee.Graphics;
	//Horizontal Bands
      graph.FillRectangle(Brushes.MediumSpringGreen, 0, 0, formwidth, band_height/2);
      graph.FillRectangle(Brushes.Yellow, 0, formheight-band_height, formwidth, formheight-band_height);
      //Coordinate Lines
      graph.DrawLine(dashpen, (formwidth/2) , band_height/2 , (formwidth/2) , formheight-band_height);
      graph.DrawLine(dashpen, 0, (formheight/2)-50, formwidth, (formheight/2)-50);

      //message construction
	CSharpSystem = "C# System = ( " + ball_int_x + " , " + ball_int_y + " )";
	MathSystem = "Math System = ( " + Math_Point_x +" , " + Math_Point_y +" )";
      //message Printing
	graph.DrawString(Topic, style_of_message, writing_tool, location_of_topic);
	graph.DrawString(CSharpSystem, style_of_message,writing_tool, location_of_CSharp);
	graph.DrawString(MathSystem, style_of_message, writing_tool, location_of_Math);
      //ball
      //if(ballvisible)
	graph.FillEllipse(Brushes.Orange, ball_int_x, ball_int_y, ball_radius, ball_radius);
      base.OnPaint(ee);
   }
   protected void Update_display (System.Object sender, ElapsedEventArgs evt)
   {
	Invalidate();
	
	if(ball_control_clock.Enabled== false){
	graphic_area_refresh_clock.Enabled = false;
	startbutton.Text = "Start";
	}
   }

   protected void Update_ball(System.Object sender, ElapsedEventArgs evt)
   {    
      		ball_real_x = ball_real_x + horizontal_delta;//UPDATES THE X COORDINATES
		ball_real_y = ball_real_y - vertical_delta;//UPDATES THE Y COORDINATES
	
		ball_int_x = (int)System.Math.Round(ball_real_x);
		ball_int_y = (int)System.Math.Round(ball_real_y);
        
        	CsharpToMath();  //CONVERTS C SHARP COORDINATES INTO CARTESIAN COORDINATES

		if(ball_int_x >=formwidth || ball_int_x <= 0 || ball_int_y >= formheight || ball_int_y <= 0)
		{//IF IT IS OUT OF THE BOUNDARIES, IT STOPS THE CLOCK AND RETURNS THE CIRCLE TO THE ORIGIN. 
		ball_control_clock.Enabled = false;
		ball_int_x = origin_x; 
		ball_int_y = origin_y;
		ball_real_x = (double) ball_int_x;
		ball_real_y = (double) ball_int_y;
	
		}

   }
	//SPEED TEXT BOX
   protected void StringtoSpeed (String s)
   {    
	speed_string = s;
	speed_input= double.Parse(speed_string);  //CONVERTS THE STRING INTO TYPE DOUBLE
   }
	//ANGLE TEXT BOX
   protected void StringtoAngle (String s)
   {
	angle_string = s;
	angle_input= double.Parse(angle_string);
	//RADIANS FOR THE COS AND SIN INPUT
	radian = DegreeToRadian(angle_input);
	horizontal_delta = distance_moved_per_tic*(Math.Cos(radian));
	vertical_delta = distance_moved_per_tic*(Math.Sin(radian));
   }

   protected void Start (Object sender, EventArgs evts)
   {	
	if(ball_control_clock.Enabled==false)
	{
		StringtoSpeed(speed.Text);
		StringtoAngle(angle.Text);	
	
		double elapsedtimepertic = 1000.0/graphic_area_refresh_rate;
		graphic_area_refresh_clock.Interval = (int) elapsedtimepertic;

		double elapsedtimebetweenballmoves = 1000.0/speed_input;
		ball_control_clock.Interval = (int) elapsedtimebetweenballmoves; 

		ball_control_clock.Enabled = true;
		graphic_area_refresh_clock.Enabled = true;
		
		startbutton.Text = "Pause";
	}
	else
	{
		ball_control_clock.Enabled = false;
		startbutton.Text = "Start";
	}
        
   }
   protected void Exit (Object sender, EventArgs evts)
   {    
        System.Console.WriteLine("Program will now close.");
	Close();
   }

 
  
    	
}//end of class
   

