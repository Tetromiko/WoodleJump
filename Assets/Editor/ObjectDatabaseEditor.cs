using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Data;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(ObjectDatabase))]
    public class ObjectDatabaseEditor : UnityEditor.Editor
    {
        private ObjectDatabase _objectDatabase;

        public override async void OnInspectorGUI()
        {
            _objectDatabase = (ObjectDatabase)target;

            DrawDefaultInspector();
            
            var getData = GUILayout.Button("Get data from folder");
            if (getData)
            {
                var selectedFolder = EditorUtility.OpenFolderPanel("Select Folder", "Assets", "");
                var relativePath = selectedFolder.Replace(Application.dataPath, "Assets");
                if (!string.IsNullOrEmpty(relativePath))
                {
                    await AddFilesFromFolderAsync(relativePath);
                }
            }
        }
        private async Task AddFilesFromFolderAsync(string folderPath)
        {
            _objectDatabase.Clear();

            string[] files = Directory.GetFiles(folderPath);
            foreach (var file in files)
            {
                var objectData = AssetDatabase.LoadAssetAtPath<ObjectData>(file);
                if (objectData == null) continue;
                await _objectDatabase.AddDataAsync(objectData);
            }

            Repaint();
            EditorUtility.SetDirty(target);
            AssetDatabase.SaveAssets();
        }
    }

}