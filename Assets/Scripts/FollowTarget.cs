using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform player;      // Référence au joueur.
    [SerializeField] private Vector3 offset;        // Décalage de la caméra.
    [SerializeField] private float smoothTime = 0.3f; // Temps pour atteindre la position (plus petit = plus rapide).

    private Vector3 velocity = Vector3.zero;        // Vitesse actuelle du SmoothDamp (nécessaire pour le calcul).

    void FixedUpdate()
    {
        // Position cible avec offset.
        Vector3 targetPosition = player.position + offset;

        // Interpolation fluide vers la position cible.
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
