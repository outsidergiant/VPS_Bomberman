using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterBase {
    //public BombManager bombManager;
    public int points;

    public Player(GameObject prefab) : base()
    {
        speed = SpeedTypes.Normal;
        explosionRadius = 1;
        bombNumber = 1;
        this.gameObject = prefab;
    }
}
