using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RayCastBase : MonoBehaviour
{
    static protected RaycastHit hit;
    static public RaycastHit Hit { get { return hit; } }

    static private ActObjectBase actObject;
    static protected ActObjectBase ActObject { set
        {
            if (value != actObject)
            {
                if (actObject != null)
                {
                    actObject.OnSelected(false);
                }

                actObject = value;
                if (actObject != null)
                {
                    actObject.OnSelected(true);
                }
            }
        }
    }
    static public ActObjectBase CurrentObject { get { return actObject; } }

    public abstract void Cast();
}
