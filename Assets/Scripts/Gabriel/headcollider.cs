using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headcollider : MonoBehaviour
{
    public Mob mob;
    public GameObject bul;
    
    void Start()
    {
       bul.SetActive(false);
       // Recherche du PlayerController dans la scène
        if (mob == null)
        {
            mob = FindObjectOfType<Mob>(); // Recherche du PlayerController dans la scène
        }
    }

    // Update is called once per frame
    void Update()
    {}
       private void OnTriggerEnter2D(Collider2D collision)
    {
        // Vérifie si l'objet entrant est la tête du mob
        
        if (collision.TryGetComponent<PlayerController>(out PlayerController player))
        {
            // Si le joueur touche la tête, tue le mob
            mob.Die();
            bul.SetActive(true);

        }
    }
} 

