using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    Vector3 oldPos;

	// Use this for initialization
	void Start () {
        oldPos = Vector3.zero;
    }

    private void OnMouseDrag()
    {
        if(oldPos != Vector3.zero)
        {
            Vector3 newPos = (Input.mousePosition - oldPos);
            newPos.z = Input.mouseScrollDelta.y;
            transform.position = transform.position + newPos.normalized * 20 * Time.deltaTime;
        }
        oldPos = Input.mousePosition;

        if(Input.GetKey(KeyCode.W))
        {
            transform.Rotate(2, 0, 0);
        }
        else if (Input.GetKey(KeyCode.X))
        {
            transform.Rotate(0, 2, 0);
        }
        else if (Input.GetKey(KeyCode.C))
        {
            transform.Rotate(0, 0, 2);
        }
    }
}
