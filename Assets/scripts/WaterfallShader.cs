using UnityEngine;

#if UNITY_EDITOR
#endif


[ExecuteInEditMode]
[RequireComponent(typeof(MeshRenderer))]
public class WaterfallShader : MonoBehaviour
{
    public Material material;
    public Renderer meshRenderer;
    public Shader Shader;


    public float FlowSpeed;
    public float Tiling;
    public float Offset;

    public Color WhiteColor;
    public Color DarkenColor;
    public Color BlackColor;

    public Texture2D WaterfallMasks;

    private void OnEnable()
    {
        if (!meshRenderer)
            meshRenderer = GetComponent<Renderer>();
        if (!material)
            material = meshRenderer.sharedMaterial;
        GetProperties();
        SetProperties();
    }

    public void GetProperties()
    {
        FlowSpeed = material.GetFloat("_FlowSpeed");
        Tiling = material.GetFloat("_Tiling");
        Offset = material.GetFloat("_Offset");

        WhiteColor = material.GetColor("_WhiteColor");
        BlackColor = material.GetColor("_BlackColor");
        DarkenColor = material.GetColor("_DarkenColor");

        WaterfallMasks = material.GetTexture("_WaterfallMasks") as Texture2D;
    }

    public void SetProperties()
    {
        material.SetFloat("_FlowSpeed", FlowSpeed);
        material.SetFloat("_Tiling", Tiling);
        material.SetFloat("_Offset", Offset);

        material.SetColor("_BlackColor", BlackColor);
        material.SetColor("_WhiteColor", WhiteColor);
        material.SetColor("_DarkenColor", DarkenColor);

        material.SetTexture("_WaterfallMask", WaterfallMasks);
    }



}
