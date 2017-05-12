using UnityEngine;
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

	int counter;

	void Start()
	{
		
	}

	protected override void Cast()
	{
		List<Vector3> results = new List<Vector3>();

		counter = 0;

		if (divLength > 0)
		{
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
					lines.points = results.ToArray();

					obj = hit.collider.gameObject;

					return;
				}

				pos = tmpPos;
				dir = Quaternion.AngleAxis(divAngle, rayObject.transform.right) * dir;
				results.Add(tmpPos);
			}
		}

		lines.points = results.ToArray();
	}
}