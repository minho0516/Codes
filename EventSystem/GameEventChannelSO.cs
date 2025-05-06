using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.EventSystem
{
    public class GameEvent
    {

    }
    [CreateAssetMenu(fileName = "GameEventChannelSO", menuName = "SO/Event/GameEventChannelSO")]
    public class GameEventChannelSO : ScriptableObject
    {
        private Dictionary<Type, Action<GameEvent>> _events = new Dictionary<Type, Action<GameEvent>>();
        private Dictionary<Delegate, Action<GameEvent>> _lookUp = new Dictionary<Delegate, Action<GameEvent>>();

        public void AddListner<T>(Action<T> handler) where T : GameEvent
        {
            if (_lookUp.ContainsKey(handler) == false)
            {
                Action<GameEvent> castHandler = (evt) => handler(evt as T);
                _lookUp[handler] = castHandler;

                Type evtType = typeof(T);
                if(_events.ContainsKey(evtType))
                {
                    _events[evtType] += castHandler;
                }
                else
                {
                    _events[evtType] = castHandler;
                }
            }
        }

        public void RemoveListner<T>(Action<T> handler) where T : GameEvent
        {
            Type evtType = typeof(T);

            if(_lookUp.TryGetValue(handler, out Action<GameEvent> action))
            {
                if(_events.TryGetValue(evtType, out Action<GameEvent> internalAction))
                {
                    internalAction -= action;
                    if(internalAction == null)
                    {
                        _events.Remove(evtType);
                    }
                    else
                    {
                        _events[evtType] = internalAction;
                    }
                }
                _lookUp.Remove(handler);
            }
        }

        public void RaiseEvent(GameEvent evt)
        {
            if (_events.TryGetValue(evt.GetType(), out Action<GameEvent> handler))
            {
                handler?.Invoke(evt);
            }
        }

        public void Clear()
        {
            _events.Clear();
            _lookUp.Clear();
        }
    }
}
