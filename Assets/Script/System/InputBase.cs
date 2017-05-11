using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class InputBase : MonoBehaviour
{
    protected Subject<Unit> inputSubject = new Subject<Unit>();
    public IObservable<Unit> OnInput { get { return inputSubject; } }
}
