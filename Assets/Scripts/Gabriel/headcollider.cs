using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headcollider : MonoBehaviour
{
    public Mob mob;
    public GameObject gun;
    
    void Start()
    {
       gun.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {}
       private void OnTriggerEnter2D(Collider2D collision)
    {
        // Vérifie si l'objet entrant est la tête du mob
        
        if (collision.TryGetComponent(out PlayerController player))
        {
            // Si le joueur touche la tête, tue le mob
            mob.Die();
            gun.SetActive(true);

        }
    }
} 

