using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
public class FileDataHandler
{

    private string filename = "";
    private string dataPath = "";

    public FileDataHandler(string filename, string dataPath)
    {
        this.filename = filename;
        this.dataPath = dataPath;
    }

    public GameData Load()
    {
        string fullpath = Path.Combine(dataPath, filename);
        GameData loadedData = null;
        Debug.Log("loading from" + fullpath);
        if (File.Exists(fullpath + "save.json"))
        {
            try
            {
                string dataToLoad = "";
                
                using (FileStream stream = new FileStream(fullpath + "save.json", FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch(Exception e)
            {
                Debug.Log("unable to load file! " + e);
            }
        }
        return loadedData;
    }
    public void Save(GameData data)
    {
        string fullpath = Path.Combine(dataPath, filename);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullpath));
            Debug.Log("saving to" + fullpath);
            string storingData = JsonUtility.ToJson(data, true);

            using (FileStream stream = new FileStream(fullpath + "save.json", FileMode.Create, FileAccess.ReadWrite))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(storingData);
                }
            }
        }
        catch(Exception e)
        {
            Debug.Log("unable to save file; " + e);
        }
    }
}
