using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingItem : MonoBehaviour
{

    private GameController gameController;
    public float SpeedOffset = 0;
    public float Speed;
    public bool noOffset = false;
    public bool dontDestroy = false;

    private void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        if (noOffset)
        {
            Speed = gameController.itemSpeed;
        }
        else
        {
            SpeedOffset = Random.Range(-0.01f, 0.01f);
            Speed = gameController.itemSpeed + SpeedOffset * gameController.SpeedMultiplier;
            transform.localScale -= new Vector3(SpeedOffset * 2, SpeedOffset * 2, 0);
        }
    }

    void Update()
    {
        transform.position += new Vector3(-1, 0, 0) * Speed;

        if (transform.position.x < -20 && dontDestroy == false)
        {
            Destroy(gameObject);
        }
    }
}
