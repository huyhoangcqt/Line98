using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BallType{
    Normal,
    Ghost,
    Bomb,
}

public enum BallColor{
    Red,
    Blue,
    Green,
    Tear,
    Yellow,
    Purple,
    Pink,
}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BallAttributeScriptableObject", order = 1)]
public class BallAttributeScriptableObject : ScriptableObject
{
    public BallType type;
    public BallColor color;
}
