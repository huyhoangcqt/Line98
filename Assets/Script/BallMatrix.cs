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
            // print("Current position: " + crrPosition.ToString());
            Vector3 newPos = new Vector3(crrPosition.x/ratio, crrPosition.y/ratio, -1);
            // print("new position: " + newPos.ToString());
            newBall.GetComponent<RectTransform>().localPosition = newPos;
            
            newBall.GetComponent<Image>().sprite = ballProperties[idx].image;
            newBall.GetComponent<BallAttribute>().ballAttribute = ballProperties[idx].ballAttributeSObj;
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

        FindMatchBall(desPos);
    }

    private BallAttribute originAttribute;
    private Point up, down, left, right, topleft, downright, topright, downleft;
    public void FindMatchBall(Point origin){
        originAttribute = GetBall(origin.x, origin.y).GetComponent<BallAttribute>();
        print("origin: " + origin.x + ", " + origin.y);
        up = FindLimitPoint(new Point(-1, 0), origin);
        // down = FindLimitPoint(new Point(1, 0), origin);
        // left = FindLimitPoint(new Point(0, -1), origin);
        // right = FindLimitPoint(new Point(0, 1), origin);
        // topleft = FindLimitPoint(new Point(-1, -1), origin);
        // downright = FindLimitPoint(new Point(1, 1), origin);
        // topright = FindLimitPoint(new Point(1, -1), origin);
        // downleft = FindLimitPoint(new Point(-1, 1), origin);

        print("Up: " + up.x + ", " + up.y);
        // print("Down: " + down.x + ", " + down.y);
    }

    public Point FindLimitPoint(Point direction, Point origin){
        Point result = new Point(origin);
        Point point = new Point(origin);
        do {
            point.x += direction.x;
            point.y += direction.y;

            if (point.x < 0 || point.x > 8 || point.y < 0 || point.y > 8){
                break;
            }

            GameObject ball = GetBall(point.x, point.y);
            if (ball == null){
                print("Null: " + point.x + ", " + point.y);
                break;
            }
            
            BallAttribute ballAttribute = ball.GetComponent<BallAttribute>();
            if (ballAttribute.ballAttribute.color != originAttribute.ballAttribute.color)
                break;
            if (ballAttribute.ballAttribute.color == originAttribute.ballAttribute.color) 
                result = point;

        } while(true);
        return result;
    }
}
