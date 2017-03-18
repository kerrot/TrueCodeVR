using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class MouseInput : InputBase
{
    [SerializeField]
    private float clickPeriod = 0.3f;
    [SerializeField]
    private float sensitivity = 1f;
    [SerializeField]
    private Camera ca;

    private Vector3 clickPosition;
    private Quaternion clickRotation;

    private void Start()
    {
        this.UpdateAsObservable().Where(_ => Input.GetMouseButtonDown(0))
                                 .Throttle(System.TimeSpan.FromSeconds(clickPeriod))
                                 .Where(_ => !Input.GetMouseButton(0))
                                 .Subscribe(_ => inputSubject.OnNext(Unit.Default));

        this.UpdateAsObservable().Where(_ => Input.GetMouseButtonDown(0))
                                 .Subscribe(_ => 
                                 {
                                     clickPosition = Input.mousePosition;
                                     clickRotation = ca.transform.rotation;
                                 } );
        this.UpdateAsObservable().Where(_ => Input.GetMouseButton(0))
                                 .Select(_ => (Input.mousePosition - clickPosition) * sensitivity)
                                 .Subscribe(v => 
                                 {
                                     ca.transform.rotation = clickRotation * Quaternion.Euler(-v.y, v.x, 0);
                                 });
    }
}
