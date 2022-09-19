
using System;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class DarknessController : UdonSharpBehaviour
{
    public Renderer rendererOfMaterial;
    // expected to be 0 to 1
    public Slider slider;
    public float gamma;
    public float min;
    public float max;

    private float _oldInput = -1;

    private void Update()
    {
        // ReSharper disable once CompareOfFloatsByEqualityOperator
        if (_oldInput == (_oldInput = slider.value)) return;
        var zeroToOne = Mathf.Pow(_oldInput, gamma);
        var value = zeroToOne * (max - min) + min;
        Debug.Log($"{_oldInput} => {zeroToOne} => {value}");
        Material mat = rendererOfMaterial.material;
        if (mat != null) mat.SetFloat("_Alpha", value);
    }
}
