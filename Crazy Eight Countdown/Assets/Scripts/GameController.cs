using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameController : MonoBehaviour
{

	public static GameController gameController;

	void Awake ()
	{
		if (gameController == null)
		{
			DontDestroyOnLoad(gameObject);
			gameController = this;
		}
		else if (gameController != this)
		{
			Destroy(gameObject);
		}
	}

	void Save()
	{
		BinaryFormatter br = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/gameData.dat");

		GameData data = new GameData();
		// Save data to 'data' object here

		br.Serialize(file, data);
		file.Close();
	}

	void Load ()
	{
		if (File.Exists(Application.persistentDataPath + "/gameData.dat"))
		{
//			BinaryFormatter bf = new BinaryFormatter();
//			FileStream file = File.Open(Application.persistentDataPath + "/gameData.dat", FileMode.Open);
//			GameData data = (GameData)bf.Deserialize(file);
//			file.Close();

			// Load data from 'data' object here

		}
	}
}

[Serializable]
class GameData
{
	// Add saveable variables here
}