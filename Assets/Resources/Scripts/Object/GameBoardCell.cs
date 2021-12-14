using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameBoardCell : MonoBehaviour
{

    public int BoardX, BoardZ;
    public GameObject Occupant{get; set;}


    public void init(int boardX, int boardZ){
        BoardX = boardX;
        BoardZ = boardZ;
    }

    // Start is called before the first frame update
    void Start()
    {
        // TMP_Text debugText = transform.Find("DebugText").gameObject.GetComponent<TMP_Text>();
        // if(debugText){
        //     debugText.text = $"{BoardX},{BoardZ}";
        // }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public GameObject GetGamePiece(){
        return Occupant;
    }
}
