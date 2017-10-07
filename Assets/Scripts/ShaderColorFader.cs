using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShaderColorFader : MonoBehaviour
{
    public Color SourceColor, TargetColor;

    [Range(0, 1)]
    public float Amount;

    private Material mEarthMaterial;

    void Start()
    {
        mEarthMaterial = GetComponent<MeshRenderer>().materials[0];
    }

    [ContextMenu("Set color")]
    public void SetColor()
    {
        mEarthMaterial.color = Color.Lerp(SourceColor, TargetColor, Amount);
    }

    public void SetColor(Slider slider)
    {
        mEarthMaterial.color = Color.Lerp(SourceColor, TargetColor, slider.value);
    }
}
