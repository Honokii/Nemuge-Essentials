using System.IO;
using UnityEditor;
using UnityEngine;

namespace Nemuge.GameEvent {
    [CreateAssetMenu(menuName = "Nemuge/Game Event Utility", fileName = "NewGameEventUtility")]
    public class GameEventUtility : ScriptableObject {
        private const string TemplateIdEventName = "#EVENTNAME#";
        
        public string baseGameEventTemplateLocation;
        public string baseGameEventListenerTemplateLocation;
        [Space]
        public string eventName;
        public string scriptLocation;

        private TextAsset _baseGameEventTemplateTextAsset;
        private TextAsset _baseGameEventListenerTemplateTextAsset;
        
        public void ClearData() {
            eventName = string.Empty;
            scriptLocation = string.Empty;
        }

        public void GenerateEventScripts() {
            if (!AreTemplatesReady) {
                Debug.Log("BaseGameEventHandler: GenerateEventScripts! some templates are not found.");
                return;
            }

            var baseGameEventTemplate = _baseGameEventTemplateTextAsset.text;
            var baseGameEventListenerTemplate = _baseGameEventListenerTemplateTextAsset.text;

            baseGameEventTemplate = baseGameEventTemplate.Replace(TemplateIdEventName, eventName);
            baseGameEventListenerTemplate = baseGameEventListenerTemplate.Replace(TemplateIdEventName, eventName);

            var validLoc = scriptLocation.Replace("Assets/", ""); //remove the Assets/ on the location as this is also in the Application.dataPath
            
            var eventFilePath = $"{Application.dataPath}/{validLoc}/{eventName}.cs";
            var eventListenerFilePath = $"{Application.dataPath}/{validLoc}/{eventName}Listener.cs";
            
            using (var eventWriter = new StreamWriter(eventFilePath, true)) {
                eventWriter.Write(baseGameEventTemplate);
                eventWriter.Close();
            }

            using (var eventListenerWriter = new StreamWriter(eventListenerFilePath, true)) {
                eventListenerWriter.Write(baseGameEventListenerTemplate);
                eventListenerWriter.Close();
            }
            
            AssetDatabase.Refresh();
        }

        public void GenerateTemplateTextAssets() {
            if (AreTemplatesReady) return;
            
            if (_baseGameEventTemplateTextAsset == null) {
                _baseGameEventTemplateTextAsset = AssetDatabase.LoadAssetAtPath(baseGameEventTemplateLocation, typeof(TextAsset)) as TextAsset;
            }

            if (_baseGameEventListenerTemplateTextAsset == null) {
                _baseGameEventListenerTemplateTextAsset = AssetDatabase.LoadAssetAtPath(baseGameEventListenerTemplateLocation, typeof(TextAsset)) as TextAsset;
            }
        }

        private bool AreTemplatesReady =>
            _baseGameEventTemplateTextAsset != null && _baseGameEventListenerTemplateTextAsset != null;
    }
}