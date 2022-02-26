using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = "PositionData", menuName = "ScriptableObjects/PositionMatrixScriptableObject", order = 1)]
public class PositionMatrixScriptableObject : ScriptableObject
{
    [SerializeField] private CustomArray[] values;
}

[System.Serializable]
public class CustomArray {
	public Vector2[] value;
}
