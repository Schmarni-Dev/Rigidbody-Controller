using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Schmarni.input
{
    public class inputManager : MonoBehaviour
    {
        public string pName;
        [SerializeField]
        private bool editor = false;
        [SerializeField]
        private InputActionAsset asset;
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
        private void Awake()
        {
            // Application.targetFrameRate = 60;
            editor = false;
            #if UNITY_EDITOR
            editor = true;
            #endif
            Singleton = this;
            helper.InputEnable(asset);
        }

        public inputData getData()
        {
            inputData D = new inputData();

            #region inputMapping
            D.move = asset.FindAction("movement/move").ReadValue<Vector2>();
            D.jump = asset.FindAction("movement/jump").ReadValue<float>() == 1;
            D.look = asset.FindAction("camera/look").ReadValue<Vector2>();
            #endregion

            return D;
        }

        private void Update()
        {
            input[0] = getData();
        }
    }
    [Serializable]
    public class inputData
    {
        public Vector2 move;
        public Vector2 look;
        public bool jump;
    }
}
