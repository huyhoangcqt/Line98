using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAttribute : MonoBehaviour
{
    [SerializeField] private BallAttributeScriptableObject ballAttribute;
    private Point positionInMatrix;
    private bool isSelected;
    public static int selectedCount = 0;

    private void Awake() {
        isSelected = false;
    }

    public void SetPosition(Point pos){
        positionInMatrix = pos;
    }
}
