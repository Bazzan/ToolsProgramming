using UnityEditor.Presets;

public class WaterfallPresetReceiver : PresetSelectorReceiver
{
    Preset initValues;


    WaterfallShader currentSettings;


    public void Init(WaterfallShader current)
    {
        currentSettings = current;
        initValues = new Preset(current);
    }


    public override void OnSelectionChanged(Preset selection)
    {
        
        if(selection != null)
        {
            selection.ApplyTo(currentSettings);
        }
        else
        {
            initValues.ApplyTo(currentSettings);
        }

        currentSettings.SetProperties();


    }

    public override void OnSelectionClosed(Preset selection)
    {
        OnSelectionChanged(selection);
        DestroyImmediate(this);

    }



}
