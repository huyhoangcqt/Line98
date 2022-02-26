using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NullableMatrix9x9<T> : MonoBehaviour where T : class
{
    private T[,] value;

    public NullableMatrix9x9(){
        value = new T[9, 9];
        for (int i = 0; i < 9; i ++){
            for (int j = 0; j < 9; j++){
                value[i,j] = null;
            }
        }
    }
}

[Serializable]
public class Matrix9x9<T> : MonoBehaviour where T : struct
{
    [SerializeField] private T[,] value;

    public Matrix9x9(){
        value = new T[9, 9];
    }
}
