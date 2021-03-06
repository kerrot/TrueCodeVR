﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CurveRayCast : RayCastBase {

	[SerializeField]
	private float divLength;
	[SerializeField]
	private float divAngle;
	[SerializeField]
	private float rate;
    [SerializeField]
    private GameObject rayObject;
	[SerializeField]
	private LineDrawControl lines;

    List<Vector3> results = new List<Vector3>();

    public GameObject RayOwner 
    {
        get { return rayObject; }
        set { rayObject = value; }
    }

    protected override void Cast()
	{
        results.Clear();

		if (divLength > 0)
		{
            int counter = 0;

			Vector3 pos = rayObject.transform.position;
			Vector3 dir = rayObject.transform.forward;

			Vector3 check = pos + dir;
			check.y = pos.y;
			check = check - pos;

			results.Add(pos);

			while (Vector3.Angle (check, dir) < 90 && counter < 360)
			{
				++counter;

				Vector3 tmpPos = pos + dir * divLength;
				results.Add(tmpPos);

                if (Physics.Raycast(pos, dir, out hit, divLength))
                {
                    obj = hit.collider.gameObject;

                    break;
                }
                else if (divAngle <= 0)
                {
                    break;
                }

				pos = tmpPos;
				dir = Quaternion.AngleAxis(divAngle, rayObject.transform.right) * dir;
				results.Add(tmpPos);
			}
		}

        if (lines)
        {
            lines.points = results.ToArray();
        }
	}
}