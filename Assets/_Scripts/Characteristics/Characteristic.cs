
using UnityEngine;

public abstract class Characteristic : MonoBehaviour
{
    protected bool isPrepared { get; private set; }

    public abstract void Execute();

    protected void TakeOff()
    {
        isPrepared = false;
    }

    protected void Prepare() 
    {
        isPrepared = true;
    }

}

