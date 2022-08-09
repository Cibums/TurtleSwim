using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Count : MonoBehaviour
{
    public TMPro.TMP_Text bagscountText;
    private GameController gameController;
    void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void Update()
    {
        bagscountText.text = gameController.bagsEaten.ToString();
    }
}
