using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAttribute : MonoBehaviour
{
    [SerializeField] private BallAttributeScriptableObject _ballAttribute;
    // private Point positionInMatrix;
    public static int selectedCount = 0;

    // public void SetPosition(Point pos){
    //     positionInMatrix = pos;
    // }

    public BallAttributeScriptableObject ballAttribute{
        set {
            _ballAttribute = value;
        }
        get {
            return _ballAttribute;
        }
    }
}
