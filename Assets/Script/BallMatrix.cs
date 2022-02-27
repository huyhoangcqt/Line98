using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMatrix : MonoBehaviour
{
    private int ballCount;
    private Matrix9x9<GameObject> balls;
    [SerializeField] private GameObject[] templateBalls;

    private void Awake() {
        balls = new Matrix9x9<GameObject>();
        ballCount = Random.Range(20, 30);

        for (int i = 0; i < ballCount; i++){
            int idx = Random.Range(0, templateBalls.Length);
            GameObject newBall = Instantiate(templateBalls[idx], Vector3.zero, Quaternion.identity);
            Point pos = new Point(Random.Range(0, 8), Random.Range(0,8));
            while (balls.GetValue(pos.x, pos.y) != null){
                pos = new Point(Random.Range(0, 8), Random.Range(0,8));
            }

            balls.SetValue(pos.x, pos.y, newBall);
            newBall.GetComponent<BallAttribute>().SetPosition(pos);
            Vector2 positionInWorldSpace = PositionMatrix.instance.GetValue(pos.x, pos.y);
            newBall.transform.position = positionInWorldSpace;
        }
    }

    public void SetBall(int x, int y, GameObject value){
        balls.SetValue(x, y, value);
    }
}
