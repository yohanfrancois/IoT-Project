using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gundrop : MonoBehaviour
{
    

     private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.TryGetComponent<PlayerController>(out _))
        {
            GameManager.Instance.hasGun=true;
            PlayerController.Instance.GetSpriteWithGun();
            Destroy(gameObject);
        }
    }
} 


