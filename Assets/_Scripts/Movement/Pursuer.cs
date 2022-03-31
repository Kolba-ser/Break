using System;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public sealed class Pursuer : MonoBehaviour, IPursuer
{
    [SerializeField] private NavMeshAgent agent;
    private Pursued target;

    private IDisposable chasing;

    public void Chasing()
    {
        chasing?.Dispose();

        chasing = Observable.Interval(0.2f.InSec()).TakeUntilDisable(target.gameObject)
            .TakeWhile(_ => target != null)
            .Subscribe(_ =>
            {
                agent.SetDestination(target.transform.position);
            });
    }

    private void OnTriggerEnter(Collider other)
    {
        if (target == null && other.TryGetComponent(out Pursued pursued))
        {
            pursued.Join(this);
            target = pursued;
            Chasing();
            Debug.Log(target.name);
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (target != null && other.transform.Equals(target.transform))
        {
            target.TakeOff(this);
            target = null;
        }
    }
}

