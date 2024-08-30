#if UNITY_EDITOR
using System;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class BuildInfos
{
    public int callbackOrder => 1;

    public static void ModifySettings(BuildReport report, string shortHash, string branch)
    {
        var originalProductName = PlayerSettings.productName.Split('_')[0];
        var formattedDate = DateTime.Now.ToString("yyMMdd_HHmm");

        PlayerSettings.productName = $"{originalProductName}_{branch}_{shortHash}_{formattedDate}";
        PlayerSettings.bundleVersion = $"{shortHash}{branch}";
        PlayerSettings.applicationIdentifier = $"com.{PlayerSettings.companyName}.{originalProductName}_{shortHash}_{formattedDate}";

        switch (report.summary.platform)
        {
            case BuildTarget.StandaloneWindows64:
            case BuildTarget.StandaloneLinux64:
            case BuildTarget.StandaloneWindows:
            case BuildTarget.StandaloneOSX:
                // PlayerSettings.macOS.buildNumber = IncrementBuildNumber(PlayerSettings.macOS.buildNumber);
                // PlayerSettings.SetApplicationIdentifier(report.summary.platform, )
                break;
            case BuildTarget.iOS:
                // PlayerSettings.iOS.buildNumber = IncrementBuildNumber(PlayerSettings.iOS.buildNumber);
                break;
            case BuildTarget.Android:
                // PlayerSettings.Android.bundleVersionCode++;
                break;
        }
    }

    public static string GetBuildName(string fullPath, string shortHash, string branch)
    {
        string directory = Path.GetDirectoryName(fullPath);
        string fileName = Path.GetFileNameWithoutExtension(fullPath);
        string extension = Path.GetExtension(fullPath);
        string newFileName = $"{fileName}_{branch}_{shortHash}{extension}";
        string newFullPath = Path.Combine(directory, newFileName);

        return newFullPath;
    }

    public static void RenameBuild(string fullPath, string shortHash, string branch)
    {
        var newFullPath = GetBuildName(fullPath, shortHash, branch);

        if (File.Exists(fullPath))
        {
            File.Move(fullPath, newFullPath);
        }
        else if (Directory.Exists(fullPath))
        {
            Directory.Move(fullPath, newFullPath);
        }
    }
}
#endif