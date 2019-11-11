using UI;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(GameOver))]
    public class GameOverEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GameOver script = (GameOver) target;

            if (GUILayout.Button("Show"))
                script.Show();
        }
    }
}