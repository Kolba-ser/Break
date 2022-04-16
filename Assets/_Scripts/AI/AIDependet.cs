using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class AIDependet : MonoBehaviour, IKillable
{
    [SerializeField] private AITrigger trigger;
    [Space(20)]

    protected ITargetable target;
    protected NavMeshAgent agent;

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    protected void OnEnable()
    {
        trigger.OnEnterEvent += OnEnter;
        trigger.OnExitEvent += OnExit;
    }
    protected void OnDisable()
    {
        trigger.OnEnterEvent -= OnEnter;
        trigger.OnExitEvent -= OnExit;
    }

    protected virtual void OnEnter(ITargetable target)
    {
        this.target = target;
    }

    protected virtual void OnExit()
    {
        target = null;
    }

    public virtual void OnDeath()
    {
        agent.enabled = false;
        target = null;
    }
}

