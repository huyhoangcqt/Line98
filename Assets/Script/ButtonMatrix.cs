using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ButtonMatrix : MonoBehaviour
{
    private int ballCount;
    private Matrix9x9<Transform> buttons;
    [SerializeField] private GameObject buttonPrefab, buttonCanvas;

    [SerializeField] private GameObject buttonTable;

    private void Awake() {
        // buttons = new Matrix9x9<Transform>();
        
        /**
         * * TO DO: find the reason that button position is don't same as canvasPosition;
         * first test: ref to the same cavans => Nothing changes.
        */
        // for (int i = 0; i < 9; i++){
        //     // string name = "row" + i;
        //     // GameObject newRow = new GameObject(name);
        //     // newRow.transform.SetParent(buttonCanvas.transform);
        //     for (int j = 0; j < 9; j++){
        //         Vector2 positionInWorldSpace = PositionMatrix.instance.GetValue(i, j);
        //         Vector3 transformationPos = new Vector3(positionInWorldSpace.x, positionInWorldSpace.y, -1);
        //         GameObject newButton = Instantiate(buttonPrefab, transformationPos, Quaternion.identity, buttonCanvas.transform);
        //         Vector3 crrPosition = newButton.GetComponent<RectTransform>().localPosition;
        //         newButton.GetComponent<RectTransform>().localPosition = buttonCanvas.transform.InverseTransformPoint(transformationPos);

        //         buttons.SetValue(i, j, newButton);
        //     }
        // }

        int rowCount = buttonTable.transform.childCount;
        for (int i = 0; i < rowCount; i++){
            Transform row = buttonTable.transform.GetChild(i);
            int buttonCountThisRow = row.childCount;
            for (int j = 0; j < buttonCountThisRow; j++){
                // buttons.SetValue(i, j, row.GetChild(j));
                row.GetChild(j).GetComponent<ButtonAttribute>().position = new Point(i,j);
            }
        }
    }
}
