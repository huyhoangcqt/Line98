using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAttribute : MonoBehaviour{
    private Point _posInMatrix;
    public Point position{
        get{
            return _posInMatrix;
        }
        set{
            _posInMatrix = value;
        }
    }
}
