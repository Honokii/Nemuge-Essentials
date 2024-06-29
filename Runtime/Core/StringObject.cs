using UnityEngine;

namespace Nemuge.Core {
    [CreateAssetMenu(menuName = "Nemuge/Object/String", fileName = "NewStringObject")]
    public class StringObject : ScriptableObject {
        public string value;
    }
}