using System;
using System.ComponentModel;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.WSA;

public class Bomb
{
    public GameObject BombPrefab { get; set; }
    public int explosionRadius = 1;
    public float timeToExplode = 2f;

    public Bomb(GameObject prefab)
    {
        BombPrefab = prefab;
    }
}


