using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntelligentSystemBase : MonoBehaviour, IIntelligence
{

    private List<int[]> directions;
    protected Enemy character;

    public IntelligentSystemBase(Enemy character)
    {
        this.character = character;
    }

    public virtual List<Vector3> CalculatePath()
    {
        return ChooseRandomPath();
    }

    public virtual List<Vector3> ChooseRandomPath()
    {
        return CalcCharacterDirection();
    }

    protected virtual void InitPossibleDirections()
    {
        directions = new List<int[]>();
        int[] x = new int[] { -1, 0, 1 };
        int[] z = new int[] { -1, 0, 1 };
        directions.Add(x);
        directions.Add(z);
    }

    protected virtual List<Vector3> CalcCharacterDirection()
    {
        InitPossibleDirections();
        return ChooseRandomDirection();
    }

    protected virtual List<Vector3> ChooseRandomDirection()
    {
        Vector3 position = character.gameObject.transform.position;
        int xDir = directions[0][UnityEngine.Random.Range(0, 3)];
        int zDir = directions[1][UnityEngine.Random.Range(0, 3)];
        if (xDir != 0)
        {
            position.x = position.x + xDir;
        } else
        {
            position.z = position.z + zDir;
        }
        return new List<Vector3>() { position };
    }
}
