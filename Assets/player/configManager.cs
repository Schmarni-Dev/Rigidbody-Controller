using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;



namespace Schmarni.config
{
    [Serializable]
    public class inputManager : Schmarni.IcomponentBehaviour
    {
        private static inputManager _singleton;
        public static inputManager Singleton
        {
            get => _singleton;
            private set
            {
                if (_singleton == null)
                    _singleton = value;
                else if (_singleton != value)
                {
                    Debug.Log($"{nameof(inputManager)} instance already exists, destroying duplicate!");
                }
            }
        }
        [SerializeField]
        private configData config;
        [SerializeField]
        private static configData data = null;
        private static string path;
        public void start()
        {
            path = Application.persistentDataPath + (Application.isEditor ? "/Econfig.json" : "/config.json");
            // Application.targetFrameRate = 60;
            Singleton = this;
        }
        public void SaveData()
        {
            string json = JsonConvert.SerializeObject(inputManager.data);
            File.WriteAllText(path,json);
        }

        private configData loadData()
        {
            if (inputManager.data != null) return inputManager.data;
            // input.inputManager.print(configManager.data != null);
            configData data = new configData();

            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                data = JsonConvert.DeserializeObject<configData>(json);
            }
            else
            {
                inputManager.data = data;
                SaveData();
            }
            inputManager.data = data;
            return data;
        }

        public configData getData()
        {
            return loadData();
        }

        public void update()
        {
            config = getData();
        }
    }
    [Serializable]
    public class configData
    {
        public float mouseSens = 1;
        public float conSens = 1;
    }
}
