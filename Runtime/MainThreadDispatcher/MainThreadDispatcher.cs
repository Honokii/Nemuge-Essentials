using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nemuge.Core;

namespace Nemuge.MTD {
    public class MainThreadDispatcher : Singleton<MainThreadDispatcher> {
        private static readonly Queue<Action> ExecutionQueue = new Queue<Action>();

        private void Update() {
            lock (ExecutionQueue) {
                while (ExecutionQueue.Count > 0) {
                    ExecutionQueue.Dequeue().Invoke();
                }
            }
        }

        /// <summary>
        /// Enqueue a couroutine action for main thread execution.
        /// </summary>
        /// <param name="action"></param>
        public void Enqueue(IEnumerator action) {
            lock (ExecutionQueue) {
                ExecutionQueue.Enqueue(() => {
                    StartCoroutine(action);
                });
            }
        }

        /// <summary>
        /// Enqueue a regular action for main thread execution.
        /// </summary>
        /// <param name="action"></param>
        public void Enqueue(Action action) {
            Enqueue(ActionWrapper(action));
        }

        /// <summary>
        /// Enqueue an Async action for main thread execution.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Task EnqueueAsync(Action action) {
            var task = new TaskCompletionSource<bool>();

            void WrappedAction() {
                try {
                    action();
                    task.TrySetResult(true);
                }
                catch (Exception ex) {
                    task.TrySetException(ex);
                }
            }
            
            Enqueue(ActionWrapper(WrappedAction));
            return task.Task;
        }

        private static IEnumerator ActionWrapper(Action a) {
            a();
            yield return null;
        }
    }
}