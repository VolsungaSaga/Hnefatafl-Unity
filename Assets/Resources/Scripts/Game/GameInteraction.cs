using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class GameInteraction : MonoBehaviour
{
    GameObject selectedPiece;


    public GameObject gameBoard;

    Vector2 mousePosition;





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Select(InputAction.CallbackContext context){

        //Debug.Log(context.ToString());

        if(context.phase == InputActionPhase.Started && context.ReadValue<float>() > 0.0f){
            //First, we get the raycast from the camera.
            Camera myCam = gameObject.GetComponent<Camera>();
            Ray selectRay = myCam.ScreenPointToRay(new Vector3(mousePosition.x, mousePosition.y, 0));
            Debug.DrawRay(selectRay.origin, selectRay.direction * 10, Color.blue, 5);

            RaycastHit hitInfo;

            //Mask depending on whether we already selected a piece and are now selecting an empty cell, or 
            // we're selecting a piece.
            if(selectedPiece == null){
                int layerMask = 1 << LayerMask.NameToLayer("GamePieces");
                bool hit = Physics.Raycast(selectRay, out hitInfo, 200, layerMask);

                if(hit){

                    SelectGamePiece(hitInfo.collider.gameObject);

                }

            }

            else{
                Debug.Log($"Selected Piece is : {selectedPiece.ToString()}");
                int layerMask = (1 << LayerMask.NameToLayer("GamePieces")) | (1 << LayerMask.NameToLayer("GameBoardCells"));
                bool hit = Physics.Raycast(selectRay, out hitInfo, 200, layerMask);

                if(hit){
                    GameObject hitObject = hitInfo.collider.gameObject;
                    Debug.Log($"The hit object is : {hitObject.ToString()}");
                    if(hitObject.layer == LayerMask.NameToLayer("GamePieces")){
                        //If we clicked another piece while having a piece selected, deselect the previous piece and select this one.
                        ClearSelection();
                        SelectGamePiece(hitObject);
                    }
                    else if(hitObject.layer == LayerMask.NameToLayer("GameBoardCells")){
                        //If we click on a cell, move the piece to it. Checking for emptiness comes later!
                        OrderSelectedGamePieceTo(hitObject, true);
                    }

                }

            } 


        }

    }


    void SelectGamePiece(GameObject obj){
        selectedPiece = obj;
        selectedPiece.GetComponent<GamePiece>().isSelected = true;
        selectedPiece.GetComponent<GamePiece>().ChangeColor(Color.blue);
    }

    void ClearSelection(){
        selectedPiece.GetComponent<GamePiece>().isSelected = false;
        selectedPiece.GetComponent<GamePiece>().ChangeColor(Color.white);
        selectedPiece = null;
    }

    /*
    Dispatches an order to the game board to move that piece to the targeted cell, clearing the current selection if 
    flagged to do so.
    */
    void OrderSelectedGamePieceTo(GameObject boardCell, bool clearSelection){
        gameBoard.GetComponent<GameBoard>().MoveGamePieceToCell(selectedPiece, boardCell);

        if(clearSelection){
            ClearSelection();
        }
    }


    public void MouseMove(InputAction.CallbackContext context){
        //Debug.Log(context.ToString());

        mousePosition = context.ReadValue<Vector2>();
    }




    



}
