using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class VRDevice : MonoBehaviour {

	public SteamVR_Controller.Device Device {get {return device;} }

	SteamVR_Controller.Device device;

	protected Subject<Unit> readySubject = new Subject<Unit>();
    public IObservable<Unit> OnReady { get { return readySubject; } }

	private void Start()
    {
		SteamVR_TrackedObject trackedObject = GetComponent<SteamVR_TrackedObject>();
		if (trackedObject)
		{
			device = SteamVR_Controller.Input((int) trackedObject.index);
			readySubject.OnNext(Unit.Default);
		}
    }
}
