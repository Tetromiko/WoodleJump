using System.IO;
using Data;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class PlatformCreation : EditorWindow
    {
        private string _path = "Assets/ObjectData/Platforms/";
        private string _platformName;
        private Sprite _sprite;
        
        [MenuItem("Window/Platform Creation Window")]
        public static void ShowWindow()
        {
            GetWindow(typeof(PlatformCreation));
        }

        private void OnGUI()
        {
            _path = EditorGUILayout.TextField("Path", _path);
            _platformName = EditorGUILayout.TextField("Name", _platformName);
            _sprite = (Sprite)EditorGUILayout.ObjectField("Sprite", _sprite, typeof(Sprite), false);

            var button = GUILayout.Button("Create");
            if (button)
            {
                var platformData = CreateInstance<PlatformData>();
                platformData.sprite = _sprite;

                if (!Directory.Exists(_path))
                {
                    Directory.CreateDirectory(_path);
                }
                
                AssetDatabase.CreateAsset(platformData, _path + _platformName + ".asset");
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                _platformName = "";
                _sprite = null;
            }
            Repaint();
        }
    }
}