using UnityEngine;
using UnityEngine.Events;

namespace Nemuge.GameEvent {
    public abstract class BaseGameEventListener<T, E, R> : MonoBehaviour, IGameEventListener<T> where E : BaseGameEvent<T> where R : UnityEvent<T> {
        [SerializeField]
        private E gameEvent;
        public E GameEvent {
            get => gameEvent;
            set => gameEvent = value;
        }

        [SerializeField]
        private R response;
        public R Response {
            get => response;

            set => response = value;
        }

        private void OnEnable() {
            if (GameEvent == null) return;

            GameEvent.RegisterListener(this);
        }

        private void OnDisable() {
            if (GameEvent == null) return;

            GameEvent.UnregisterListener(this);
        }

        public void OnEventRaised(T item) {
            Response?.Invoke(item);
        }
    }
}