using Managers;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(ResourceManager))]
    public class ResourceManagerEditor : UnityEditor.Editor
    {
        private static int _amount = 100;
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            ResourceManager manager = (ResourceManager) target;

            _amount = EditorGUILayout.IntField("Amount", _amount);
            if (GUILayout.Button("Add Gold"))
            {
                manager.AddGold(_amount);
            }
        }
    }
}