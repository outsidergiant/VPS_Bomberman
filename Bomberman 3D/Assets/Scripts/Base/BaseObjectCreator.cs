using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObjectCreator {

    public virtual GameObject GetBomb()
    {
        return Resources.Load(@"Bomb") as GameObject;
    }

    public virtual GameObject GetPlayer()
    {
        return Resources.Load(@"Player") as GameObject;
    }

    public GameObject GetEnemyByName(string name)
    {
        return Resources.Load(name) as GameObject;
    }

}
