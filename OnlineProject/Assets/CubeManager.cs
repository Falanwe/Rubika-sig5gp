using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Assets.Models.Dto;

public class CubeManager : MonoBehaviour {
    public string FileSaveName = "SavedData";
    public string BinaryFileSaveName = "BinarySavedData";
    public string MessagePackFileSaveName = "MessagePackSavedData";

    public GameObject cubePrefab;
    public int count = 100;
    public bool createOnAwake = false;
    public bool rndPosition = false;
    List<Cube> cubes;
    GameObject selectedCube = null;


	void Awake () {
        cubes = new List<Cube>();
        if(createOnAwake)
        {
            for (int i = 0; i < count; i++)
            {
                Cube c = new Cube();
                if (rndPosition)
                {
                    c.position = new Vector3(UnityEngine.Random.Range(-20, 20), UnityEngine.Random.Range(-15, 40), UnityEngine.Random.Range(-10, 50));
                    c.rotation = new Vector3(UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360));
                }
                c.createInScene(cubePrefab, this.transform);
                cubes.Add(c);
            }
            Debug.Log(cubes.Count + " cubes Created");
        }
        else
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Cubes");
            foreach (var obj in objects)
            {
                Cube c = new Cube();
                c.LinkCube(obj);
                cubes.Add(c);
            }
            Debug.Log(cubes.Count + " cubes linked");
        }
	}
	
	void Update () {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            Save();
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            Load();
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            SaveBinary();
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            LoadBinary();
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SaveBinaryWithMessagePack();
        }
        if (Input.GetKeyDown(KeyCode.F6))
        {
            LoadBinaryWithMessagePack();
        }
    }

    private void SaveBinary()
    {
        Debug.Log("Save Data in binary format");
        SaveManager.SaveData(GameObject.FindGameObjectsWithTag("Cubes").Select(cube => new CubeDto { Position = cube.transform.position, Rotation = cube.transform.rotation.eulerAngles, Color = cube.GetComponent<Renderer>().material.color }).ToArray(), BinaryFileSaveName);
    }

    private void SaveBinaryWithMessagePack()
    {
        Debug.Log("Save Data in binary format");
        SaveManager.SaveDataWithMessagePack(GameObject.FindGameObjectsWithTag("Cubes").Select(cube => new CubeDto { Position = cube.transform.position, Rotation = cube.transform.rotation.eulerAngles, Color = cube.GetComponent<Renderer>().material.color }).ToArray(), MessagePackFileSaveName);
    }

    private void LoadBinary()
    {
        clearList();
        foreach(var cubeDto in SaveManager.LoadBinaryData(BinaryFileSaveName))
        {
            var cube = new Cube();
            cube.position = cubeDto.Position;
            cube.rotation = cubeDto.Rotation;
            cube.color = cubeDto.Color;

            cube.createInScene(cubePrefab, this.transform);
            cubes.Add(cube);
        }
    }

    private void LoadBinaryWithMessagePack()
    {
        clearList();
        foreach (var cubeDto in SaveManager.LoadBinaryDataWithMessagePack(MessagePackFileSaveName))
        {
            var cube = new Cube();
            cube.position = cubeDto.Position;
            cube.rotation = cubeDto.Rotation;
            cube.color = cubeDto.Color;

            cube.createInScene(cubePrefab, this.transform);
            cubes.Add(cube);
        }
    }

    private void Load()
    {
        Debug.Log("Load Data");
        string[] datas = SaveManager.loadData(FileSaveName);
        if (datas != null)
        {
            clearList();
            foreach (var d in datas)
            {
                Cube c = new Cube();
                c.load(d);
                c.createInScene(cubePrefab, this.transform);
                cubes.Add(c);
            }
            Debug.Log("Done !");
        }
        else
        {
            Debug.Log("Load Failed");
        }
    }

    private void Save()
    {
        Debug.Log("Save Data");
        List<string> str = new List<string>();
        foreach (var c in cubes)
        {
            str.Add(c.serialize());
        }
        SaveManager.SaveData(str.ToArray(), FileSaveName, true);
    }

    void clearList()
    {
        foreach (var c in cubes)
        {
            c.remove();
        }
        cubes.Clear();
    }

}
