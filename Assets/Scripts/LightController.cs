using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightController : MonoBehaviour
{
    [SerializeField] private Light2D[] lights2D;  // Pour les lumières 2D.
    [SerializeField] private GameObject hiddenWall; // Mur à révéler.

    public void LightOff()
    {
        // Éteindre les lumières 2D.
        foreach (var light in lights2D)
        {
            light.intensity = 0f;
        }

        // Révéler le mur.
        hiddenWall.SetActive(true);
    }
    
    public void LightOn()
    {
        // Allumer les lumières 2D.
        foreach (var light in lights2D)
        {
            light.intensity = 3f;
        }

        // Révéler le mur.
        hiddenWall.SetActive(true);
    }
}
