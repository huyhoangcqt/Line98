using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionMatrix : Singleton<PositionMatrix>
{
    public PositionMatrixScriptableObject defaultPosition;

    public Vector2 GetValue(int x, int y){
        return defaultPosition.GetValue(x, y);
    }
}
