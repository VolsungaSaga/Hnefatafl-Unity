using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;

public class CameraRotate : MonoBehaviour
{

    private bool rotating = false;

    /*
    */
   [Tooltip( "The point around which the camera will rotate horizontally, if any.")]
    public Transform HorizontalPointOfRotation;
   [Tooltip( "The point around which the camera will rotate vertically, if any.")]

    public Transform VerticalPointOfRotation;

    [Tooltip("If this is true, the camera script will attempt to find the center coordinates of the board and set its axis to those coordinates. This should be set to true if you don't have a ready made board prefab available.")]
    public bool autoSetAxis = true;

    public float RotationSpeed = 10f;

    private Vector2 mouseDeltas;

    // Start is called before the first frame update
    void Start()
    {

        if(autoSetAxis){
            //We want to lock onto the center of the board.
            GameObject board = GameObject.Find("TaflBoard");

            if(!board){return;}

            int centerIndex = Mathf.FloorToInt(board.GetComponent<GameBoard>().Size / 2);


            //Get center cell of the board, set our axis.
            GameObject cell = board.GetComponent<GameBoard>().GetTaflCellAt(centerIndex, centerIndex);

            HorizontalPointOfRotation = cell.transform;
            VerticalPointOfRotation = cell.transform;

            //Set look at constraint
            ConstraintSource source = new ConstraintSource();
            source.sourceTransform = HorizontalPointOfRotation;
            source.weight = 1.0f;

            gameObject.GetComponent<LookAtConstraint>().AddSource(source);
            gameObject.GetComponent<LookAtConstraint>().constraintActive = true;
            
        }


    }

    void OnDrawGizmos(){
        if(HorizontalPointOfRotation != null){
            Gizmos.color = Color.red;
            Gizmos.DrawRay(HorizontalPointOfRotation.position, Vector3.up * 100);
        }

        if(VerticalPointOfRotation != null){
            Gizmos.color = Color.green;
            Gizmos.DrawRay(VerticalPointOfRotation.position,  (transform.rotation * Vector3.left) * 100);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Rotate(InputAction.CallbackContext context){
        //Debug.Log(context.ToString());

        if(context.phase == InputActionPhase.Started){
            mouseDeltas = context.ReadValue<Vector2>();
        }

        else if(context.phase == InputActionPhase.Performed){
            mouseDeltas = context.ReadValue<Vector2>();

            if(rotating){
                

                gameObject.transform.RotateAround(HorizontalPointOfRotation.position, Vector3.up, mouseDeltas.x);

                gameObject.transform.RotateAround(VerticalPointOfRotation.position, transform.rotation *  Vector3.left, mouseDeltas.y);


            }

        }



        

    }


    public void RotateToggle(InputAction.CallbackContext context){
        //Debug.Log(context.ToString());

        rotating = context.ReadValue<float>() > 0.0f ? true : false;
        

    }
}
