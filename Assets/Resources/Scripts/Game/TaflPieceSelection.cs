using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TaflPieceSelection : MonoBehaviour
{
    GameObject selectedPiece;


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
                int layerMask = 1 << 6;
                bool hit = Physics.Raycast(selectRay, out hitInfo, 200, layerMask);

                if(hit){

                    SelectGamePiece(hitInfo.collider.gameObject);

                }

            }

            else{
                //Reset everything
                ResetSelection();

            } 


        }

    }


    void SelectGamePiece(GameObject obj){
        selectedPiece = obj;
        selectedPiece.GetComponent<TaflPiece>().isSelected = true;
        selectedPiece.GetComponent<TaflPiece>().ChangeColor(Color.blue);
    }

    void ResetSelection(){
        selectedPiece.GetComponent<TaflPiece>().isSelected = false;
        selectedPiece.GetComponent<TaflPiece>().ChangeColor(Color.white);
        selectedPiece = null;
    }


    public void MouseMove(InputAction.CallbackContext context){
        Debug.Log(context.ToString());

        mousePosition = context.ReadValue<Vector2>();
    }




    



}
