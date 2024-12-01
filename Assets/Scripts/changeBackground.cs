using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeBackground : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject BGFine;
    [SerializeField] GameObject BGGlitched;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AfficheBackground(){
        SpriteRenderer fine = BGFine.GetComponent<SpriteRenderer>();
        SpriteRenderer glitched = BGGlitched.GetComponent<SpriteRenderer>();
        if(GameManager.Instance.unlockedPlatform){
            //Changement de background
            fine.color = new Color(1f,1f,1f,1f);
            glitched.color = new Color(1f,1f,1f,0f);
        }
        else{
            //Changement de background
            fine.color = new Color(1f,1f,1f,0f);
            glitched.color = new Color(1f,1f,1f,1f);
        }
    }
}
