using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gundrop : MonoBehaviour
{
    

     private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.TryGetComponent(out PlayerController player))
        {
            GameManager.Instance.hasGun=true;
            Destroy(gameObject);
        }
    }
} 


