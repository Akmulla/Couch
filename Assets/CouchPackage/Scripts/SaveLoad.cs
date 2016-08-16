using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoad : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        if (!File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
            Data data = new Data();
            data.bestScore = 0;
            bf.Serialize(file, data);
            file.Close();
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    public static void Save(int newBestScore)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        Data data = new Data();
        data.bestScore = newBestScore;
        bf.Serialize(file, data);
        file.Close();
    }
    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            Data data = (Data)bf.Deserialize(file);
            file.Close();
            Score.scoreComponent.GetSetBestScore = data.bestScore;
        }
    }
    
}
[Serializable]
class Data
{
    public int bestScore;
}
