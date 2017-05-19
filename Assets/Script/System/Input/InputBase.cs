using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public abstract class InputBase : MonoBehaviour
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
    [SerializeField]
    private float clickPeriod = 0.3f;

    protected Subject<Unit> inputSubject = new Subject<Unit>();
    public IObservable<Unit> OnInput { get { return inputSubject; } }
	protected Subject<Unit> inputOffSubject = new Subject<Unit>();
	public IObservable<Unit> OnInputOff { get { return inputOffSubject; } }

    protected abstract bool GetKeyDown();
    protected abstract bool GetKeyUp();
    protected abstract bool GetKeyPressed();

    protected void BaseInput()
    {
        switch (type)
        {
            case InputType.Up:
                {
                    this.UpdateAsObservable().Where(_ => GetKeyUp())
                                 .Subscribe(_ => inputSubject.OnNext(Unit.Default));
                }
                break;
            case InputType.Down:
                {
                    this.UpdateAsObservable().Where(_ => GetKeyDown())
                                 .Subscribe(_ => inputSubject.OnNext(Unit.Default));
                }
                break;
            case InputType.Pressed:
                {
                    this.UpdateAsObservable().Where(_ => GetKeyPressed())
											.Subscribe(_ => inputSubject.OnNext(Unit.Default));

					this.UpdateAsObservable().Where(_ => GetKeyUp())
											.Subscribe(_ => inputOffSubject.OnNext(Unit.Default));
                }
                break;
            case InputType.Click:
                {
                    this.UpdateAsObservable().Where(_ => GetKeyDown())
                                 .Throttle(System.TimeSpan.FromSeconds(clickPeriod))
                                 .Where(_ => !GetKeyPressed())
                                 .Subscribe(_ => inputSubject.OnNext(Unit.Default));
                }
                break;
        }
    }
}
