using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour {

    private GameController gameController;

    public float Speed;

	void Update () {
        transform.position += new Vector3(0,1,0) * Time.deltaTime * Speed/transform.localScale.x;

        if (transform.position.y > 2.65f)
        {
            gameController.PlaySound(gameController.sounds[2], 0.4f);
            Destroy(gameObject);
        }
	}

    private void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

}
