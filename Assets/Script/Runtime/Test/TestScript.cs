using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YellowCat.Utils;


public class TestScript : MonoSingletonTemplate<TestScript>
{
    private void Start() {
        print(Camera.main.WorldToScreenPoint(transform.position));
        Debugger.Warning(LogTags.Battle, "This is a warning message from TestScript.");
    }
}
