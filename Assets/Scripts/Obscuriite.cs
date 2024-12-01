using System.Collections;
using UnityEngine;

public class Obscuriite : MonoBehaviour
{
    private void Start()
    {
        Debug.Log(GameManager.Instance.returnedOnce);
        if (GameManager.Instance.returnedOnce)
        {
            gameObject.SetActive(false);
        }
    }
}
