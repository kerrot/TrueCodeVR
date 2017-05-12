using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRayCast : RayCastBase
{
    [SerializeField]
    private Camera ca;

    private Vector3 tmpPosition;

	protected override void Cast()
    {
        GameObject tmp = obj;
        obj = null;

        if (ca)
        {
            tmpPosition = Input.mousePosition;

            Ray ray = ca.ScreenPointToRay(tmpPosition);
            if (Physics.Raycast(ray, out hit))
            {
                obj = hit.collider.gameObject;
                if (tmp == null)
                {
                    CastIn();
                }
            }
            else if (tmp != null)
            {
                CastOut(tmp);
            }
        }
    }
}
