using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Point{
    public int x;
    public int y;

    public Point(){
        x = 0;
        y = 0;
    }

    public Point(Point point){
        x = point.x;
        y = point.y;
    }

    public Point(int a, int b){
        x = a;
        y = b;
    }

    public static Point operator+ (Point a, Point b) {
        Point result = new Point();
        result.x = a.x + b.x;
        result.y = a.y + b.y;
        return result;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;
        Point b = (Point)obj;
        return (x == b.x && y == b.y);
    }

    public static bool operator== (Point a, Point b){
        if (a.x == b.x && a.y == b.y){
            return true;
        }
        else 
            return false;
    }

    public static bool operator !=(Point a, Point b) => !(a == b);

    public override int GetHashCode()
    {
        unchecked
        {
            int hashCode = x.GetHashCode();
            hashCode = (hashCode * 397) ^ y.GetHashCode();
            return hashCode;
        }
    }
}

public class Matrix9x9<T> where T : class
{
    private T[,] _value;
    private Point[] crrDirection;

    public Matrix9x9(){
        _value = new T[9, 9];
        for (int i = 0; i < 9; i ++){
            for (int j = 0; j < 9; j++){
                _value[i,j] = null;
            }
        }

        crrDirection = new Point[4];
        crrDirection[0] = new Point(0, 1);
        crrDirection[1] = new Point(0, -1);
        crrDirection[2] = new Point(-1, 0);
        crrDirection[3] = new Point(1, 0);
    }

    public void SetValue(int x, int y, T value){
        _value[x,y] = value;
    }
    public T GetValue(int x, int y){
        if (_value[x,y] == null){
            Debug.Log("Point(" + x + ", " + y + ") is null");
        }
        return _value[x,y];
    }

    public int FindMinDirectionPath(Point src, Point des){
        int[,] directionCount = new int[9,9];
        Point[,] direction = new Point[9,9];
        for (int i = 0; i < 9; i ++){
            for (int j = 0; j < 9; j++){
                directionCount[i,j] = 0;
                direction[i,j] = new Point(0,0);
            }
        }

        Queue<Point> points = new Queue<Point>();
        points.Enqueue(src);

        if (_value[des.x, des.y] != null){   //if destinate already has a ball
            return 0;
        }

        while (points.Count > 0){
            Point crrPoint = points.Dequeue();
            if (crrPoint != des){
                for (int i = 0; i < 4; i++){
                    Point newPoint = crrPoint + crrDirection[i];

                    if (newPoint.x >= 0 && newPoint.x < 9 && newPoint.y >= 0 && newPoint.y < 9
                    && _value[newPoint.x, newPoint.y] == null){ //Don't exist ball at this point;

                        int oldDirectionCount = directionCount[crrPoint.x,crrPoint.y];
                        Point oldDirection = direction[crrPoint.x, crrPoint.y];

                        if (oldDirectionCount == 0){//Current Point is Started Point;
                            directionCount[newPoint.x, newPoint.y] = oldDirectionCount + 1;
                            direction[newPoint.x, newPoint.y] = crrDirection[i];
                            points.Enqueue(newPoint);                            
                        }

                        int tempDirectionCount;
                        if (crrDirection[i] == oldDirection){
                            tempDirectionCount = oldDirectionCount;
                        }
                        else {
                            tempDirectionCount = oldDirectionCount + 1;
                        }

                        if (tempDirectionCount < 5){ 
                            //Requirement: the ball can only move 4 direction
                            if (tempDirectionCount < directionCount[newPoint.x, newPoint.y]
                            //this point is existed in another path and have lower moving direction
                            || direction[newPoint.x, newPoint.y] == new Point(0,0)){
                            //this point isn't existed in another path
                                directionCount[newPoint.x, newPoint.y] = tempDirectionCount;
                                direction[newPoint.x, newPoint.y] = crrDirection[i];
                                points.Enqueue(newPoint); 
                            }
                        }
                    }
                }
            }
        }
    
        return directionCount[des.x, des.y];
    }
}
