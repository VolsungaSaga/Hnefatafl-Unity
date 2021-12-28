using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuCallbacks : MonoBehaviour
{

    public MenuManager manager;

    public SceneTransit transitionManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    public void onStartButtonClick(){
        
        transitionManager.GoToScene("Play");

    }

    public void onInstructionButtonClick(){

        manager.SwitchTo(manager.HelpMenuRootObj);

    }

    public void onQuitButtonClick(){
        Application.Quit(0);
    }


    
}
