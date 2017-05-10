using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class VRRayCast : RayCastBase {

	[SerializeField]
	private float divLength;
	[SerializeField]
	private float divAngle;
	[SerializeField]
	private float rate;
	[SerializeField]
    private Camera ca;
    [SerializeField]
    private GameObject rayObject;

	RaycastHit hit;
	LineDrawControl lines;
	SteamVR_Controller.Device device;

	int counter;

	public override void Cast()
    {
		lines.gameObject.SetActive(false);
		ActObject = null;

		if (device != null)
		{
			if (device.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
			{
				lines.gameObject.SetActive(true);
				if (Raycast())
				{
					ActObject = hit.collider.gameObject.GetComponent<ActObjectBase>();
					return;
				}
			}

		}
    }

	void Start()
	{
		lines = GetComponent<LineDrawControl> ();
		SteamVR_TrackedObject trackedObject = rayObject.GetComponent<SteamVR_TrackedObject>();
		if (trackedObject)
		{
			device = SteamVR_Controller.Input((int) trackedObject.index);
		}
	}

	public bool Raycast()
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

					return true;
				}

				pos = tmpPos;
				dir = Quaternion.AngleAxis(divAngle, ca.transform.right) * dir;
				results.Add(tmpPos);
			}
		}

		lines.points = results.ToArray();

		return false;
	}
}