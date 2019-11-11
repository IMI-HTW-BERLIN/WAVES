using UI;
using UnityEditor;
using UnityEngine;
using Waves;

namespace Editor
{
    [CustomEditor(typeof(WaveManager))]
    public class WaveEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            WaveManager script = (WaveManager) target;

            if (GUILayout.Button("Spawn Wave"))
                script.SpawnWave();
        }
    }
}