using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpeedTypes
{
    Slowest = 1, Slow = 2, Normal = 3, Fast = 4
}


public class CharacterBase {

    //public Rigidbody rigidbody;
    public GameObject gameObject;
    public List<PowerUp> abilities;
    public int bombNumber;
    public int explosionRadius;
    public SpeedTypes speed;

    public CharacterBase()
    {
        abilities = new List<PowerUp>();
    }

    //void Start()
    //{
    //    OnStart();
    //}

    //protected virtual void OnStart()
    //{
    //    movingBehaviour = new MovingBehaviourBase();
    //    abilities = new Dictionary<Type, Type>();
    //}

}
