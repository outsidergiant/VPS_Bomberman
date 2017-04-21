using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Smart
{
    Low = 1, Normal = 2, High = 3
}

public class Enemy : CharacterBase {

    public Smart intelligence;
    public int numberOfPoints;
    public string name;
    public Dictionary<Vector3, int> fieldMap;

    public Enemy() : base()
    {
        fieldMap = new Dictionary<Vector3, int>();
        ///Enemy1Factory factory = FactoryContainer.Instance.Resolve<Enemy1Factory>();
        //this.intelligence = intelligence;
        //this.numberOfPoints = numberOfPoints;
        //this.speed = speed;
        //fieldMap = new Dictionary<Vector3, int>();
        //gameObject = factory.GetObject();
    }

    public static int GetPointNumberByName(string name)
    {
        if (name.Contains("Oneal")) return 200;
        else if (name.Contains("Balloom")) return 100;
        else return 0;
    }

}
