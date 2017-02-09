using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Assets.Models.Dto;
using System;

public static class SaveManager
{

    public static void SaveData(string[] datas, string fileName, bool overrideFile)
    {
        string tPath = Application.persistentDataPath + "/" + fileName;
        if (File.Exists(tPath) && overrideFile)
        {
            File.Delete(tPath);
            File.WriteAllLines(tPath, datas);
            Debug.Log("Override File at " + tPath);
        }
        else
        {
            File.WriteAllLines(tPath, datas);
            Debug.Log("Saving File at " + tPath);
        }

    }

    public static string[] loadData(string fileName)
    {
        string tPath = Application.persistentDataPath + "/" + fileName;
        string[] datas;
        if (File.Exists(tPath))
        {
            datas = File.ReadAllLines(tPath);
            Debug.Log("Read File at " + tPath);
            return datas;
        }

        return null;
    }

    public static IEnumerable<CubeDto> LoadBinaryData(string fileName)
    {
        using (var file = new FileStream(Path.Combine(Application.persistentDataPath, fileName), FileMode.Open))
        {
            using (var reader = new BinaryReader(file))
            {
                while (file.Position != file.Length)
                {
                    yield return reader.ReadCube();
                }
            }
        }
    }

    public static void SaveData(CubeDto[] cubeDto, string fileSaveName)
    {
        using (var file = new FileStream(Path.Combine(Application.persistentDataPath, fileSaveName), FileMode.Create))
        {
            using (var writer = new BinaryWriter(file))
            {
                foreach (var cube in cubeDto)
                {
                    writer.Write(cube);
                }
            }
        }
    }

    public static IEnumerable<CubeDto> LoadBinaryDataWithMessagePack(string fileName)
    {
        using (var file = new FileStream(Path.Combine(Application.persistentDataPath, fileName), FileMode.Open))
        {
            var serializer = MsgPack.Serialization.MessagePackSerializer.Create<CubeDto>();

            while (file.Position != file.Length)
            {
                yield return serializer.Unpack(file);
            }
        }
    }

    public static void SaveDataWithMessagePack(CubeDto[] cubeDtos, string fileSaveName)
    {
        using (var file = new FileStream(Path.Combine(Application.persistentDataPath, fileSaveName), FileMode.Create))
        {
            var serializer = MsgPack.Serialization.MessagePackSerializer.Create<CubeDto>();
            foreach (var dto in cubeDtos)
            {
                serializer.Pack(file, dto);
            }
        }
    }
}
