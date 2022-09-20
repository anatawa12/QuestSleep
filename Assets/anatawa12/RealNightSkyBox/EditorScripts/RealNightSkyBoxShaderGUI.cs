using UnityEditor;
using UnityEngine;

namespace anatawa12.RealNightSkyBox
{
    public class RealNightSkyBoxShaderGUI : UnityEditor.ShaderGUI
    {
        public static readonly Quaternion Initial = Quaternion.Euler(90, 180, 0);

        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
        {
            base.OnGUI(materialEditor, properties);

            // when you modify this logic, you also must update ShaderRotationSetter.
            MaterialProperty prop = FindProperty("_RotQuot", properties);
            var quot = new Quaternion(prop.vectorValue.x, prop.vectorValue.y, prop.vectorValue.z, prop.vectorValue.w);
            quot *= Quaternion.Inverse(Initial);
            var euler = quot.eulerAngles;

            EditorGUI.BeginChangeCheck();
            {
                euler.x = EditorGUILayout.FloatField("Latitude", euler.x);
                euler.y = EditorGUILayout.FloatField("NorthDirection", euler.y);
                euler.z -= 180;
                euler.z = EditorGUILayout.FloatField("Rotation", euler.z);
                euler.z += 180;
            }
            if (EditorGUI.EndChangeCheck())
            {
                quot = Quaternion.Euler(euler); 
                quot *= Initial;
                prop.vectorValue = new Vector4(quot.x, quot.y, quot.z, quot.w);
            }
        }
    }
}
