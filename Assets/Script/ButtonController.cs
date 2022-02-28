using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private static Queue<Point> selectedPositions;

    private void Awake(){
        selectedPositions = new Queue<Point>();

        GetComponent<Button>().onClick.AddListener(OnTriggerButton);
    }

    public void OnTriggerButton(){
        print("Button Clicked");
        Point position = GetComponent<ButtonAttribute>().position;
        if (selectedPositions.Count == 0){
            print("queue = 0");
            if (BallMatrix.instance.GetBall(position.x, position.y) != null){
                selectedPositions.Enqueue(position);
                GameObject ball = BallMatrix.instance.GetBall(position.x, position.y);
                ball.GetComponent<BallController>().SelectBall();
                SoundController.instance.PlaySelectedBallSound();
            }
            else {
                SoundController.instance.PlayImpossibleSelectedSound();
            }
        }
        else if (selectedPositions.Count > 0){ // or == 1 => already selected one button ealier
            print("queue > 0");
            if (BallMatrix.instance.GetBall(position.x, position.y) != null){
                SoundController.instance.PlayImpossibleSelectedSound();
                while (selectedPositions.Count > 0){
                    Point savedPos = selectedPositions.Dequeue();
                    GameObject ball = BallMatrix.instance.GetBall(savedPos.x, savedPos.y);
                    ball.GetComponent<BallController>().UnselectedBall();
                }
            }
            else {
                if (BallMatrix.instance.GetBall(position.x, position.y) == null){
                    Point srcPos = selectedPositions.Dequeue();
                    Point desPos = position;

                    if (BallMatrix.instance.balls.FindMinDirectionPath(srcPos, desPos) == 0){//Can't move
                        GameObject ball = BallMatrix.instance.GetBall(srcPos.x, srcPos.y);
                        ball.GetComponent<BallController>().UnselectedBall();
                        SoundController.instance.PlayImpossibleSelectedSound();
                    }
                    else if (BallMatrix.instance.balls.FindMinDirectionPath(srcPos, desPos) > 0){                        
                        BallMatrix.instance.Moving(srcPos, desPos);
                        SoundController.instance.PlayBallMovingSound();

                        GameObject ball = BallMatrix.instance.GetBall(desPos.x, desPos.y);
                        ball.GetComponent<BallController>().UnselectedBall();
                    }
                }
            }
        }
    }
}
