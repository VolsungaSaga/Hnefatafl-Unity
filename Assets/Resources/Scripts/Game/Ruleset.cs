using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Ruleset
{

    public static bool CheckIfTeamMatch(int team, GameObject piece){
        return team == piece.GetComponent<GamePiece>().Team;

    }

    /*
    The rules of piece movement in Hnefatafl, according to Hurstwic, are:
    1. The move must be orthogonal.
    2. The move must not intersect with other pieces.
    3. The move cannot pass through the center spot or end there.
    
    */
    public static bool CheckMoveLegality(GameObject piece, GameObject board, int targetBoardX, int targetBoardZ){
        int pieceX = piece.GetComponent<GamePiece>().BoardX;
        int pieceZ = piece.GetComponent<GamePiece>().BoardZ;

        string pieceType = piece.GetComponent<GamePiece>().Type;

        Debug.Log($"Checking Move ...\nPiece at {pieceX}, {pieceZ} \nTargeted cell at : {targetBoardX}, {targetBoardZ}");

        GameBoard boardData = board.GetComponent<GameBoard>();

        //First, orthogonality.
        if(!(pieceX == targetBoardX ^ pieceZ == targetBoardZ)){
            Debug.Log("The move is not orthogonal!");
            return false;
        }

        //Second, ask if the path taken intersects with another game piece.
        GameObject currentCell = boardData.GetTaflCellAt(pieceX, pieceZ);
        GameObject targetCell = boardData.GetTaflCellAt(targetBoardX, targetBoardZ);

        List<GameObject> slice;
        bool validSlice = boardData.GetBoardSlice(currentCell, targetCell, out slice);

        foreach(GameObject boardCell in slice){
            if(boardCell.GetComponent<GameBoardCell>().GetGamePiece() && boardCell != currentCell){
                Debug.Log($"There's a tafl piece in the way at {boardCell.GetComponent<GameBoardCell>().BoardX}, {boardCell.GetComponent<GameBoardCell>().BoardZ}");
                return false;
            }
        }

        

        //Third, check if the center is being targeted. You can't go to the center!
        if(pieceType != "King" && (targetBoardX == boardData.Size / 2) && (targetBoardZ == boardData.Size / 2)){
            return false;
        }


        return true;
    }


    /*
    This function is meant to be called only after a move by the given piece is done. 
    It iterates over each direction, travelling one cell in that direction and seeing if there's another piece there.
    |*|*|*|
    |^|R|W| ---> R is captured.
    |W|*|*|

    |*|*|*|*|*|
    |W|R|^|R|W|  ---> BOTH R's are captured.
    |*|*|W|*|*|

    From Hurstwic - "A piece is captured and removed from the board when an opponent is able to place two of his pieces on two opposite sides of the piece under attack. 
    However, if a player moves his piece between two pieces of his opponent, the piece is safe and is not captured and removed."
    
    */
    public static bool GetCaptures(GameObject piece, GameObject board, out List<GameObject> capturedPieces){
        GamePiece pieceData = piece.GetComponent<GamePiece>();
        GameBoard boardData = board.GetComponent<GameBoard>();
        capturedPieces = new List<GameObject>();

        /*
        Get the neighbor from each orthogonal direction.
        */
        foreach(int i in Enum.GetValues(typeof(GameBoard.Direction))){
            bool edgeOrigin;
            GameObject neighborObj = boardData.GetNeighbor((GameBoard.Direction)i, piece, out edgeOrigin);


            //If there's a neighbor, check one more cell in the same direction.
            if(neighborObj){
                //The neighbor's attributes
                int neighborTeam = neighborObj.GetComponent<GamePiece>().Team;
                string neighborType = neighborObj.GetComponent<GamePiece>().Type;
                //Get the neighbor of that neighbor!
                bool edgeNeighbor;
                GameObject neighborOfNeighborObj = boardData.GetNeighbor((GameBoard.Direction)i, neighborObj, out edgeNeighbor);

                if(neighborOfNeighborObj){
                    int neighborOfNeighborTeam = neighborOfNeighborObj.GetComponent<GamePiece>().Team;
                    string neighborOfNeighborType = neighborOfNeighborObj.GetComponent<GamePiece>().Type;

                    //Now that we've gotten both the neighbor and the neighbor's neighbor, we want to make sure that the neighbor of the neighbor is on our team
                    // and the neighbor is of the opposing team.

                    int originTeam = piece.GetComponent<GamePiece>().Team;
                    if(originTeam == neighborOfNeighborTeam && originTeam != neighborTeam){
                        //That's a capture!
                        capturedPieces.Add(neighborObj);
                    }
                }
            }
        }

        //Return if we actually have any pieces.
        return capturedPieces.Count > 0;
    }



    public static int CheckForVictoryState(GameObject board){
        GameBoard boardData = board.GetComponent<GameBoard>();


        return 1;
    }

}
