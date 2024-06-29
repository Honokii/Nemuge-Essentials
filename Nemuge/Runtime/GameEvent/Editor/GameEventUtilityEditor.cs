using UnityEditor;
using UnityEngine;

namespace Nemuge.GameEvent {
    [CustomEditor(typeof(GameEventUtility))]
    public class GameEventUtilityEditor : Editor {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            var handler = (GameEventUtility) target;
            handler.GenerateTemplateTextAssets();

            if (string.IsNullOrEmpty(handler.eventName)) return;
            if (string.IsNullOrEmpty(handler.scriptLocation)) return;
            if (!AssetDatabase.IsValidFolder(handler.scriptLocation)) return;

            if (GUILayout.Button("Generate Event Scripts")) {
                handler.GenerateEventScripts();
                handler.ClearData();
            }
        }
    }
}