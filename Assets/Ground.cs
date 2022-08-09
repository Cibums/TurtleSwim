using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour {

    bool spawned = false;

    public float DestroyPoint;
    public float InstantiateX;


	void Update () {
        if (transform.position.x <= 0 && spawned == false)
        {
            GameObject go = Instantiate(gameObject, new Vector3(InstantiateX,transform.position.y,0), Quaternion.identity);
            spawned = true;

            go.GetComponent<Ground>().spawned = false;
        }

        if (transform.position.x < DestroyPoint)
        {
            Destroy(gameObject);
        }
	}
}
