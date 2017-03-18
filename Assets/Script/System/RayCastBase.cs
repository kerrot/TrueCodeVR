using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RayCastBase : MonoBehaviour
{
    static protected RaycastHit hit;
    static public RaycastHit Hit { get { return hit; } }

    static protected ActObjectBase actObject;
    static public ActObjectBase CurrentObject { get { return actObject; } }

    public abstract void Cast();
}
