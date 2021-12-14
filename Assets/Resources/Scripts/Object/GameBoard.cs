using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GameBoard : MonoBehaviour
{
    public int Size = 9;
    public GameObject taflCellPrefab;
    public GameObject redWarriorPrefab;
    public GameObject redKingPrefab;
    public GameObject whiteWarriorPrefab;
    public GameObject whiteKingPrefab;

    private GameObject[,] Cells;

    public GameManager manager;


    public enum Direction{
        PLUSZ, 
        MINUSZ, 
        MINUSX, 
        PLUSX 
    }


    public UnityEvent<GamePiece> e_boardMoveBegin;
    public UnityEvent<GamePiece> e_boardMoveFinished;


    void Awake(){
        Cells = new GameObject[Size,Size];
        GenerateTaflBoard(Size);

    }


    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject cell in Cells){
            GameObject pieceObj = cell.GetComponent<GameBoardCell>().GetGamePiece();
            if(pieceObj){
                GamePiece piece = pieceObj.GetComponent<GamePiece>();
                piece.e_MoveFinished.AddListener(OnPieceMoveFinished);
                piece.e_MoveBegin.AddListener(OnPieceMoveBegin);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmos(){
        Gizmos.color = Color.magenta;
        Gizmos.DrawCube(transform.position, Vector3.one * 0.5f);
    }


    /*These two event callbacks collectively form the middle layer of our callback chain.
        For now, they won't do much but immediately invoke events of their own, but they could be used to 
        regulate when it happens -- a variable keeping track of how many pieces have moved in a given turn, for instance.
    */
    public void OnPieceMoveBegin(GamePiece piece){
        Debug.Log("A piece is starting its move!");
        e_boardMoveBegin.Invoke(piece);
    }

    public void OnPieceMoveFinished(GamePiece piece){
        Debug.Log("A piece finished moving!");
        e_boardMoveFinished.Invoke(piece);
    }

    public Vector3 GetWorldCoordinates(int boardX, int boardZ){
        return Cells[boardX, boardZ].transform.position;
        //return transform.position + GetLocalCoordinates(boardX, boardZ);
    }

    public Vector3 GetLocalCoordinates(int boardX, int boardZ){
        float poseX = (boardX * taflCellPrefab.transform.localScale.x);// + (taflCellPrefab.transform.localScale.x / 2);
        float poseZ = (boardZ * taflCellPrefab.transform.localScale.z);// + (taflCellPrefab.transform.localScale.z / 2);

        return new Vector3(poseX, 0.0f, poseZ);
    }

    public GameObject GetTaflCellAt(int boardX, int boardZ){
        if(boardX < 0 || boardZ < 0 || boardX > Size - 1 || boardZ > Size - 1){
            return null;
        }
        return Cells[boardX, boardZ];
    }



    //Returns null if there is no neighbor in the given direction. Sets edge to true if it was because there is no cell in the direction, false otherwise.
    public GameObject GetNeighbor(Direction dir, GameObject gamePiece, out bool edge){
        int pieceX = gamePiece.GetComponent<GamePiece>().BoardX;
        int pieceZ = gamePiece.GetComponent<GamePiece>().BoardZ;
        edge = false;
        GameObject neighborCell = null;
        switch (dir){
            case Direction.PLUSZ:
                neighborCell = GetTaflCellAt(pieceX, pieceZ + 1);
                break;
            case Direction.MINUSZ:
                neighborCell = GetTaflCellAt(pieceX, pieceZ - 1);
                break;
            case Direction.PLUSX:
                neighborCell = GetTaflCellAt(pieceX + 1, pieceZ);
                break;
            case Direction.MINUSX:
                neighborCell = GetTaflCellAt(pieceX - 1, pieceZ);
                break;
        }

        if(!neighborCell) { edge = true; return null;}

        GameObject neighbor = neighborCell.GetComponent<GameBoardCell>().Occupant;
        return neighbor;


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
                taflCell.GetComponent<GameBoardCell>().init(x, z);
                Cells[x,z] = taflCell;
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
                CreateTaflPieceAtCell(whiteKingPrefab, Mathf.FloorToInt(Size / 2), Mathf.FloorToInt(Size / 2));
                CreateTaflPieceAtCell(whiteWarriorPrefab, 2,4);
                CreateTaflPieceAtCell(whiteWarriorPrefab, 3,4);
                CreateTaflPieceAtCell(whiteWarriorPrefab, 4,4);
                CreateTaflPieceAtCell(whiteWarriorPrefab, 4,3);
                CreateTaflPieceAtCell(whiteWarriorPrefab, 4,2);
                CreateTaflPieceAtCell(whiteWarriorPrefab, 3,2);
                CreateTaflPieceAtCell(whiteWarriorPrefab, 2,2);
                CreateTaflPieceAtCell(whiteWarriorPrefab, 2,3);


                //Red Team, on the edges

                //North side
                CreateTaflPieceAtCell(redWarriorPrefab, 2,6);
                CreateTaflPieceAtCell(redWarriorPrefab, 3,6);
                CreateTaflPieceAtCell(redWarriorPrefab, 4,6);
                CreateTaflPieceAtCell(redWarriorPrefab, 3,5);

                //East side
                CreateTaflPieceAtCell(redWarriorPrefab, 5,3);
                CreateTaflPieceAtCell(redWarriorPrefab, 6,3);
                CreateTaflPieceAtCell(redWarriorPrefab, 6,2);
                CreateTaflPieceAtCell(redWarriorPrefab, 6,4);

                //West side
                CreateTaflPieceAtCell(redWarriorPrefab, 0,2);
                CreateTaflPieceAtCell(redWarriorPrefab, 0,3);
                CreateTaflPieceAtCell(redWarriorPrefab, 1,3);
                CreateTaflPieceAtCell(redWarriorPrefab, 0,4);

                //South side
                CreateTaflPieceAtCell(redWarriorPrefab, 2,0);
                CreateTaflPieceAtCell(redWarriorPrefab, 3,0);
                CreateTaflPieceAtCell(redWarriorPrefab, 4,0);
                CreateTaflPieceAtCell(redWarriorPrefab, 3,1);

                break;

            case 9:
                //White team
                CreateTaflPieceAtCell(whiteKingPrefab, Mathf.FloorToInt(Size / 2), Mathf.FloorToInt(Size / 2));
                CreateTaflPieceAtCell(whiteWarriorPrefab, 3, 4);
                CreateTaflPieceAtCell(whiteWarriorPrefab, 2, 4);

                CreateTaflPieceAtCell(whiteWarriorPrefab, 4, 5);
                CreateTaflPieceAtCell(whiteWarriorPrefab, 4, 6);
                
                CreateTaflPieceAtCell(whiteWarriorPrefab, 5, 4);
                CreateTaflPieceAtCell(whiteWarriorPrefab, 6, 4);

                CreateTaflPieceAtCell(whiteWarriorPrefab, 4, 3);
                CreateTaflPieceAtCell(whiteWarriorPrefab, 4, 2);
                //Red team
                // West side
                CreateTaflPieceAtCell(redWarriorPrefab, 1, 4);
                CreateTaflPieceAtCell(redWarriorPrefab, 0, 4);
                CreateTaflPieceAtCell(redWarriorPrefab, 0, 5);
                CreateTaflPieceAtCell(redWarriorPrefab, 0, 3);

                //North side
                CreateTaflPieceAtCell(redWarriorPrefab, 4, 7);
                CreateTaflPieceAtCell(redWarriorPrefab, 4, 8);
                CreateTaflPieceAtCell(redWarriorPrefab, 3, 8);
                CreateTaflPieceAtCell(redWarriorPrefab, 5, 8);

                //East side
                CreateTaflPieceAtCell(redWarriorPrefab, 7, 4);
                CreateTaflPieceAtCell(redWarriorPrefab, 8, 4);
                CreateTaflPieceAtCell(redWarriorPrefab, 8, 5);
                CreateTaflPieceAtCell(redWarriorPrefab, 8, 3);

                //South side
                CreateTaflPieceAtCell(redWarriorPrefab, 4, 1);
                CreateTaflPieceAtCell(redWarriorPrefab, 4, 0);
                CreateTaflPieceAtCell(redWarriorPrefab, 3, 0);
                CreateTaflPieceAtCell(redWarriorPrefab, 5, 0);


                break;
            default: 
                UnityEngine.Debug.LogError("Given board size does not match any cases for placing tafl pieces!");
                break;
        }

    }

    /*
        Given a game piece *prefab*, instantiates it and places it on the cell at a given set of board coordinates.
    */
    public void CreateTaflPieceAtCell(GameObject piecePrefab, int boardX, int boardZ){
        var cell = Cells[boardX, boardZ];
        var taflPiece = Instantiate(piecePrefab, cell.transform.position, Quaternion.identity); //Create the piece

        taflPiece.transform.parent = cell.transform; //Set transform parent.
        taflPiece.GetComponent<GamePiece>().init(boardX, boardZ); //Initialize data that the tafl piece needs to know.
        cell.GetComponent<GameBoardCell>().Occupant = taflPiece;
    }


    /*
        Given a reference to a game piece and the game board cell, translate the game piece to the corresponding position in the world frame.
        This function handles both starting the animation for the game piece and ensuring that all the data's properly updated for the cell, the board, and the game piece.
    */
    public void MoveGamePieceToCell(GameObject gamePiece, GameObject targetBoardCell){
        int cellBoardX = targetBoardCell.GetComponent<GameBoardCell>().BoardX;
        int cellBoardZ = targetBoardCell.GetComponent<GameBoardCell>().BoardZ;

        //First, decouple the game piece from its existing parent, if any.
        gamePiece.transform.parent.gameObject.GetComponent<GameBoardCell>().Occupant = null;
        gamePiece.transform.parent = null;

        //Set the game piece's relevant data to appropriate values for its new position.
        gamePiece.GetComponent<GamePiece>().BoardX = cellBoardX;
        gamePiece.GetComponent<GamePiece>().BoardZ = cellBoardZ;

        //Begin the animation for the game piece!
        Vector3 targetPose = GetWorldCoordinates(cellBoardX, cellBoardZ);
        gamePiece.GetComponent<GamePiece>().MoveTo(targetPose);

        //Set the new parent.
        gamePiece.transform.parent = targetBoardCell.transform;
        targetBoardCell.GetComponent<GameBoardCell>().Occupant = gamePiece;
    }

    /*
    Gets a straight slice of the board, assuming that start and end are orthogonal to each other.
    The result when start and end are NOT orthogonal to each other is undefined.
    */
    public bool GetBoardSlice(GameObject start, GameObject end, out List<GameObject> slice){

        slice = new List<GameObject>();    
            
        int startX = start.GetComponent<GameBoardCell>().BoardX;
        int startZ = start.GetComponent<GameBoardCell>().BoardZ;

        int endX = end.GetComponent<GameBoardCell>().BoardX;
        int endZ = end.GetComponent<GameBoardCell>().BoardZ;


        if(endX - startX > 0){
            for(int x = startX; x <= endX; x ++){
                slice.Add(GetTaflCellAt(x, startZ));
            }
            return true;
        }

        else if(endX - startX < 0){
            for(int x = startX; x >= endX; x--){
                slice.Add(GetTaflCellAt(x, startZ));
            }
            return true;
        }

        else if(endZ - startZ > 0){
            for(int z = startZ; z <= endZ; z ++){
                slice.Add(GetTaflCellAt(startX, z));
            }
            return true;
        }

        else if(endZ - startZ < 0){
            for(int z = startZ; z >= endZ; z--){
                slice.Add(GetTaflCellAt(startX, z));
            }
            return true;
        }

        return false;


    }




}





