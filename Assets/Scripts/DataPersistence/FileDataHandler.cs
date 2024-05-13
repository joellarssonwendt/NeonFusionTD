using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }
    public GameData Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                // ladda serialized data fr�n filen
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                // deserialize datan fr�n Json tillbaka till C# objekt.
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error h�nde n�r vi f�rs�kte ladda upp data fr�n filen: " + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }
    public void Save(GameData data)
    {
        //kombinera dessa med Combine s� att de blir r�tt path oavsett va de �r f�r operativ system
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            //skapa en plats f�r filen om den inte redan finns
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // k�r filen till en l�sbar fil via Json
            string dataToStore = JsonUtility.ToJson(data, true);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }

        }
        catch (Exception e)
        {
            Debug.LogError("Error h�nde n�r vi f�rs�kte spara data till filen: " + fullPath + "\n" + e);
        }
    }
}
