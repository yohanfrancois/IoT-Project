using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class Luminosity : MonoBehaviour
{
    [SerializeField] private Slider luminositySlider;
    [SerializeField] private GameObject code;
    [SerializeField] private Light2D lightCode;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Color oldColor = code.GetComponent<SpriteRenderer>().color;
        code.GetComponent<SpriteRenderer>().color = new 
            Color(oldColor.r, oldColor.g, oldColor.b, luminositySlider.value);
        lightCode.intensity = luminositySlider.value;
    }
}
