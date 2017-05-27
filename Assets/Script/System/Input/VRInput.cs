using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class VRInput : InputBase
{
    public enum VRKey
    {

        System          ,
        ApplicationMenu ,
        Grip            ,
        Touchpad        ,
        Trigger         ,
    }

    public enum VRInputType
    {
        Touch,
        Press,
    }

    [SerializeField]
    private VRDevice DV;
    [SerializeField]
    private VRInputType vrType = VRInputType.Press;
	[SerializeField]
	private GameObject chara;
	[SerializeField]
	private VRKey inputKey;
	[SerializeField]
	private VRKey teleportKey;
    [SerializeField]
    private CurveRayCast ray;

    SteamVR_Controller.Device device;

    private void Start()
    {
		if (DV)
		{
			DV.OnReady.Subscribe(_ => Init());
		}
    }

	void Init( ) 
	{
		device = DV.Device;
			if (device != null)
			{
                BaseInput();

                Warpper warp = GameObject.FindObjectOfType<Warpper>();
                if (ray && warp)
                {

                    // when press teleport button, show ray and VR character, then purform raycast;
                    this.UpdateAsObservable().Where(_ => device.GetPress(GetButton(teleportKey)))
                                 .Subscribe(_ => {
                                     ray.RayOwner = DV.gameObject;
                                     ray.gameObject.SetActive(true);
									 chara.SetActive(true);
                                     ray.RayCast();
                                     warp.WarpTest(ray.Hit);
                                 });

                    //when teleport keyup, hide ray and VR character
                    this.UpdateAsObservable().Where(_ =>    device.GetPressUp(GetButton(teleportKey)) && 
                                                            ray.RayOwner == DV.gameObject) // for two hand, only owner can hide.
                                 .Subscribe(_ => {
                                        ray.gameObject.SetActive(false);
										chara.SetActive(false);		
								});
                }
            }
	}

	private ulong GetButton(VRKey k)
    {
        switch (k)
        {
        case VRKey.System          : return SteamVR_Controller.ButtonMask.System;
        case VRKey.ApplicationMenu : return SteamVR_Controller.ButtonMask.ApplicationMenu;
        case VRKey.Grip            : return SteamVR_Controller.ButtonMask.Grip;
        case VRKey.Touchpad        : return SteamVR_Controller.ButtonMask.Touchpad;
        case VRKey.Trigger         : return SteamVR_Controller.ButtonMask.Trigger;
        }

        return SteamVR_Controller.ButtonMask.Trigger;
    }

    protected override bool GetKeyDown() { return (vrType == VRInputType.Press) ? device.GetPressDown(GetButton(inputKey)) : device.GetTouchDown(GetButton(inputKey)); }
    protected override bool GetKeyUp() { return (vrType == VRInputType.Press) ? device.GetPressUp(GetButton(inputKey)) : device.GetTouchUp(GetButton(inputKey)); }
    protected override bool GetKeyPressed() { return (vrType == VRInputType.Press) ? device.GetPress(GetButton(inputKey)) : device.GetTouch(GetButton(inputKey)); }
}
