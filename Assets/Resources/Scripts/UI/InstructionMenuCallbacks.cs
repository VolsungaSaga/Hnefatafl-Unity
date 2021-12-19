using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionMenuCallbacks : MonoBehaviour
{

    public MenuManager manager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnBackButtonClick(){
        manager.SwitchTo(manager.StartMenuRootObj);
    }
}
