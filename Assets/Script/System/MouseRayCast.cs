using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRayCast : RayCastBase
{
    [SerializeField]
    private Camera ca;

    public override void Cast()
    {
        if (ca)
        {
            Ray ray = ca.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                actObject = hit.collider.gameObject.GetComponent<ActObjectBase>();
                return;
            }
        }

        actObject = null;
    }
}
