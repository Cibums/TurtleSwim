using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public bool cheatsEnabled = false;
    public bool AI;
    public GameObject graphics;
    public GameObject deadGraphics;
    public bool dragToNormalPosition = false;
    public Vector2 normalPos;
    public Image health;
    public float SpeedVertical;
    public float SpeedHorizontal;
    GameController gameController;
    public GameObject deadPanel;
    private Rigidbody2D rb;
    public float DeathSpeed;
    public float DeathSpeedOffset = 0;
    public bool isInStream = false;

    bool saved = false;

    private void Start()
    {
        gameController.score = 0;
        
        gameController.playerController = this;
    }

    private void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        if (deadPanel != null)
        {
            deadPanel.SetActive(false);
        }
        DeathSpeed = gameController.itemSpeed + DeathSpeedOffset;
        rb = GetComponent<Rigidbody2D>();
    }

    public void ResetHealth()
    {
        gameController.health = gameController.maxHealth;
        gameController.bagsEaten = 0;
    }

    void Update()
    {
        //Cheats

        if (cheatsEnabled)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                AI = !AI;
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                gameController.health = 0;
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                ResetHealth();
            }
        }

        //
            if (gameController.health > gameController.maxHealth)
            {
                gameController.health = gameController.maxHealth;
            }
        health.fillAmount = gameController.health / 1000;
        if(gameController.health <= 0)
        {
            graphics.SetActive(false);
            deadGraphics.SetActive(true);
            rb.gravityScale = 1;
            deadPanel.SetActive(true);
            gameController.dead = true;
            transform.position += new Vector3(-1, 0, 0) * DeathSpeed;
            if (transform.position.x < -20)
            {
                Destroy(gameObject);
            }
            if (gameController.score > gameController.highScore && saved == false)
            {
                saved = true;
                gameController.highScore = gameController.score;
                gameController.SaveHighScore();
            }

            
        }
        else if(AI == false)
        {
            if (isInStream == false)
            {
                if (Input.GetButton("Vertical"))
                {
                    if (Input.GetAxis("Vertical") > 0)
                    {
                        transform.localRotation = new Quaternion(0, 0, 10, 90);
                    }
                    else
                    {
                        transform.localRotation = new Quaternion(0, 0, -10, 90);
                    }

                    rb.AddForce(new Vector2(0, 1) * SpeedVertical * Input.GetAxis("Vertical"));

                }
                else
                {
                    transform.localRotation = new Quaternion(0, 0, 0, 90);
                }

                if (Input.GetButton("Horizontal"))
                {
                    rb.AddForce(new Vector2(1, 0) * SpeedHorizontal * Input.GetAxis("Horizontal"));
                }
            }
        }
        else
        {

            
            //AI som spelar själv

            GameObject nearestFood = null;
            

            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Food"))
            {
                if (nearestFood == null)
                {
                    nearestFood = go;
                }
                else if (Vector2.Distance(go.transform.position, transform.position) < Vector2.Distance(nearestFood.transform.position, transform.position))
                {
                    nearestFood = go;
                }
            }

            Debug.Log(nearestFood);


            if (nearestFood.transform.position.y > transform.position.y)
            {
                rb.AddForce(new Vector2(0, 1) * SpeedVertical);
            }
            else
            {
                rb.AddForce(new Vector2(0, -1) * SpeedVertical);
            }

            if (nearestFood.transform.position.x > -9 && nearestFood.transform.position.x < 9)
            {
                if (nearestFood.transform.position.x > transform.position.x)
                {
                    rb.AddForce(new Vector2(1, 0) * SpeedHorizontal);
                }
                else
                {
                    rb.AddForce(new Vector2(-1, 0) * SpeedHorizontal);
                }
            }
        }
    }

    private void LateUpdate()
    {
        if (dragToNormalPosition)
        {
            float distance = Vector2.Distance(transform.position, normalPos);

            rb.AddForce((normalPos - new Vector2(transform.position.x, transform.position.y)) * distance);
        }
    }

}
