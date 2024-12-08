using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LowLight : MonoBehaviour
{
    [SerializeField] private Light2D lowLight;
    void Start()
    {
        if (GameManager.Instance.hasPressedStart && !GameManager.Instance.unlockedPlatform)
        {
            lowLight.intensity = 0.5f;
        }
        else
        {
            lowLight.intensity = 5f;
        }
    }


}
