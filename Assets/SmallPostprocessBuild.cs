// #if UNITY_EDITOR
// using System.IO;
// using UnityEngine;
// using UnityEditor;
// using UnityEditor.Callbacks;

// public class SmallPostprocessBuild
// {
//     [PostProcessBuildAttribute(1)]
//     public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
//     {
//         var shortHash = Git.RetrieveCurrentCommitShorthash();
//         var branch = Git.GetCurrentBranch();
//         var buildName = BuildInfos.GetBuildName(pathToBuiltProject, shortHash, branch);
//         var canWrite = true;

//         if (File.Exists(buildName) || Directory.Exists(buildName))
//         {
//             canWrite = EditorUtility.DisplayDialog("Override", $"Do you want to override {buildName} ?", "Yes", "No");
//             if (canWrite)
//             {
//                 if (File.Exists(buildName))
//                 {
//                     File.Delete(buildName);
//                 }
//                 if (Directory.Exists(buildName))
//                 {
//                     Directory.Delete(buildName, true);
//                 }
//             }
//         }

//         if (canWrite)
//         {
//             BuildInfos.RenameBuild(pathToBuiltProject, shortHash, branch);
//         }
//     }
// }
// #endif