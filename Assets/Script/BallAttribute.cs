using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAttribute : MonoBehaviour
{
    [SerializeField] private BallAttributeScriptableObject ballAttribute;
    // private Point positionInMatrix;
    public static int selectedCount = 0;

    // public void SetPosition(Point pos){
    //     positionInMatrix = pos;
    // }

    public void SetAttribute(BallAttributeScriptableObject value){
        ballAttribute = value;
    }
}
