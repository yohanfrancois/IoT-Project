using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform target;

    private Vector2 _currentVelocity;


    void Start()
    {
        transform.position = target.position;
    }

    // Update is called once per frame
    protected void Update()
    {
        transform.position = Vector2.SmoothDamp(transform.position, target.position, ref _currentVelocity, 0.2f);
    }
}
