using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WaterfallShader))]
public class WaterfallShaderEditor : Editor
{


    private WaterfallShader waterfallShader;
    private bool isReady = false;
    private GameObject selected;

    private WaterfallShaderEditor waterfallShaderEditor;
    private new SerializedObject serializedObject;

    private SerializedProperty flowSpeed;
    private SerializedProperty offset;
    private SerializedProperty tiling;
    private SerializedProperty blackColor;
    private SerializedProperty darkenColor;
    private SerializedProperty whiteColor;
    private SerializedProperty waterfallMasks;


    private GUILayoutOption[] colorBoxLayout;
    private void OnEnable()
    {
        isReady = false;
        selected = Selection.activeGameObject;
        if (!selected) return;

        if (!waterfallShader)
            waterfallShader = selected.GetComponent<WaterfallShader>();

        serializedObject = new SerializedObject(waterfallShader);
        GetProperties();
        EditorUtility.SetDirty(target);


        //SetColorBoxLayoutOption();


        isReady = true;
    }

    //private void SetColorBoxLayoutOption()
    //{
    //    colorBoxLayout[0] = GUILayout.Width(10);
    //    colorBoxLayout[1] = GUILayout.Height(10);

    //}



    public override void OnInspectorGUI()
    {
        if (!isReady) return;

        serializedObject.Update();
        ShowWaterfallUI();
        Undo.RecordObject(target, "Component");

        serializedObject.ApplyModifiedProperties();
        waterfallShader.SetProperties();
    }


    private void GetProperties()
    {
        Debug.Log("running");
        waterfallShader.GetProperties();

        flowSpeed = serializedObject.FindProperty("FlowSpeed");
        tiling = serializedObject.FindProperty("Tiling");
        offset = serializedObject.FindProperty("Offset");
        blackColor = serializedObject.FindProperty("WhiteColor");
        whiteColor = serializedObject.FindProperty("BlackColor");
        darkenColor = serializedObject.FindProperty("DarkenColor");
        waterfallMasks = serializedObject.FindProperty("WaterfallMasks");
    }


    private void ShowWaterfallUI()
    {
        if (!selected) return;
        TopUI();
        SettingsUI();


    }

    private void TopUI()
    {
        EditorGUILayout.Space();
        using (new GUILayout.HorizontalScope())
        {
            GUILayout.Label("My first waterfalls shade ui ", new GUIStyle(EditorStyles.boldLabel)
            {
                alignment = TextAnchor.MiddleLeft,
                wordWrap = true,
                fontSize = 11,
            });
        }
        EditorGUILayout.Space();

        FontStyle originalFontStyle = EditorStyles.label.fontStyle;
        EditorStyles.label.fontStyle = FontStyle.Bold;

        AddSliderSettings("Adjusts the speed streaming water", flowSpeed, 0f, 10f);
        AddSliderSettings("Adjusts the size of tiles", tiling, 0f, 15f);
        AddSliderSettings("Offsets the texture", offset, 0f, 1f);

        ExplanationBox("Colors");
        AddColorSettings(blackColor, "Black Color");
        AddColorSettings(whiteColor, "White Color");
        AddColorSettings(darkenColor, "Darken Color");




        DetiledExplanationBox("R channel: Main Color Mask,\n" +
                              "G channel: Highlight Pattern,\n" +
                              "B channel: Variation Map");

        EditorGUILayout.ObjectField(waterfallMasks, typeof(Texture2D));
    }
    private void SettingsUI()
    {

        
    }
    private void AddColorSettings(/*string explanation,*/ SerializedProperty property, string colorName)
    {

        //DetiledExplanationBox(explanation);
        using (new GUILayout.HorizontalScope())
        {
            EditorGUILayout.PropertyField(property, new GUIContent(colorName) );
        }
    }


    private void AddSliderSettings(string explanation,SerializedProperty property ,float minFloat, float maxFloat)
    {
        DetiledExplanationBox(explanation);
        using (new GUILayout.HorizontalScope())
        {
            EditorGUILayout.Slider(property, minFloat, maxFloat);
        }
    }


    private void DetiledExplanationBox(string inputText)
    {
        using (new GUILayout.HorizontalScope())
        {
            GUILayout.Label(inputText, new GUIStyle(EditorStyles.helpBox)
            {
                alignment = TextAnchor.MiddleLeft,
                wordWrap = true,
                fontSize = 12,
            });
        }
    }
    private void ExplanationBox(string inputText)
    {
        using (new GUILayout.HorizontalScope())
        {
            GUILayout.Label(inputText, new GUIStyle(EditorStyles.helpBox)
            {
                alignment = TextAnchor.MiddleLeft,
                wordWrap = true,
                fontSize = 14,
            });
        }
    }
}
