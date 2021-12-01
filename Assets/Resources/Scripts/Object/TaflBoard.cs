using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaflBoard : MonoBehaviour
{
    public int Size = 7;

    private GameObject[,] Cells;
    private float cellWidth = 1f, cellHeight = 0.5f, cellDepth = 1f;
    public GameObject TaflBoardModel;

    // Start is called before the first frame update
    void Start()
    {

        if(!TaflBoardModel){

            for(int row = 0; row < Size; row++){

                for(int col = 0; col < Size; col++){



                }


            }

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    TaflPiece GetTaflPiece(int boardX, int boardY){
        return null;
    }

    public GameObject GenerateTaflBoard(){
        return null;
    }
}
