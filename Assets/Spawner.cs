using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject BoatPrefab;
    public GameObject BubblePrefab;
    public GameObject SeaWeedPrefab;
    public GameObject streamPointPrefab;
    public int totalFrequencyPoints = 0;

    public Spawnable[] spawnables;

    public float yOffsetMin;
    public float yOffsetMax;
    public float xOffset;

    public int spawnsPerTime = 1;

    private GameController gameController;

    private void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void Start()
    {

        //Difficulty (Spawnrate)
        switch (gameController.difficulty)
        {
            case 0:
                spawnables[0].frequency = 41;
                spawnables[1].frequency = 59;
                break;
            case 1:
                spawnables[0].frequency = 40;
                spawnables[1].frequency = 60;
                break;
            case 2:
                spawnables[0].frequency = 31;
                spawnables[1].frequency = 69;
                break;
        }

        StartCoroutine(StartSpawning());

        StartCoroutine(BubbleSpawning(1, 2));
        StartCoroutine(StreamSpawning(20, 40));
        StartCoroutine(SpawnBoats(40, 100));
    }

    private void FixedUpdate()
    {
        if (Random.Range(1, 100) > 95)
        {
            GameObject seaweed = Instantiate(SeaWeedPrefab, new Vector3(13, -2.95f, 0), Quaternion.identity);
            float offset = Random.Range(0, 1.2f);

            seaweed.transform.position -= new Vector3(0, offset, 0);
            seaweed.transform.localScale = new Vector3(5 + offset, 5 + offset, 0);
        }
    }

    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.B))
        {
            Instantiate(BoatPrefab, new Vector2(13, 3.5f), Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            SpawnStream();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            int bags = 0;
            int food = 0;

            foreach (GameObject b in GameObject.FindGameObjectsWithTag("PlasticBags"))
            {
                bags++;
            }

            foreach (GameObject b in GameObject.FindGameObjectsWithTag("Food"))
            {
                food++;
            }

            Debug.Log("Food: " + food + "; Bags: " + bags);
        }
        */

        totalFrequencyPoints = 0;

        foreach (Spawnable spawnable in spawnables)
        {
            totalFrequencyPoints += spawnable.frequency;
        }

    }

    void SpawnStream()
    {
        int index = 0;
        Vector2[] list = SpawnStreamPoints(5);

        foreach (Vector2 point in list)
        {
            GameObject pointObj = Instantiate(streamPointPrefab, point, Quaternion.identity);
            if (index < list.Length - 1)
            {

                Vector2 direction = new Vector2(
                    list[index + 1].x - pointObj.transform.position.x,
                    list[index + 1].y - pointObj.transform.position.y
                );

                pointObj.GetComponent<StreamPoint>().direction = direction;

                pointObj.transform.up = direction;

                var main = pointObj.transform.Find("Particle System").GetComponent<ParticleSystem>().main;

                main.startLifetime = 0.5f;
            }
            else
            {
                pointObj.GetComponent<StreamPoint>().direction = new Vector2(1, 0);
                Destroy(pointObj);
            }

            pointObj.GetComponent<FloatingItem>().Speed = gameController.itemSpeed;

            index++;
        }
    }

    IEnumerator StartSpawning()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            for (int i = 0; i < spawnsPerTime; i++)
            {
                Spawn();
            }
        }
    }

    void Spawn()
    {
        int rnd = Random.Range(0, totalFrequencyPoints);

        int previousFrequencyPoints = 0;

        GameObject prefab = spawnables[0].prefab;

        foreach (Spawnable spawnable in spawnables)
        {
            //Debug.Log("RND: " + rnd + "; Percentage: " + (spawnable.frequency - previousFrequencyPoints));
            if (rnd < spawnable.frequency + previousFrequencyPoints)
            {
                prefab = spawnable.prefab;

                Vector2 pos = new Vector2(13 + Random.Range(-xOffset, xOffset), Random.Range(yOffsetMin, yOffsetMax));

                GameObject instance = Instantiate(prefab, pos, Quaternion.identity);

                return;
            }
            else
            {
                previousFrequencyPoints += spawnable.frequency;
            }


        }


    }

    Vector2[] SpawnStreamPoints(int pointCount)
    {
        Vector2[] streamPoints = new Vector2[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            float previousPointY;

            if (i == 0)
            {
                previousPointY = Random.Range(yOffsetMin, yOffsetMax);
            }
            else
            {
                previousPointY = streamPoints[i - 1].y;
            }



            Vector2 point = new Vector2((i * 2) + 12, previousPointY + Random.Range(-2.0f, 2.0f));

            if (point.y > yOffsetMax)
            {
                point = new Vector2(point.x, yOffsetMax);
            }

            streamPoints[i] = point;
        }

        return streamPoints;
    }

    IEnumerator BubbleSpawning(int min, int max)
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(min, max));
            GameObject bubble = Instantiate(BubblePrefab, new Vector3(Random.Range(-10, 10), -7, 0), Quaternion.identity);
            float size = Random.Range(3.0f, 5.0f);
            bubble.transform.localScale = new Vector3(size, size, 1);

        }
    }

    IEnumerator StreamSpawning(int min, int max)
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(min, max));
            SpawnStream();
        }
    }

    IEnumerator SpawnBoats(int min, int max)
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(min, max));
            Instantiate(BoatPrefab, new Vector2(13, 3.5f), Quaternion.identity);
        }
    }
}

[System.Serializable]
public class Spawnable
{
    public GameObject prefab;
    public int frequency;
}
