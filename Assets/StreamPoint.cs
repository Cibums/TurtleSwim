using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamPoint : MonoBehaviour {

    public float Force;
    public Vector2 direction;

    private PlayerController playerController;
    GameController gameController;

    private void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        playerController = gameController.playerController;
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerController.isInStream = true;
        }
        else
        {
            return;
        }

        Rigidbody2D rb;

        try
        {
            rb = collision.gameObject.GetComponent<Rigidbody2D>();
        }
        catch
        {
            return;
        }

        rb.AddForce(Force * direction);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerController.isInStream = false;
        }
    }

}
