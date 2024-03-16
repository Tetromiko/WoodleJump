using System.IO;
using Data;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class ItemCreation : EditorWindow
    {
        private string _path = "Assets/ObjectData/Items/";
        private string _itemName;
        private Sprite _sprite;
        
        [MenuItem("Window/Item Creation Window")]
        public static void ShowWindow()
        {
            GetWindow(typeof(ItemCreation));
        }

        private void OnGUI()
        {
            _path = EditorGUILayout.TextField("Path", _path);
            _itemName = EditorGUILayout.TextField("Name", _itemName);
            _sprite = (Sprite)EditorGUILayout.ObjectField("Sprite", _sprite, typeof(Sprite), false);

            var button = GUILayout.Button("Create");
            if (button)
            {
                var itemData = CreateInstance<ItemData>();
                itemData.sprite = _sprite;
                
                if (!Directory.Exists(_path))
                {
                    Directory.CreateDirectory(_path);
                }
                
                AssetDatabase.CreateAsset(itemData, _path + _itemName + ".asset");
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                _itemName = "";
                _sprite = null;
            }
            Repaint();
        }
    }
}