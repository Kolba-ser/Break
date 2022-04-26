
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public delegate T StateFactory<out T>();

public class StateMachine<TTrigger>
{

    private readonly Dictionary<Type, IStateDefinition> states =
        new Dictionary<Type, IStateDefinition>();
    private readonly Dictionary<(Type, TTrigger), IStateDefinition> transitions =
        new Dictionary<(Type, TTrigger), IStateDefinition>();

    private (IStateDefinition Defenition, IState State) activeState;

    private interface IStateDefinition
    {
        public IState CreateState();
        public Type StateType
        {
            get;
        }
    }

    /// <summary>
    /// Выполняет преход по триггеру из одного состояния в другое.
    /// </summary>
    /// <param name="trigger"></param>
    public void Fire(TTrigger trigger)
    {
        var activeDefenition = activeState.Defenition;

        var transitionDefention = (activeDefenition?.StateType, trigger);

        Assert.IsTrue(transitions.ContainsKey(transitionDefention),
            $"Transitions from state {activeState.State?.ToString() ?? "ROOT"} not found by trigger {trigger}");

        activeDefenition = transitions[transitionDefention];

        Debug.Log("Exit " + activeState.State);
        activeState.State?.OnExit();
        activeState = (activeDefenition, activeDefenition?.CreateState());
        Debug.Log("Enter " + activeState.State);
        activeState.State.OnEnter();
    }
    /// <summary>
    /// Позволяет отпределить состояние
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    /// <param name="factory"></param>
    public void DefineState<TState>(StateFactory<TState> factory) where TState : IState
    {
        states[typeof(TState)] = new StateMachine<TTrigger>.StateDefinition<TState>(factory);
    }

    /// <summary>
    /// Позволяет определить переход между состояниями.
    /// </summary>
    /// <typeparam name="TState1"></typeparam>
    /// <typeparam name="TState2"></typeparam>
    /// <param name="trigger"></param>
    public void DefineTransition<TState1, TState2>(TTrigger trigger)
        where TState1 : IState
        where TState2 : IState
    {
        var type1 = typeof(TState1);
        Assert.IsTrue(states.ContainsKey(type1), $"State of {type1} not found");
        var type2 = typeof(TState2);
        Assert.IsTrue(states.ContainsKey(type2), $"State of {type2} not found");

        transitions[(type1, trigger)] = states[type2];
    }
    /// <summary>
    /// Позволяет определить переход в начальное состояние.
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    /// <param name="trigger"></param>
    public void DefineStartTransition<TState>(TTrigger trigger) where TState : IState
    {
        var type = typeof(TState);
        Assert.IsTrue(states.ContainsKey(type), $"State of {type} not found");


        transitions[(null, trigger)] = states[type];
    }

    private class StateDefinition<TState> : IStateDefinition where TState : IState
    {
        private readonly StateFactory<TState> factory;

        public StateDefinition(StateFactory<TState> factory)
        {
            this.factory = factory;
        }

        public Type StateType => typeof(TState);

        public IState CreateState()
        {
            return factory();
        }
    }
}

