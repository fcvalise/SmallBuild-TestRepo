#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class BuildInfoMenuItem : MonoBehaviour
{
    public const string c_is_build_info = "is_build_info";

    static BuildInfoMenuItem()
    {
        if (!EditorPrefs.HasKey(c_is_build_info))
        {
            EditorPrefs.SetBool(c_is_build_info, true);
            Menu.SetChecked("Small Studio/Build Helper/Build Infos", true);
        }
    }

    [MenuItem("Small Studio/Build Helper/Build Infos")]
    static void EnableDisableBuildInfos()
    {
        var isBuildInfo = EditorPrefs.GetBool(c_is_build_info);
        EditorPrefs.SetBool(c_is_build_info, !isBuildInfo);
        Menu.SetChecked("Small Studio/Build Helper/Build Infos", !isBuildInfo);
    }

    public static bool IsActive()
    {
        return EditorPrefs.GetBool(c_is_build_info, true);
    }
}
#endif