/*
 * Created by SharpDevelop.
 * User: citcer
 * Date: 06.11.2019
 * Time: 21:24
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;  
using System.IO;  
using System.Net;  
using System.Text;
using System.Diagnostics;
using ReflactionNamespace = System.Reflection;

namespace RouteWriter
{
	class Program
	{
		public static void Main(string[] args)
		{
			
			// Initiation Variables and Process		
			Process p = new Process();
			string AppPath =  System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName; // App full name
			string path = Path.GetDirectoryName(AppPath); // Extract dir from app fullname
			string val1 = "";
			DateTime datNow = DateTime.Now;
			string logFile = path +"\\" + datNow.ToString("yyyy.MM.dd") +".log";
			
 p.StartInfo.UseShellExecute = false;
 p.StartInfo.RedirectStandardOutput = true;
 p.StartInfo.FileName = path +"\\ping.cmd";
 // Start PING command via CMD.exe
 p.Start();
 string output = p.StandardOutput.ReadToEnd();
 p.WaitForExit();
 
 Console.OutputEncoding = System.Text.Encoding.UTF8;
 // read value of default route
 val1 = File.ReadAllText(path+"\\CurRoute.txt");
 if(output.ToLower().Contains("ttl=")) // PING success
 {
 	if(val1 == "2") // if value is ROUTE-2
 	{
 	File.WriteAllText(path+"\\CurRoute.txt","1"); // Change value to ROUTE-1
 	//System.Diagnostics.Process.Start(path+"\\ApplyRoute1.cmd"); // Execute changes
 	File.AppendAllText(logFile, datNow.ToString("yyyy.MM.dd HH:mm")+" ***  Route was changed: 2 -> 1" + Environment.NewLine);
 	}
 } else { 					// PING failed
 	if(val1 == "1") //if value ROUTE-1, changes to ROUTE-2
 	{
 	File.WriteAllText(path+"\\CurRoute.txt","2");
 	//System.Diagnostics.Process.Start(path+"\\ApplyRoute2.cmd");
 	File.AppendAllText(logFile, datNow.ToString("yyyy.MM.dd HH:mm")+" ***  Route was changed: 1 -> 2" + Environment.NewLine);
 	}
 }
 
 
		}
			
	}
}
	