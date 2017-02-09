using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube  {

    public Vector3 position;
    public Vector3 rotation;
    public Color color;

    private GameObject linkedCube;

    public Cube()
    {
        rotation = Vector3.zero;
        position = Vector3.zero;
        color = new Color(Random.Range(0f,1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        
    }

    public string serialize()
    {
        if(linkedCube != null)
        {
            position = linkedCube.transform.position;
            rotation = linkedCube.transform.rotation.eulerAngles;
        }
        string data = "(";
        data += position.x + "/";
        data += position.y + "/";
        data += position.z + "/";
        data += rotation.x + "/";
        data += rotation.y + "/";
        data += rotation.z + "/";
        data += color.r + "/";
        data += color.g + "/";
        data += color.b + "/";
        data += ")";
        
        return data;
    }
    
    public bool load(string data)
    {
        bool success = true;
        data = data.Replace('(', ' ');
        data = data.Replace(')', ' ');
        data = data.Trim();
        string[] cube = data.Split('/');

        success = float.TryParse(cube[0], out position.x) && success;
        success = float.TryParse(cube[1], out position.y) && success;
        success = float.TryParse(cube[2], out position.z) && success;
        success = float.TryParse(cube[3], out rotation.x) && success;
        success = float.TryParse(cube[4], out rotation.y) && success;
        success = float.TryParse(cube[5], out rotation.z) && success;
        success = float.TryParse(cube[6], out color.r) && success;
        success = float.TryParse(cube[7], out color.g) && success;
        success = float.TryParse(cube[8], out color.b) && success;
        return success;
    }

    public void createInScene(GameObject prefab, Transform parent)
    {
        linkedCube = (GameObject)GameObject.Instantiate(prefab, parent);
        linkedCube.transform.position = position;
        linkedCube.transform.rotation = Quaternion.Euler(rotation);
        linkedCube.GetComponent<Renderer>().material.color = color;
    }

    public void LinkCube(GameObject cube)
    {
        linkedCube = cube;
        position = linkedCube.transform.position;
        rotation = linkedCube.transform.rotation.eulerAngles;
        color = linkedCube.GetComponent<Renderer>().material.color;
    }

    public void remove()
    {
        if (linkedCube)
            GameObject.Destroy(linkedCube);
    }
}
