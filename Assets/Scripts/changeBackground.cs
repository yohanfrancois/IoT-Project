using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeBackground : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private SpriteRenderer fineSpriteRenderer;
    [SerializeField] private SpriteRenderer glitchedSpriteRenderer;
    void Start()
    {
        AfficheBackground();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AfficheBackground(){
        if(GameManager.Instance.unlockedPlatform){
            //Changement de background
            //print("unlocked");
            fineSpriteRenderer.color = new Color(1f,1f,1f,1f);
            glitchedSpriteRenderer.color = new Color(1f,1f,1f,0f);
        }
        else{
            //print("locked");
            //Changement de background
            fineSpriteRenderer.color = new Color(1f,1f,1f,0f);
            glitchedSpriteRenderer.color = new Color(1f,1f,1f,1f);
        }
    }
}
