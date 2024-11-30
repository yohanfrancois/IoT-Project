using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EventLoad : MonoBehaviour
{
    private Button _startButton;
    private Button _settingsButton;
    [SerializeField] private GameObject _platform;
    [SerializeField] private GameObject _light;
    [SerializeField] private GameObject _light2;
    private Animator _animator;
    private Animator _animatorLight;
    private Animator _animatorLight2;
    private bool _isAnimationPlaying = false;
    private bool _isBroken = false;
    [SerializeField] private GameObject _pauseScreen;

    void Start()
    {
        _startButton = GameObject.Find("Button").GetComponent<Button>();
        _settingsButton = GameObject.Find("Settings").GetComponent<Button>();
        _animator = _platform.GetComponent<Animator>();
        _animatorLight = _light.GetComponent<Animator>();
        _animatorLight2 = _light2.GetComponent<Animator>();
        

        _startButton.onClick.AddListener(() => StartCoroutine(ButtonSelected()));
        _settingsButton.onClick.AddListener(() => StartCoroutine(SettingsSelected()));
    }

    IEnumerator ButtonSelected()
    {
        if (_animator != null)
        {
            _animator.SetBool("IsClicked", true); // Déclenche l'animation.
            _animatorLight.SetBool("IsClicked", true); // Déclenche l'animation.
            _isAnimationPlaying = true;

            // Attendre que l'animation commence.
            yield return new WaitForSeconds(1f);
            _animatorLight2.SetBool("IsClicked", true); // Déclenche l'animation.

            // Attendre que l'animation se termine.
            while (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            {
                yield return null;
            }
            
            // Éteindre les lumières et activer le mur.
            LightController lightController = FindObjectOfType<LightController>();
            if (lightController != null)
            {
                lightController.LightOff();
                Debug.Log("Mur activé et lumières éteintes !");
            }
        
            yield return new WaitForSeconds(0.2f); // Délai optionnel avant de rallumer.

            lightController?.LightOn(); // Rallumer les lumières.
            
            yield return new WaitForSeconds(1f); // Délai optionnel avant d'éteindre.
            lightController?.LightOff(); // Éteindre les lumières.
            yield return new WaitForSeconds(0.3f); // Délai optionnel avant de rallumer
            lightController?.LightOn(); // Rallumer les lumières.
            yield return new WaitForSeconds(0.7f); // Délai optionnel avant d'éteindre.
            lightController?.LightOff(); // Éteindre les lumières.
            yield return new WaitForSeconds(0.2f); // Délai optionnel avant de rallumer.
            lightController?.LightOn(); // Rallumer les lumières.
            yield return new WaitForSeconds(0.5f); // Délai optionnel avant d'éteindre.
            lightController?.LightOff(); // Éteindre les lumières.
            yield return new WaitForSeconds(0.1f); // Délai optionnel avant de rallumer.
            lightController?.LightOn(); // Rallumer les lumières.
        }

        Debug.Log("Vous avez trouvé le bouton " + _startButton.name);
        _isAnimationPlaying = false;
        _isBroken = true;
    }

    
    IEnumerator SettingsSelected()
    {
        if (_isBroken)
        {
            Debug.LogWarning("Le bouton est cassé !");
            // Activer la page des paramètres.
            _pauseScreen.SetActive(true);
            _isBroken = false;
            yield return null;
        } else if (!_isBroken)
        {
            _pauseScreen.SetActive(false);
            _isBroken = true;
            yield return null;
        }

        Debug.Log("Vous avez trouvé le bouton " + _settingsButton.name);
        
        yield break; // Si tu veux attendre une action avant de revenir.
    }
}