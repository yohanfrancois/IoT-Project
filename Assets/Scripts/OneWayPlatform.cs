using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private GameObject player;
    private SpriteRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (player.transform.position.y > transform.position.y)
            {
                renderer.color = new Color(1f, 1f, 1f, 1f);
            }
        }
    }
}
