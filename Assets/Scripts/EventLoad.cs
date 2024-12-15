using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;

public class EventLoad : MonoBehaviour
{
    private Button _startButton;
    private Button _settingsButton;
    [SerializeField] private GameObject _platform;
    [SerializeField] private GameObject _platformBroken;
    [SerializeField] private GameObject _logo;
    [SerializeField] private GameObject _light;
    [SerializeField] private GameObject _light2;
    [SerializeField] private GameObject backgroundFine;
    [SerializeField] private GameObject backgroundGlitched;
    [SerializeField] private GameObject controlBlock;
    [SerializeField] private Light2D generalLight;
    [SerializeField] private ParticleSystem _particleSystemleft;
    [SerializeField] private ParticleSystem _particleSystemright;
    [SerializeField] private ParticleSystem _particleSystemfront;

    
    private Animator _animator;
    private Animator _animatorLogo;
    private Animator _animatorLight;
    private Animator _animatorLight2;
    private bool _isAnimationPlaying = false;
    private bool isMenuActivated = false;

    private bool firstClick = false;

    [SerializeField] private GameObject _pauseScreen;

    void Start()
    {
        
        if (GameManager.Instance.buttonAnimationPlayed)
        {
            PlayerController.Instance.canMove = true; // Débloquer immédiatement le joueur
            _platform.SetActive(false);
            _platformBroken.SetActive(true);
            _logo.SetActive(true);
            if (GameManager.Instance.unlockedPlatform)
            {
                _animatorLogo = _logo.GetComponent<Animator>();
                _animatorLogo.SetBool("Phase1", true);
                if (GameManager.Instance.unlockedInventory)
                {
                    _animatorLogo.SetBool("Phase2", true);
                    if (GameManager.Instance.hasGun)
                    {
                        _animatorLogo.SetBool("Phase3", true);
                    }
                }
            }
        }
        else
        {

            _animator = _platform.GetComponent<Animator>();
                _animatorLight = _light.GetComponent<Animator>();
                _animatorLight2 = _light2.GetComponent<Animator>();
                _animatorLogo = _logo.GetComponent<Animator>();
            
        }
        
    }

    public void HitButton()
    {
        if (!firstClick)
        {
            Dialogue dialogue = new Dialogue
            {
                text = "MAIS !!? Qu’est ce qui se passe ?",
                audioClip = DialogueManager.Instance.dialoguesList[DialogueManager.GetDialogueIndex()],
                characterSprite = DialogueManager.Instance.spritesList[5],
                characterPosition = new Vector3(-881, 475, 0),
                characterRotation = new Vector3(0, 0, -90)
            };
            DialogueManager.Instance.StartDialogue(dialogue);
            _particleSystemleft.Play();
            _particleSystemright.Play();
            AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.firstExplosion);
            firstClick = true;
        }
        else if (!_isAnimationPlaying)
        {
            StartCoroutine(ButtonSelected());
        }

    }

    IEnumerator ButtonSelected()
    {
        if (_animator != null)
        {
            _isAnimationPlaying = true;
            _particleSystemleft.Play();
            _particleSystemright.Play();
            AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.firstExplosion);
            _animator.SetBool("IsClicked", true); // Déclenche l'animation.
            _animatorLight.SetBool("IsClicked", true); // Déclenche l'animation.

            AudioManager.Instance.StopMusic();

            
            // Attendre que l'animation commence.
            yield return new WaitForSeconds(1f);
            _animatorLight2.SetBool("IsClicked", true); // Déclenche l'animation.

           
            
            PlayerController.Instance.GetGlitchedSprite();

            Dialogue dialogue = new Dialogue
            {
                text = "Mais Arrête d’appuyer ! Tu vas casser mon jeu !",
                audioClip = DialogueManager.Instance.dialoguesList[DialogueManager.GetDialogueIndex()],
                characterSprite = DialogueManager.Instance.spritesList[1],
                characterPosition = new Vector3(760, 55, 0),
                characterRotation = new Vector3(0, 0, -75)
            };

            DialogueManager.Instance.StartDialogue(dialogue);

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
                //Debug.Log("Mur activé et lumières éteintes !");
            }
            
            // Débloquer le mouvement du joueur.
            PlayerController.Instance.canMove = true;
            GameManager.Instance.buttonAnimationPlayed = true;
            _logo.SetActive(true);
        
            yield return new WaitForSeconds(0.2f); // Délai optionnel avant de rallumer.

            //Changement de background
            backgroundFine.SetActive(false);
            backgroundGlitched.SetActive(true);
            controlBlock.SetActive(false);
            
            lightController?.LightOn(); // Rallumer les lumières.
            _particleSystemfront.Play();
            AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.secondExplosion);
            yield return new WaitForSeconds(1f); // Délai optionnel avant d'éteindre.
            PlayerController.Instance.transform.position = new Vector3(4.9f, -3.93f, 0);

            AudioManager.Instance.PlayMusic(1);
            GameManager.Instance.hasPressedStart = true;

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
            yield return new WaitForSeconds(0.8f); // Délai optionnel avant de rallumer.
            generalLight.intensity = 0.5f;


            Dialogue dialogue3 = new Dialogue
            {
                text = "AH bah bravo... ça devait être un bête jeu de plateforme, avec un prince qui part sauver sa dulcinée...",
                audioClip = DialogueManager.Instance.dialoguesList[DialogueManager.GetDialogueIndex()],
                characterSprite = DialogueManager.Instance.spritesList[3],
                characterPosition = new Vector3(-750, -400, 0),
                characterRotation = new Vector3(0, 0, -90)
            };

            DialogueManager.Instance.StartDialogue(dialogue3);

            Dialogue dialogue4 = new Dialogue
            {
                text = "Mais regarde ce que t’as fais ! Le prince est bloqué...",
                audioClip = DialogueManager.Instance.dialoguesList[DialogueManager.GetDialogueIndex()],
                characterSprite = DialogueManager.Instance.spritesList[2],
                characterPosition = new Vector3(57, -360, 0),
                characterRotation = new Vector3(0, 0, -105)
            };

            DialogueManager.Instance.StartDialogue(dialogue4);

            Dialogue dialogue5 = new Dialogue
            {
                text = "Bon. J’imagine que c’est à elle de le sauver. Au moins ça rend l’histoire un peu moins cliché...",
                audioClip = DialogueManager.Instance.dialoguesList[DialogueManager.GetDialogueIndex()],
                characterSprite = DialogueManager.Instance.spritesList[0],
                characterPosition = new Vector3(500, 165, 0),
                characterRotation = new Vector3(0, 0, -253)
            };

            DialogueManager.Instance.StartDialogue(dialogue5);
            _isAnimationPlaying = false;


        }

    }

    
    public void SettingsSelected()
    {
            print(isMenuActivated);

            isMenuActivated = !isMenuActivated;
            if(isMenuActivated)
            {
                _pauseScreen.SetActive(true);
            }
            else
            {
                _pauseScreen.SetActive(false);
            }
            
        
    }
}