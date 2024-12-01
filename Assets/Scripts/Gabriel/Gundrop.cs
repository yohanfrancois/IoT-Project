using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gundrop : MonoBehaviour
{
    public GameObject bulletManager;
    void Start()
    {
        bulletManager.SetActive(false);
    }

     private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.TryGetComponent<PlayerController>(out PlayerController player))
        {
            bulletManager.SetActive(true);
            Destroy(gameObject);
        }
    }
} 


