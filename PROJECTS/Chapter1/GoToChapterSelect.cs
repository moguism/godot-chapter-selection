using Godot;
using System;
using System.Diagnostics;

public partial class GoToChapterSelect : Node
{

	private const string GAMENAME = "ChapterSelect"; // HERE YOU WOULD PUT THE NAME OF YOUR GAME EXECUTABLE (without ".exe", ".x86_64", etc.)

	public void ReturnToChapters()
	{
		string path = GetMainPath(OS.GetExecutablePath());

		// You could also make another function to get the name of the executable instead of having a variable, but this works too.
		if(OS.GetName() == "Windows")
		{
			path = path + GAMENAME + ".exe";
		}
		else if (OS.GetName() == "X11" || OS.GetName() == "Linux")
		{
			path = path + GAMENAME + ".x86_64";
		}
		else if (OS.GetName() == "OSX")
		{
			// Again, I don't have a Mac.
		}
		else
		{
			GD.PrintErr("Unknown OS: " + OS.GetName());
			return;
		}

		try
		{
			ProcessStartInfo startInfo = new ProcessStartInfo
			{
				FileName = path
			};

			Process process = Process.Start(startInfo);
			GD.Print("Process started successfully: " + process.Id);
			GD.Print("Returning to chapter select...");
		}
		catch (Exception error)
		{
			GD.PrintErr("Error trying to start the process: " + error.Message);
		}
	}

	// Obtains the main path of the game (where the chapter selection executable is)
	private static string GetMainPath(string path)
	{
		int index = path.Find("Chapters");
		if (index != -1)
		{
			return path.Substring(0, index);
		}
		return path;  // Returns the entire path if key word isn't found
	}
}
