using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class LightIntensityController : MonoBehaviour
{
    [SerializeField] private Light2D lightToControl; // La lumière à contrôler.
    [SerializeField] private Slider intensitySlider; // Le slider lié à la lumière.

    void Start()
    {
        // Initialiser le slider avec la valeur actuelle de l'intensité de la lumière.
        if (lightToControl != null && intensitySlider != null)
        {
            intensitySlider.value = lightToControl.intensity;
            intensitySlider.onValueChanged.AddListener(ChangeLightIntensity);
        }
    }

    void ChangeLightIntensity(float value)
    {
        if (lightToControl != null)
        {
            lightToControl.intensity = value;
        }
    }
}