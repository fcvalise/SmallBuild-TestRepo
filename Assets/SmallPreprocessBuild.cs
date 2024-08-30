#if UNITY_EDITOR
using System.IO;
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
    }
}
#endif