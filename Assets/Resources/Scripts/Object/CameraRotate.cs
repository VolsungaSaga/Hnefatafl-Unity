using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRotate : MonoBehaviour
{

    private bool rotating = false;
    private Vector3 AxisOfRotation;

    private Vector3 W_CenterOfBoard;


    // Start is called before the first frame update
    void Start()
    {
        //We want to lock onto the center of the board, but we don't necessarily have the cells initialized yet.
        // So, we calculate where the center's going to be by using the Size variable, which we know exists already.
        GameObject board = GameObject.Find("TaflBoard");

        if(!board){return;}

        int centerIndex = Mathf.FloorToInt(board.GetComponent<TaflBoard>().Size / 2);
        W_CenterOfBoard = board.GetComponent<TaflBoard>().GetWorldCoordinates(centerIndex, centerIndex);


        //Debug

    }

    void OnDrawGizmos(){
        Gizmos.DrawCube(W_CenterOfBoard, Vector3.one);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Rotate(InputAction.CallbackContext context){
        //Debug.Log(context.ToString());

        if(rotating){

        }

    }

    public void RotateToggle(InputAction.CallbackContext context){
        //Debug.Log(context.ToString());

        rotating = context.ReadValue<float>() > 0.0f ? true : false;
        

    }
}
