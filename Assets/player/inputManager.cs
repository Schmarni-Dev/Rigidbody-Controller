using System;
using UnityEngine;
using UnityEngine.InputSystem;


using Schmarni.config;


namespace Schmarni.input
{
    public class inputManager : MonoBehaviour
    {
        public string pName;
        [SerializeField]
        private InputActionAsset asset;
        public config.inputManager configManager = null;
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
        private inputData[] input = new inputData[1];
        private void Start()
        {
            // Application.targetFrameRate = 60;
            Singleton = this;
            helper.InputEnable(asset);
            if (inputManager.Singleton.configManager != null) configManager = new config.inputManager();
            configManager.start();
        }

        public inputData getData()
        {
            inputData D = new inputData();

            #region inputMapping
            D.move = asset.FindAction("movement/move").ReadValue<Vector2>();
            D.jump = asset.FindAction("movement/jump").ReadValue<float>() == 1;
            D.look = (asset.FindAction("camera/lookM").ReadValue<Vector2>() * configManager.getData().mouseSens) + (asset.FindAction("camera/lookC").ReadValue<Vector2>() * (configManager.getData().conSens)*100);
            D.OpenMenu = asset.FindAction("etc/menuToggle").ReadValue<float>() == 1;
            #endregion

            return D;
        }

        private void Update()
        {
            input[0] = getData();
            configManager.update();
        }
    }
    [Serializable]
    public class inputData
    {
        public Vector2 move;
        public Vector2 look;
        public bool jump;
        public bool OpenMenu;
    }
}
