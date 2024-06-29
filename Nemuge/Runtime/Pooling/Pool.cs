using Nemuge.Core;
using UnityEngine;

namespace Nemuge.Pooling {
    [CreateAssetMenu(menuName = "Nemuge/Pool", fileName = "newPool")]
    public class Pool : ScriptableObject {
        public StringObject poolTag;
        public Poolable poolable;
        public int initialCount;
        public bool expandable;
    }
}