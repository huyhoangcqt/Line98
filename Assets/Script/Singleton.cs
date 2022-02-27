using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;

    public static T instance{
        set { _instance = value;}
        get { 
            if (_instance == null){
                _instance = FindObjectOfType<T>();
                if (_instance == null){
                    GameObject newOb = new GameObject();
                    _instance = newOb.AddComponent<T>();
                } 
            }
            return _instance;
        }
    }
    protected virtual void Awake(){
        _instance = this as T;
    }
}
