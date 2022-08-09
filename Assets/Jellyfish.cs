using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jellyfish : MonoBehaviour {
    
    public int healthBoost;

    private GameController gameController;

    private void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PickUp();
        }
    }

    void PickUp()
    {
        if(gameController.dead == false)
        {
            gameController.health += healthBoost;
            gameController.PlaySound(gameController.sounds[1]);
            Destroy(gameObject);
        }
    }
}
