using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Schmarni.input;

public class manager : MonoBehaviour
{
    [SerializeField] private GameObject menuHolder;
    [SerializeField] private GameObject hudHolder;
    public static bool OpenMenu;
    private bool _oldOpen;
    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (inputManager.Singleton.getData().OpenMenu && !_oldOpen)
        {
            _oldOpen = true;

            if (!menuHolder.activeInHierarchy)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                menuHolder.SetActive(true);
                hudHolder.SetActive(false);
                OpenMenu = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                menuHolder.SetActive(false);
                hudHolder.SetActive(true);
                OpenMenu = false;
            }
        }
        
        if(!inputManager.Singleton.getData().OpenMenu)
        {
            _oldOpen = false;
        }
    }
}
