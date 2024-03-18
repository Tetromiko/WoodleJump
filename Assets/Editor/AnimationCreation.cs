using System.Collections.Generic;
using System.IO;
using Data;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Editor
{
    public class AnimationCreation : EditorWindow
    {
        private string _path = "Assets/ObjectData/Animations/";
        private ObjectData _objectData;
        private List<string> _animations = new ();

        [MenuItem("Window/Animation Creation Window")]
        public static void ShowWindow()
        {
            GetWindow(typeof(AnimationCreation));
        }

        private void OnGUI()
        {
            _path = EditorGUILayout.TextField("Path", _path);
            _objectData = (ObjectData)EditorGUILayout.ObjectField("Object", _objectData, typeof(ObjectData), false);

            EditorGUILayout.LabelField("Animations:");
            
            EditorGUILayout.BeginVertical(GUI.skin.box);
            
            for (int i = 0; i < _animations.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                _animations[i] = EditorGUILayout.TextField(_animations[i]);
                if (GUILayout.Button("Remove"))
                {
                    _animations.RemoveAt(i);
                }
                EditorGUILayout.EndHorizontal();
            }
            
            if (GUILayout.Button("New"))
            {
                _animations.Add("New animation");
            }
            
            EditorGUILayout.EndVertical();
            
            var button = GUILayout.Button("Create");
            if (button)
            {
                var localPath = _path + _objectData.GetType().Name.Replace("Data","s") + "/" + _objectData.name + "/";
                if (!Directory.Exists(localPath))
                {
                    Directory.CreateDirectory(localPath);
                }
                var controller = AnimatorController.CreateAnimatorControllerAtPath(localPath + _objectData.name + "AnimationController.controller");
                foreach (var animation in _animations)
                {
                    var animationClip = new AnimationClip();
                    AssetDatabase.CreateAsset(animationClip, localPath + _objectData.name + animation + ".anim");
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                    
                    var state = controller.layers[0].stateMachine.AddState(animationClip.name);
                    state.motion = animationClip;
                }
            }
        }
    }
}