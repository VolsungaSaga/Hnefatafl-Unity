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

        

        //Third, check if the center is being targeted. You can't occupy the center!
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


            //If there's a neighbor, check one more cell in the same direction. We don't consider the King for captures (but we do for victory!)
            if(neighborObj && !neighborObj.GetComponent<GamePiece>().isKing()){
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

    // Is this location on a corner?
    public static bool IsCorner(GameBoard board, int boardX, int boardZ){
        return (boardX == 0 || boardX == board.Size - 1) && (boardZ == 0 || boardZ == board.Size - 1);
    }

    //Given a piece and its board, determine if there are any empty spots
    // in an orthogonal direction. If there are, the piece is not surrounded. If you couldn't 
    // find any, then it is surrounded. 
    public static bool IsSurroundedByEnemies(GamePiece piece, GameBoard board){
        bool currentEdgeCheck;
        foreach(int dir in Enum.GetValues(typeof(GameBoard.Direction))){
            GameObject neighbor = board.GetNeighbor((GameBoard.Direction) dir, piece.gameObject, out currentEdgeCheck);
            //If there is no neighbor, and current edge check is false, then we must not be surrounded!
            if((!neighbor && currentEdgeCheck == false)){
                return false;
            }
            //Also check if the neighbor is on your team. If any neighbor's on your own team, you're not surrounded.
            else if(neighbor && neighbor.GetComponent<GamePiece>().Team == piece.Team){
                return false;
            }
        }
        //If all orthogonal directions were occupied, then we are indeed surrounded.
        return true;

    }

    /*
    Returns either the team that is victorious or -1 if no team has won yet.
    */
    public static int CheckForVictoryState(GameObject board, GameManager manager){
        GameBoard boardData = board.GetComponent<GameBoard>();
        int kingX = boardData.whiteKing.GetComponent<GamePiece>().BoardX;
        int kingZ = boardData.whiteKing.GetComponent<GamePiece>().BoardZ;
        

        //White team, the one with a king.
        //Check if he's in a corner spot. If so, white has the victory!
        if (IsCorner(board.GetComponent<GameBoard>(), kingX, kingZ)){
            return 1; //White
        }

        //Red team, which must surround the king to win.
        if (IsSurroundedByEnemies(boardData.whiteKing.GetComponent<GamePiece>(), boardData)){
            return 2; //Red
        }
        


        return -1;
    }

}
