using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticBag : MonoBehaviour
{

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
        if (gameController.dead == false)
        {
            gameController.bagsEaten++;
            gameController.PlaySound(gameController.sounds[0]);
            Destroy(gameObject);
        }
    }
}
