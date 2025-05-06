using RPG.Entities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.FSM
{
    public class StateMachine
    {
        public EntityState currentState { get; private set; }
        private Dictionary<FSMState, EntityState> _states;
        public StateMachine(EntityStateListSO fsmStates, Entity entity)
        {
            _states = new Dictionary<FSMState, EntityState>();

            foreach (StateSO state in fsmStates.states)
            {
                try
                {
                    Type t = Type.GetType(state.className);
                    //Debug.Log(t);
                    var entityState = Activator.CreateInstance(t, entity, state.animParam) as EntityState;
                    _states.Add(state.stateName, entityState);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"{state.className} loading Error, Message : {ex.Message}");
                    //Debug.Log(ex);
                }
            }

        }

        public void Initialize(FSMState startState)
        {
            currentState = GetState(startState);
            Debug.Assert(currentState != null, $"{startState} state not found");
            currentState.Enter();
        }

        public void ChangeState(FSMState startState)
        {
            currentState.Exit();
            Debug.Assert(currentState != null, $"{startState} state not found");
            currentState = GetState(startState);
            currentState.Enter();
        }

        public void UpdateStateMachine()
        {
            currentState.Update();
        }

        public EntityState GetState(FSMState state) => _states.GetValueOrDefault(state);
    }
}
