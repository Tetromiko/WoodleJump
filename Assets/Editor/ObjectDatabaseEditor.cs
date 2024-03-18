using System.Linq;
using Data;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(ObjectDatabase))]
    public class ObjectDatabaseEditor : UnityEditor.Editor
    {
        private string _path = "";

        public override void OnInspectorGUI()
        {
            ObjectDatabase objectDatabase = (ObjectDatabase)target;

            DrawDefaultInspector();

            EditorGUILayout.Space();
            GUILayout.Label("Get data from folder", EditorStyles.centeredGreyMiniLabel);
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Path :");
            GUILayout.Label(_path, GUI.skin.box);
            var browse = GUILayout.Button("Browse");
            if (browse)
            {
                _path = EditorUtility.OpenFolderPanel("Select folder", "", "");
                _path = "Assets" + _path.Substring(Application.dataPath.Length);
            }
            EditorGUILayout.EndHorizontal();
            var getData = GUILayout.Button("Get data");
            if (getData)
            {
                var objectData = AssetDatabase.FindAssets("t:ObjectData", new[] { _path })
                    .Select(AssetDatabase.GUIDToAssetPath)
                    .Select(AssetDatabase.LoadAssetAtPath<ObjectData>)
                    .ToList();
                objectDatabase.SetData(objectData);
            }
        }
    }

}