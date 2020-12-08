using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class DataSerializationUtility
{
    public static T Load<T>(string pathEnd)
    {
        string path = Application.persistentDataPath + pathEnd;

        if (File.Exists(path))
        {
            try
            {
                string data = File.ReadAllText(path);
                //      Debug.Log("LOADED "+data);
                return JsonUtility.FromJson<T>(data);
            }
            catch (Exception e)
            {
                Debug.Log(e);
                throw;
            }
         
        }
        else
        {


            string data = File.ReadAllText(path);
            //      Debug.Log("LOADED "+data);
            return JsonUtility.FromJson<T>(data);
        }
    }
}
