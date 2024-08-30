using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBuildInfosTest : MonoBehaviour
{
#if UNITY_EDITOR
    [Oisif.Tooling.Button]
    public void LogShortHash()
    {
        Git.RetrieveCurrentCommitShorthash();
    }

    [Oisif.Tooling.Button]
    public void LogBranch()
    {
        Git.GetCurrentBranch();
    }
#endif
}
