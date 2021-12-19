using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayTurn : MonoBehaviour
{
    [Tooltip("The game manager holding the data required.")]
    public GameManager gameManager;

    [Tooltip("The text upon which to spit out our data.")]
    public Text myText;

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        int currentTurn = gameManager.getCurrentPlayerTurn();

        myText.text = $"{gameManager.Teams[currentTurn]} Team's Turn";

        
    }
}
