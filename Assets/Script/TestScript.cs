﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    private void Start() {
        print(Camera.main.WorldToScreenPoint(transform.position));
    }
}
