#if UNITY_EDITOR
/* MIT License
Copyright (c) 2016 RedBlueGames
Code written by Doug Cox
*/

using System;
using System.IO;
using System.Diagnostics;
using UnityEngine;
using UnityEditor;
using System.Text;

public static class Git
{
    public static string RunGitCommand(string gitCommand)
    {
        // Strings that will catch the output from our process.
        string output = "no-git";
        string errorOutput = "no-git";

        // Set up our processInfo to run the git command and log to output and errorOutput.
        ProcessStartInfo processInfo = new ProcessStartInfo("git", @gitCommand)
        {
            CreateNoWindow = true,          // We want no visible pop-ups
            UseShellExecute = false,        // Allows us to redirect input, output and error streams
            RedirectStandardOutput = true,  // Allows us to read the output stream
            RedirectStandardError = true    // Allows us to read the error stream
        };

        // Set up the Process
        Process process = new Process
        {
            StartInfo = processInfo
        };

        try
        {
            process.Start();  // Try to start it, catching any exceptions if it fails
        }
        catch (Exception e)
        {
            // For now just assume its failed cause it can't find git.
            UnityEngine.Debug.LogError("Git is not set-up correctly, required to be on PATH, and to be a git project.");
            throw e;
        }

        // Read the results back from the process so we can get the output and check for errors
        output = process.StandardOutput.ReadToEnd();
        errorOutput = process.StandardError.ReadToEnd();

        process.WaitForExit();  // Make sure we wait till the process has fully finished.
        process.Close();        // Close the process ensuring it frees it resources.

        // Check for failure due to no git setup in the project itself or other fatal errors from git.
        if (output.Contains("fatal") || output == "no-git" || output == "")
        {
            UnityEngine.Debug.LogError("Command: git " + @gitCommand + " Failed\n" + output + errorOutput);
        }
        // Log any errors.
        if (errorOutput != "")
        {
            UnityEngine.Debug.LogError("Git Error: " + errorOutput);
        }

        return output;  // Return the output from git.
    }

    public static string RetrieveCurrentCommitShorthash()
    {
        var result = RunGitCommand("rev-parse --short --verify HEAD");
        // Clean up whitespace around hash. (seems to just be the way this command returns :/ )
        var shortHash = string.Join("", result.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
        UnityEngine.Debug.Log("Current Commit: " + shortHash);
        return shortHash;
    }

    public static string GetCurrentBranch()
    {
        var result = RunGitCommand("branch");
        var branch = result.Replace("* ", "").Trim();
        UnityEngine.Debug.Log("Current Branch: " + branch);
        return branch;
    }

    public static bool IsCleanStatus()
    {
        var result = RunGitCommand("status -s");
        return string.IsNullOrEmpty(result);
    }
}
#endif