using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public GameObject StartMenuRootObj;
    public GameObject HelpMenuRootObj;

    private GameObject currentOpenMenuObj;



    // Start is called before the first frame update
    void Start()
    {
        currentOpenMenuObj = StartMenuRootObj;
        Open(currentOpenMenuObj);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchTo(GameObject rootMenuUIObject){
        Close(currentOpenMenuObj);
        Open(rootMenuUIObject);
        currentOpenMenuObj = rootMenuUIObject;
    }


    public void Open(GameObject rootMenuUIObject){
        Menu menu;
        bool exists = rootMenuUIObject.TryGetComponent<Menu>(out menu);

        if(!exists){
            Debug.LogError("MenuManager.Open: No Menu script detected.");
            return;
        }

        if(menu.Opened){
            return;
        }

        menu.Open();


    }

    public void Close(GameObject rootMenuUIObject){
        Menu menu;
        bool exists = rootMenuUIObject.TryGetComponent<Menu>(out menu);

        if(!exists){
            Debug.LogError("MenuManager.Close: No Menu script detected.");
            return;
        }

        if(!menu.Opened){
            return;
        }

        menu.Close();
    }
}
