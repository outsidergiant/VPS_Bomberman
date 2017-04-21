using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IIntelligence {

    List<Vector3> CalculatePath();
    List<Vector3> ChooseRandomPath();

}
