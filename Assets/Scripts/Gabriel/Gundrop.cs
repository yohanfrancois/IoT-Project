using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gundrop : MonoBehaviour
{
    public GameObject bulletManager;
    void Start()
    {
        bulletManager.SetActive(false);
        if (GameManager.Instance.hasGun){
            bulletManager.SetActive(true);
        }
    }

     private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.TryGetComponent<PlayerController>(out PlayerController player))
        {
            GameManager.Instance.hasGun=true;
            bulletManager.SetActive(true);
            Destroy(gameObject);
        }
    }
} 


