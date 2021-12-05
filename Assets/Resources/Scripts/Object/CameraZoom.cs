using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class CameraZoom : MonoBehaviour
{

    public float ZoomSpeed = 1.0f;

    private float currentZoom = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        var newVector = Vector3.MoveTowards(transform.position, transform.position + transform.forward * currentZoom, ZoomSpeed * Time.deltaTime );
        transform.position = newVector;
    
    }


    public void Zoom(InputAction.CallbackContext context){
//Debug.Log(context.action);
        Debug.Log(context.ToString());

        if(context.phase == InputActionPhase.Performed){
            currentZoom = Mathf.Clamp(context.ReadValue<float>(), -1, 1);
            
            Debug.Log(currentZoom);
        }

        else if(context.phase == InputActionPhase.Canceled){
            currentZoom = 0f;
        }


    }

}
