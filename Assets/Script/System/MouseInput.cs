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
        this.UpdateAsObservable().Where(_ => Input.GetMouseButtonDown(0))// && Input.mousePosition.x > ca.pixelWidth)
                                 .Throttle(System.TimeSpan.FromSeconds(clickPeriod))
                                 .Where(_ => !Input.GetMouseButton(0))
                                 .Subscribe(_ => inputSubject.OnNext(Unit.Default));

        this.UpdateAsObservable().Where(_ => Input.GetMouseButtonDown(0))// && Input.mousePosition.x > ca.pixelWidth)
                                 .Subscribe(_ => 
                                 {
                                     clickPosition = Input.mousePosition;
                                     clickRotation = ca.transform.rotation;
                                 } );
        this.UpdateAsObservable().Where(_ => Input.GetMouseButton(0))// && Input.mousePosition.x > ca.pixelWidth)
                                 .Select(_ => (Input.mousePosition - clickPosition) * sensitivity)
                                 .Subscribe(v => 
                                 {
                                     ca.transform.rotation = Quaternion.Euler(clickRotation.eulerAngles.x - v.y, clickRotation.eulerAngles.y + v.x, 0);
                                 });
    }
}
