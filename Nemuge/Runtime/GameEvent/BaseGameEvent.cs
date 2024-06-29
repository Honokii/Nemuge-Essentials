using System.Collections.Generic;
using Nemuge.Pooling;
using UnityEngine;

namespace Nemuge.GameEvent {
    public class BaseGameEvent<T> : ScriptableObject {
        private readonly List<IGameEventListener<T>> _eventListeners = new List<IGameEventListener<T>>();

        public void Raise(T item) {
            for (var i = _eventListeners.Count -1; i >= 0; i--) {
                _eventListeners[i].OnEventRaised(item);
            }
        }

        public void RegisterListener(IGameEventListener<T> listener) {
            if (_eventListeners.Contains(listener)) {
                return;
            }

            _eventListeners.Add(listener);
        }

        public void UnregisterListener(IGameEventListener<T> listener) {
            if (!_eventListeners.Contains(listener)) {
                return;
            }

            _eventListeners.Remove(listener);
        }
    }
}