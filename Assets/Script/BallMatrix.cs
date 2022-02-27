using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BallMatrix : MonoBehaviour
{
    private int ballCount;
    private Matrix9x9<GameObject> balls;
    [SerializeField] private GameObject[] templateBalls;
    [SerializeField] private GameObject ballPanel;
    private float ratio;

    private const int BASE_SCREEN_WIDTH = 720;

    private void Awake() {
        balls = new Matrix9x9<GameObject>();
        ballCount = Random.Range(20, 30);

        for (int i = 0; i < ballCount; i++){
            int idx = Random.Range(0, templateBalls.Length);
            Point posInMatrix = new Point(Random.Range(0, 8), Random.Range(0,8));
            while (balls.GetValue(posInMatrix.x, posInMatrix.y) != null){
                posInMatrix = new Point(Random.Range(0, 8), Random.Range(0,8));
            }
            
            Vector2 positionInWorldSpace = PositionMatrix.instance.GetValue(posInMatrix.x, posInMatrix.y);
            Vector3 transformationPos = new Vector3(positionInWorldSpace.x, positionInWorldSpace.y, -1);
            GameObject newBall = Instantiate(templateBalls[idx], transformationPos, Quaternion.identity, ballPanel.transform);
            
            newBall.GetComponent<BallAttribute>().SetPosition(posInMatrix);
            balls.SetValue(posInMatrix.x, posInMatrix.y, newBall);
        }
    }

    private void Start(){
        ratio = (Screen.width / BASE_SCREEN_WIDTH);
    }

    public void SetBall(int x, int y, GameObject value){
        balls.SetValue(x, y, value);
    }
}
