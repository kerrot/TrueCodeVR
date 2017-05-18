using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class MouseInput : InputBase
{
    [SerializeField]
    private RayCastBase ray;
    [SerializeField]
    private int inputKey;
    [SerializeField]
    private float sensitivity = 1f;
    [SerializeField]
    private GameObject view;
    [SerializeField]
    private int rotateKey;

    private Vector3 clickPosition;
    private Quaternion clickRotation;
    private Vector3 tmpRotation;

    private void Start()
    {
        BaseInput();

        Warpper warp = GameObject.FindObjectOfType<Warpper>();
        if (ray && warp)
        {
            this.UpdateAsObservable().Subscribe(_ => {
                                        ray.RayCast();
                                        warp.WarpTest(ray.Hit);
                                    });
        }

        this.UpdateAsObservable().Where(_ => Input.GetMouseButtonDown(rotateKey))// && Input.mousePosition.x > ca.pixelWidth)
                                 .Subscribe(_ => 
                                 {
                                     clickPosition = Input.mousePosition;
                                     clickRotation = view.transform.rotation;
                                 });
        this.UpdateAsObservable().Where(_ => Input.GetMouseButton(rotateKey))// && Input.mousePosition.x > ca.pixelWidth)
                                 .Select(_ => (Input.mousePosition - clickPosition) * sensitivity)
                                 .Subscribe(v => 
                                 {
                                     tmpRotation = clickRotation.eulerAngles;
                                     tmpRotation.x -= v.y;
                                     tmpRotation.y += v.x;
                                     tmpRotation.z = 0;
                                     if (tmpRotation.x > 90) tmpRotation.x = 90;
                                     if (tmpRotation.x < -90) tmpRotation.x = -90;

                                     view.transform.rotation = Quaternion.Euler(tmpRotation);
                                 });
    }

    protected override bool GetKeyDown() { return Input.GetMouseButtonDown(inputKey); }
    protected override bool GetKeyUp() { return Input.GetMouseButtonUp(inputKey); }
    protected override bool GetKeyPressed() { return Input.GetMouseButton(inputKey); }
}
