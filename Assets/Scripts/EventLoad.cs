using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EventLoad : MonoBehaviour
{
    private Button _startButton;
    private Animator _animator;
    private bool _isAnimationPlaying = false;

    void Start()
    {
        _startButton = GameObject.Find("Button").GetComponent<Button>();
        _animator = _startButton.GetComponent<Animator>();

        _startButton.onClick.AddListener(() => StartCoroutine(ButtonSelected()));
    }

    IEnumerator ButtonSelected()
    {
        if (_animator != null)
        {
            _animator.SetBool("IsClicked", true); // Assure-toi que ce trigger existe dans l'Animator.
            _isAnimationPlaying = true;

            yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length); // Attend la fin de l'animation.

            _isAnimationPlaying = false;
        }

        Debug.Log("Vous avez trouvé le bouton " + _startButton.name);
    }
}