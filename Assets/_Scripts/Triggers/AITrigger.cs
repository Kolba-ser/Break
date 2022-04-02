using UnityEngine;

public sealed class AITrigger : MonoBehaviour, IPursuer
{
    private Pursued target;

    public delegate void TriggerEnterHandler(ITargetable target);
    public event TriggerEnterHandler OnEnterEvent;
    public delegate void TriggerExitHandler();
    public event TriggerExitHandler OnExitEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (target == null && other.TryGetComponent(out Pursued pursued))
        {
            pursued.Join(this);
            target = pursued;
            OnEnterEvent(target);
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (target != null && other.transform.Equals(target.transform))
        {
            target.TakeOff(this);
            target = null;
            OnExitEvent();
        }
    }
}

