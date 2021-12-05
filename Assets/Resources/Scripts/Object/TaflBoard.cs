using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaflBoard : MonoBehaviour
{
    public int Size = 7;
    public GameObject taflCellPrefab;
    public GameObject redWarriorPrefab;
    public GameObject redKingPrefab;
    public GameObject whiteWarriorPrefab;
    public GameObject whiteKingPrefab;

    private GameObject[,] Cells;


    void Awake(){
        Cells = new GameObject[Size,Size];
        Destroy(gameObject.GetComponent<MeshRenderer>()); //The flagpole's just for our reference, so we destroy it here.
    }


    // Start is called before the first frame update
    void Start()
    {
        GenerateTaflBoard(Size);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    GameObject GetCell(int boardX, int boardZ){
        return Cells[boardZ,boardX];
    }

    public Vector3 GetWorldCoordinates(int boardX, int boardZ){
        
        float poseX = (boardX * taflCellPrefab.transform.localScale.x) + (taflCellPrefab.transform.localScale.x / 2);
        float poseZ = (boardZ * taflCellPrefab.transform.localScale.z) + (taflCellPrefab.transform.localScale.z / 2);

        return transform.position + new Vector3(poseX, 0.0f, poseZ);
    }

    public void GenerateTaflBoard(int size){
        
        for(int z = 0; z < size; z++){
            for(int x = 0; x < size; x++){

                //Cubes, so the true x,y,z lengths are simply factors of the scale.
                float xLength = 1 * gameObject.transform.localScale.x;
                float yLength = 1 * gameObject.transform.localScale.y;
                float zLength = 1 * gameObject.transform.localScale.z;

                float xPos = x + (xLength / 2); 
                float yPos = 0;
                float zPos = z + (zLength / 2);
                //A board cell is a child of the board object itself.
                var taflCell = Instantiate(taflCellPrefab, new Vector3(xPos, yPos, zPos ), Quaternion.identity);
                taflCell.transform.parent = gameObject.transform;

                Cells[z,x] = taflCell;
            }


        }

        PlaceTaflPieces();



    }

    private void PlaceTaflPieces(){
        //We'll need to put all the pieces down manually.

        switch (Size)
        {
            case 7:
                //White Team - In the center.
                AddTaflPieceToCell(whiteKingPrefab, Mathf.FloorToInt(Size / 2), Mathf.FloorToInt(Size / 2));
                AddTaflPieceToCell(whiteWarriorPrefab, 2,4);
                AddTaflPieceToCell(whiteWarriorPrefab, 3,4);
                AddTaflPieceToCell(whiteWarriorPrefab, 4,4);
                AddTaflPieceToCell(whiteWarriorPrefab, 4,3);
                AddTaflPieceToCell(whiteWarriorPrefab, 4,2);
                AddTaflPieceToCell(whiteWarriorPrefab, 3,2);
                AddTaflPieceToCell(whiteWarriorPrefab, 2,2);
                AddTaflPieceToCell(whiteWarriorPrefab, 2,3);


                //Red Team, on the edges

                //North side
                AddTaflPieceToCell(redWarriorPrefab, 2,6);
                AddTaflPieceToCell(redWarriorPrefab, 3,6);
                AddTaflPieceToCell(redWarriorPrefab, 4,6);
                AddTaflPieceToCell(redWarriorPrefab, 3,5);

                //East side
                AddTaflPieceToCell(redWarriorPrefab, 5,3);
                AddTaflPieceToCell(redWarriorPrefab, 6,3);
                AddTaflPieceToCell(redWarriorPrefab, 6,2);
                AddTaflPieceToCell(redWarriorPrefab, 6,4);

                //West side
                AddTaflPieceToCell(redWarriorPrefab, 0,2);
                AddTaflPieceToCell(redWarriorPrefab, 0,3);
                AddTaflPieceToCell(redWarriorPrefab, 1,3);
                AddTaflPieceToCell(redWarriorPrefab, 0,4);

                //South side
                AddTaflPieceToCell(redWarriorPrefab, 2,0);
                AddTaflPieceToCell(redWarriorPrefab, 3,0);
                AddTaflPieceToCell(redWarriorPrefab, 4,0);
                AddTaflPieceToCell(redWarriorPrefab, 3,1);

                break;
            default: 
                UnityEngine.Debug.LogError("Given board size does not match any cases for placing tafl pieces!");
                break;
        }

    }

    public void AddTaflPieceToCell(GameObject piecePrefab, int boardX, int boardZ){
        var cell = Cells[boardX, boardZ];
        var taflPiece = Instantiate(piecePrefab, cell.transform.position, Quaternion.identity); //Create the piece

        taflPiece.transform.parent = cell.transform; //Set transform parent.
        taflPiece.GetComponent<TaflPiece>().initTaflPiece(boardX, boardZ); //Initialize data that the tafl piece needs to know.
    }


}
