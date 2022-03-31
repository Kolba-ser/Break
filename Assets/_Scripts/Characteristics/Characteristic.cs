
using UnityEngine;

public abstract class Characteristic : MonoBehaviour
{
    protected bool IsPrepared { get; private set; }
    protected bool IsExecuted { get; private set; }
    protected bool IsJoined { get; private set; }


    public void Execute()
    {
        IsExecuted = true;
    }

    public void Join()
    {
        IsJoined = true;
    }

    protected void TakeOff()
    {
        IsPrepared = false;
        IsExecuted = false;
        IsJoined = false;
    }

    protected void Prepare() 
    {
        IsPrepared = true;
    }
}

