using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class MouseInput : InputBase
{
    [SerializeField]
    private int inputKey;
    [SerializeField]
    private float clickPeriod = 0.3f;
    [SerializeField]
    private float sensitivity = 1f;
    [SerializeField]
    private Camera ca;
    [SerializeField]
    private int rotateKey;

    private Vector3 clickPosition;
    private Quaternion clickRotation;
    private Vector3 tmpRotation;

    private void Start()
    {
        switch (type)
        {
            case InputType.Up:
                {
                    this.UpdateAsObservable().Where(_ => Input.GetMouseButtonUp(inputKey))
                                 .Subscribe(_ => inputSubject.OnNext(Unit.Default));
                }
                break;
            case InputType.Down:
                {
                    this.UpdateAsObservable().Where(_ => Input.GetMouseButtonDown(inputKey))
                                 .Subscribe(_ => inputSubject.OnNext(Unit.Default));
                }
                break;
            case InputType.Pressed:
                {
                    this.UpdateAsObservable().Where(_ => Input.GetMouseButton(inputKey))
                                 .Subscribe(_ => inputSubject.OnNext(Unit.Default));
                }
                break;
            case InputType.Click:
                {
                    this.UpdateAsObservable().Where(_ => Input.GetMouseButtonDown(inputKey))
                                 .Throttle(System.TimeSpan.FromSeconds(clickPeriod))
                                 .Where(_ => !Input.GetMouseButton(inputKey))
                                 .Subscribe(_ => inputSubject.OnNext(Unit.Default));
                }
                break;
        }

        this.UpdateAsObservable().Where(_ => Input.GetMouseButtonDown(rotateKey))// && Input.mousePosition.x > ca.pixelWidth)
                                 .Subscribe(_ => 
                                 {
                                     clickPosition = Input.mousePosition;
                                     clickRotation = ca.transform.rotation;
                                 } );
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

                                     ca.transform.rotation = Quaternion.Euler(tmpRotation);
                                 });
    }
}
