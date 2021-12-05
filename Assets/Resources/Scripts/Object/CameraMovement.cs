using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{

    private Vector2 currentMotionVector;
    public float Speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentMotionVector != null){
            var newVector = Vector3.MoveTowards(gameObject.transform.position, transform.position + new Vector3(currentMotionVector.x, 0.0f, currentMotionVector.y), Speed * Time.deltaTime );
            transform.position = newVector;
        }
    }

    public void Move(InputAction.CallbackContext context){
        //Debug.Log(context.action);
        Debug.Log(context.ToString());

        if(context.phase == InputActionPhase.Performed){
            currentMotionVector = context.ReadValue<Vector2>();
            Debug.Log(currentMotionVector);
        }

        else if(context.phase == InputActionPhase.Canceled){
            currentMotionVector = Vector2.zero;
        }

    }


}
