#!/bin/bash
#Author: Michael Li
#Course: CPSC223n
#Semester: Fall 2017
#Assignment: 3
#Due: November 6, 2017.

echo First remove old binary files
rm *.dll
rm *.exe

echo View the list of source files
ls -l

#Creating dll file for frame.cs
mcs -target:library frame.cs -r:System.Windows.Forms.dll -r:System.Drawing.dll -out:frame.dll

#Executable program
mcs main.cs -r:System.Windows.Forms.dll -r:System.Drawing.dll -r:frame.dll -out:TravelingBall.exe

echo View the list of files in the current folder
ls -l

echo Run the Assignment 3 program.
./TravelingBall.exe

echo The script has terminated
