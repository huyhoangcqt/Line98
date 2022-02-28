using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public struct BallProperty{
    public Sprite image;
    public BallAttributeScriptableObject ballAttributeSObj;
}

public class BallMatrix : Singleton<BallMatrix>
{
    private int ballCount;
    private Matrix9x9<GameObject> _balls;
    [SerializeField] private BallProperty[] ballProperties;
    [SerializeField] private GameObject templateBall, transparentBall, ballCanvas;
    private const int BASE_SCREEN_WIDTH = 720;
    private float ratio;

    protected override void Awake() {
        // print("Screen width: " + Screen.width);
        // print("base screen width: " + BASE_SCREEN_WIDTH);
        ratio = (float)Screen.width / BASE_SCREEN_WIDTH;
        // print("Ratio: " + ratio);
        
        base.Awake();

        Initialize();

        ballCount = Random.Range(20, 30);

        for (int i = 0; i < ballCount; i++){
            int idx = Random.Range(0, ballProperties.Length);

            Point posInMatrix = new Point(Random.Range(0, 8), Random.Range(0,8));
            while (GetBall(posInMatrix.x, posInMatrix.y) != null){
                posInMatrix = new Point(Random.Range(0, 8), Random.Range(0,8));
            }
            
            Vector2 positionInWorldSpace = PositionMatrix.instance.GetValue(posInMatrix.x, posInMatrix.y);
            Vector3 transformationPos = new Vector3(positionInWorldSpace.x, positionInWorldSpace.y, -1);
            GameObject newBall = Instantiate(templateBall, transformationPos, Quaternion.identity, ballCanvas.transform);
            Vector3 crrPosition = newBall.GetComponent<RectTransform>().localPosition;
            print("Current position: " + crrPosition.ToString());
            Vector3 newPos = new Vector3(crrPosition.x/ratio, crrPosition.y/ratio, -1);
            print("new position: " + newPos.ToString());
            newBall.GetComponent<RectTransform>().localPosition = newPos;
            
            newBall.GetComponent<Image>().sprite = ballProperties[idx].image;
            newBall.GetComponent<BallAttribute>().SetAttribute(ballProperties[idx].ballAttributeSObj);
            // newBall.GetComponent<BallAttribute>().SetPosition(posInMatrix);
            

            SetBall(posInMatrix.x, posInMatrix.y, newBall);
        }
    }

    public void Initialize(){
        _balls = new Matrix9x9<GameObject>();
    }

    public void SetBall(int x, int y, GameObject value){
        _balls.SetValue(x, y, value);
    }

    public GameObject GetBall(int x, int y){
        return _balls.GetValue(x, y);
    }

    public Matrix9x9<GameObject> balls{
        get {
            return _balls;
        }
    }

    public void Moving(Point srcPos, Point desPos){
        GameObject ball = GetBall(srcPos.x, srcPos.y);
        SetBall(srcPos.x, srcPos.y, null);
        SetBall(desPos.x, desPos.y, ball);

        Vector2 positionInWorldSpace = PositionMatrix.instance.GetValue(desPos.x, desPos.y);
        Vector3 transformationPos = new Vector3(positionInWorldSpace.x, positionInWorldSpace.y, -1);
        GameObject newBall = Instantiate(transparentBall, transformationPos, Quaternion.identity, ballCanvas.transform);
        Vector3 crrPosition = newBall.GetComponent<RectTransform>().localPosition;
        ball.GetComponent<RectTransform>().localPosition = new Vector3(crrPosition.x, crrPosition.y, -1);
        Destroy(newBall);
    }
}
