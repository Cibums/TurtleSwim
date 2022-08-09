using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{

    GameController gameController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 100), ForceMode2D.Impulse);
            PlayerController pc = collision.gameObject.GetComponent<PlayerController>();

            pc.ResetHealth();
            pc.gameObject.transform.position = transform.position - new Vector3(3f,1.5f,0);
            StartCoroutine(HideForSeconds(collision.transform, 2));
            
        }
    }

    private void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    IEnumerator HideForSeconds(Transform tf, float seconds)
    {
        tf.gameObject.SetActive(false);
        gameController.KillAllPlasticBags();
        yield return new WaitForSeconds(seconds);
        tf.gameObject.SetActive(true);
    }
}
