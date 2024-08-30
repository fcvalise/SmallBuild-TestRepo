#if UNITY_EDITOR
using System.IO;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class SmallPreprocessBuild : IPreprocessBuildWithReport
{
    public int callbackOrder { get { return 0; } }

    public void OnPreprocessBuild(BuildReport report)
    {
        var shortHash = Git.RetrieveCurrentCommitShorthash();
        var branch = Git.GetCurrentBranch();

        BuildInfos.ModifySettings(report, shortHash, branch);
        WaitForBuildCompletion(report);
    }

    static async void WaitForBuildCompletion(BuildReport report)
    {
        while (report.summary.result == BuildResult.Unknown)
        {
            await Task.Delay(1000);
        }

        switch (report.summary.result)
        {
            case BuildResult.Cancelled:
                break;
            case BuildResult.Failed:
                break;
            case BuildResult.Succeeded:
                var path = BuildInfos.RenameBuild(report.summary.outputPath);
                BuildInfos.OpenFolder(path);
                break;
        }
    }
}
#endif