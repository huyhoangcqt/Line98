using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = "PositionData", menuName = "ScriptableObjects/PositionMatrixScriptableObject", order = 1)]
public class PositionMatrixScriptableObject : ScriptableObject
{
    [SerializeField] public CustomArray[] values;

    public Vector2 GetValue(int x, int y){
        return values[x].GetValue(y);
    }
}

[System.Serializable]
public class CustomArray {
	public Vector2[] value;

    public Vector2 GetValue(int idx){
        return value[idx];
    }
}
