using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class LightIntensityController : MonoBehaviour
{
    [SerializeField] private Light2D lightPrince; // La lumière à contrôler.
    [SerializeField] private Light2D lightPrincess; // La lumière à contrôler.
    [SerializeField] private Light2D lightGeneral; // La lumière à contrôler.
    [SerializeField] private TriggerDialogue changeScene; // La lumière à contrôler.

    private bool isPrince = false;
    private bool isPrincess = false;
    private bool isGeneral = false;

    public void ChangeLightPrince(float value)
    {
        lightPrince.intensity = value;
        if(value == 5)
        {
            isPrince = true;
            if(isPrince && isPrincess && isGeneral)
            {
                changeScene.isLightOpen = true;
            }
        }
    }

    public void ChangeLightPrincess(float value)
    {
        lightPrincess.intensity = value;
        if(value == 5)
        {
            isPrincess = true;
            if(isPrince && isPrincess && isGeneral)
            {
                changeScene.isLightOpen = true;
            }
        }
    }

    public void ChangeLightGeneral(float value)
    {
        lightGeneral.intensity = value;
        if(value == 5)
        {
            isGeneral = true;
            if(isPrince && isPrincess && isGeneral)
            {
                changeScene.isLightOpen = true;
            }
        }

    }
}