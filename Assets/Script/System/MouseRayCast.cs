using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRayCast : RayCastBase
{
    [SerializeField]
    private Camera ca;

    private Vector3 tmpPosition;

    public override void Cast()
    {
        if (ca)
        {
            tmpPosition = Input.mousePosition;
            //tmpPosition.x -= ca.pixelWidth;
            //Debug.Log(Input.mousePosition);
            Ray ray = ca.ScreenPointToRay(tmpPosition);
            //Debug.DrawRay(ray.origin, ray.direction);
            if (Physics.Raycast(ray, out hit))
            {
                actObject = hit.collider.gameObject.GetComponent<ActObjectBase>();
                return;
            }
        }

        actObject = null;
    }
}
