using System.Collections.Generic;
using Kinel.VideoPlayer.Scripts;
using Kinel.VideoPlayer.Scripts.Parameter;
using Kinel.VideoPlayer.Udon;
using UdonSharp;
using UdonSharpEditor;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kinel.VideoPlayer.Editor.Internal
{
    public static class KinelEditorUtilities
    {
        public static void UpdateKinelVideoUIComponents(MonoBehaviour component)
        {
            UpdateKinelVideoUIComponents(component, null);
        }
        
        public static void UpdateKinelVideoUIComponents(MonoBehaviour component, Object udon)
        {
            var targetObject = component.gameObject;
            var components = targetObject.GetUdonSharpComponentsInChildrenByKinel<KinelVideoPlayerUI>();
            foreach (var ui in components)
            {
                if (ui == null)
                {
                    continue;
                }
                
                Undo.RecordObject(ui, "kinelUI Udon Update");

                var editor = UnityEditor.Editor.CreateEditor(ui, typeof(KinelUIEditor)) as KinelUIEditor;

                var serializedObject = editor.serializedObject;
                serializedObject.Update();
                // editor.a
                if (udon != null)
                {
                    var videoPlayer = serializedObject.FindProperty(nameof(KinelVideoPlayerUI.videoPlayer));
                    videoPlayer.objectReferenceValue = videoPlayer.objectReferenceValue;
                    serializedObject.ApplyModifiedProperties();
                    serializedObject.Update();
                }
                
                editor.AutoFillProperties();
                
                editor.ApplyUdonProperties();
                
                serializedObject.ApplyModifiedProperties();
                UdonSharpEditorUtility.CopyProxyToUdon(ui);
                EditorUtility.SetDirty(ui);
            }
        }
        
        

        public static T[] GetUdonSharpScripts<T>(GameObject parent) where T: UdonSharpBehaviour
        {
            T[] targets = parent.GetUdonSharpComponentsInChildrenByKinel<T>();
            if (targets.Length == 0)
            {
                // UdonSharp
                // targets = GameObject.FindObjectsOfType<T>();
                var tempList = new List<T>();
                foreach (var rootObject in SceneManager.GetActiveScene().GetRootGameObjects())
                {
                    tempList.AddRange(rootObject.GetUdonSharpComponentsInChildrenByKinel<T>());
                }
                // if (targets.Length == 0)
                //     return targets;
                return tempList.ToArray();

            }

            return targets;
        }

        public static FillResult FillUdonSharpInstance<T>(ref SerializedProperty targetProperty, GameObject parent, bool overwrite) where T: UdonSharpBehaviour
        {
            if (targetProperty.objectReferenceValue != null && !overwrite)
                return FillResult.AlreadyExistence;

            var instance = GetUdonSharpScripts<T>(parent);

            if(instance.Length == 0)
                return FillResult.NoExistence;

            if (instance.Length >= 2)
                return FillResult.MultipleExistence;

            targetProperty.objectReferenceValue = instance[0];
            targetProperty.serializedObject.ApplyModifiedProperties();
            
            return FillResult.Success;
        }

        public static void DrawFillMessage(int fillResultInteger /*, params string[] messages*/)
        {
            switch (fillResultInteger)
            {
                case((int)FillResult.Success):
                    EditorGUILayout.HelpBox("????????????????????????????????????", MessageType.Info);
                    break;
                case((int)FillResult.AlreadyExistence):
                    EditorGUILayout.HelpBox("???????????????????????????", MessageType.Info);
                    break;
                case((int)FillResult.MultipleExistence):
                    EditorGUILayout.HelpBox("???????????????????????????????????????????????????????????????????????????????????????????????????????????????", MessageType.Info);
                    break;
                case((int)FillResult.NoExistence):
                    EditorGUILayout.HelpBox("?????????????????????????????????????????????", MessageType.Info);
                    break;
                case((int)FillResult.NotInitialized):
                    EditorGUILayout.HelpBox("... ???????????? ...", MessageType.Info);
                    break;
                default:
                    EditorGUILayout.HelpBox("??????????????????????????????????????????(??????????????????#30)", MessageType.Error);
                    return;
            }
        }
        
    }
}