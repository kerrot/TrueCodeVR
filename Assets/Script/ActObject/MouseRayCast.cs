using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class MouseRayCast : RayCastBase
{
    [SerializeField]
    private Camera ca;

    protected override void Cast()
    {
        GameObject tmp = obj;
        obj = null;

        if (ca)
        {
            Ray ray = ca.ScreenPointToRay(Input.mousePosition);
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
