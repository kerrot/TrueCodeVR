using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class InputBase : MonoBehaviour
{
	public enum InputType
	{
		Down,
		Up,
		Pressed,
		Click,
	}

	[SerializeField]
	protected InputType type;

    protected Subject<Unit> inputSubject = new Subject<Unit>();
    public IObservable<Unit> OnInput { get { return inputSubject; } }
}
