using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class VRInput : InputBase
{
    [SerializeField]
    private GameObject rayObject;
	[SerializeField]
	private LineDrawControl lines;
	[SerializeField]
	private SteamVR_Controller.ButtonMask inputKey;
	[SerializeField]
	private SteamVR_Controller.ButtonMask teleportKey;

	SteamVR_Controller.Device device;

    private void Start()
    {
		SteamVR_TrackedObject trackedObject = rayObject.GetComponent<SteamVR_TrackedObject>();
		if (trackedObject)
		{
			device = SteamVR_Controller.Input((int) trackedObject.index);
			if (device != null)
			{
				//this.UpdateAsObservable().Where(_ => device.GetPressDown(inputKey))
				//						 .Subscribe(_ => inputSubject.OnNext(Unit.Default));
			}
		}
    }
}
