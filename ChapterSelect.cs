using Godot;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

public partial class ChapterSelect : Node2D
{
	// Paths to the executables
	private Dictionary<string, string> projectPaths = new Dictionary<string, string>(){
		{"Chapter 1", "res://Chapters/Chapter1/Prueba"}
	};

	public override void _Ready()
	{
		// To keep it simple, it creates a button for each chapter [I'm lazy and don't want to create a GUI :)].
		foreach (var project in projectPaths)
		{
			Button button = new Button
			{
				Text = "Execute Project " + project.Key
			};

			string projectPath = project.Value;

			if (OS.GetName() == "Windows")
			{
				projectPath += ".exe";
			}
			else if (OS.GetName() == "X11" || OS.GetName() == "Linux")
			{
				projectPath += ".x86_64";
			}
			else if (OS.GetName() == "OSX")
			{
				// I don't have a Mac hehe.
			}
			else
			{
				// Just in case it doesn't get recognized
				GD.PrintErr("Unknown OS: " + OS.GetName());
				return;
			}

			// Connecting the button's Pressed signal to the OnButtonPressed method with the project path as an argument
			button.Pressed += () => OnButtonPressed(ProjectSettings.GlobalizePath(projectPath));

			AddChild(button);
		}
	}

	private void OnButtonPressed(string projectPath)
	{
		GD.Print("Trying to start game: " + projectPath);

		// Checking if the file exists
		if (!File.Exists(projectPath))
		{
			GD.PrintErr("File doesn't exist: " + projectPath);
			return;
		}

		try
		{
			// If other options are needed (could be removed)
			ProcessStartInfo startInfo = new ProcessStartInfo
			{
				FileName = projectPath
			};

			Process process = Process.Start(startInfo);
			GD.Print("Process started successfully: " + process.Id); // Printing the PID of the process
			GetTree().Quit(); // Closing the chapter select window cuz it's not needed anymore
		}
		catch (System.Exception error)
		{
			GD.PrintErr("Error trying to start the process: " + error.Message); // Just in case something fails
		}
	}
}
