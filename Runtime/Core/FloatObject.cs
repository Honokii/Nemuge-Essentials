using UnityEngine;

namespace Nemuge.Core {
    [CreateAssetMenu(menuName = "Nemuge/Object/Float", fileName = "NewFloatObject")]
    public class FloatObject : ScriptableObject {
        public float value;
    }
}