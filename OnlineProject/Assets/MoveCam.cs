using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam : MonoBehaviour {
    public float speed = 10;
	void Update () {
        if (Input.GetKey(KeyCode.Q))
        {
            transform.position += Vector3.right * -1 * Time.deltaTime* speed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * Time.deltaTime* speed;
        }
        else if (Input.GetKey(KeyCode.Z))
        {
            transform.position += Vector3.up * Time.deltaTime* speed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.up * -1 * Time.deltaTime* speed;
        }
    }
}
