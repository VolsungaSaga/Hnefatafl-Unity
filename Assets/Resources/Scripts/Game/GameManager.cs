using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GameManager : MonoBehaviour
{
    public GameBoard _board;

    //Game State Variables
    private int  _playerTurn;




    void Awake(){
        _playerTurn = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        _board.e_boardMoveBegin.AddListener(onPieceMoveBegin);
        _board.e_boardMoveFinished.AddListener(onPieceMoveFinish);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getCurrentPlayerTurn(){
        return _playerTurn;
    }

    public void cyclePlayerTurn(){
        _playerTurn = (_playerTurn == 1 ? 2 : 1);
    }

    void onPlayerTurnBegin(){

    }

    void onPieceMoveBegin(GamePiece piece){
        //TODO: Block player input here.
    }

    void onPieceMoveFinish(GamePiece piece){

        List<GameObject> captures;
        
        bool captured = Ruleset.GetCaptures(piece.gameObject, _board.gameObject, out captures);

        foreach(GameObject pieceObj in captures){
            //pieceObj.GetComponent<GamePiece>().ChangeColor(Color.red);
            Destroy(pieceObj);
        }

        onPlayerTurnFinish();


        //TODO: Unblock player input here.
    }



    void onPlayerTurnFinish(){
        Debug.Log("Turn completed event invoked!");

        cyclePlayerTurn();

        Debug.Log($"Now it's player {getCurrentPlayerTurn()}'s turn ");
    }

}
