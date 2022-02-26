using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "SampleData", menuName = "ScriptableObjects/MySampleScriptableObject", order = 1)]
public class MySampleScriptableObject : ScriptableObject, ISerializationCallbackReceiver
{
    public Vector3[,] unserializable = new Vector3[3, 3];
 
    // A list that can be serialized
    [SerializeField] private List<Package<Vector3>> serializable;
    // A package to store our stuff
    [System.Serializable]
    struct Package<TElement>
    {
        public int Index0;
        public int Index1;
        public TElement Element;
        public Package(int idx0, int idx1, TElement element)
        {
            Index0 = idx0;
            Index1 = idx1;
            Element = element;
        }
    }
    public void OnBeforeSerialize()
    {
        // Convert our unserializable array into a serializable list
        serializable = new List<Package<Vector3>>();
        for (int i = 0; i < unserializable.GetLength(0); i++)
        {
            for (int j = 0; j < unserializable.GetLength(1); j++)
            {
                serializable.Add(new Package<Vector3>(i, j, unserializable[i, j]));
            }
        }
    }
    public void OnAfterDeserialize()
    {
        // Convert the serializable list into our unserializable array
        // unserializable = new Vector3[3, 3];
        // foreach(var package in serializable)
        // {
        //     unserializable[package.Index0, package.Index1] = package.Element;
        // }
    }
}
