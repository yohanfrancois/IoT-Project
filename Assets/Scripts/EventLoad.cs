using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EventLoad : MonoBehaviour
{
    private Button _startButton;
    private Button _settingsButton;
    private Animator _animator;
    private bool _isAnimationPlaying = false;
    private bool _isBroken = false;

    void Start()
    {
        _startButton = GameObject.Find("Button").GetComponent<Button>();
        _settingsButton = GameObject.Find("Settings").GetComponent<Button>();
        _animator = _startButton.GetComponent<Animator>();

        _startButton.onClick.AddListener(() => StartCoroutine(ButtonSelected()));
        _settingsButton.onClick.AddListener(() => StartCoroutine(SettingsSelected()));
    }

    IEnumerator ButtonSelected()
    {
        if (_animator != null)
        {
            _animator.SetBool("IsClicked", true); // Déclenche l'animation.
            _isAnimationPlaying = true;

            // Attendre que l'animation commence.
            yield return new WaitForSeconds(1f);

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
            yield break;
        }

        Debug.Log("Vous avez trouvé le bouton " + _settingsButton.name);

        // Activer la page des paramètres.

        yield return null; // Si tu veux attendre une action avant de revenir.
    }
}