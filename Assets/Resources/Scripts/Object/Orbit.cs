using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{

    public Transform OrbitedBody;

    public float Speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        gameObject.transform.RotateAround(OrbitedBody.position, Vector3.up, Speed * Time.deltaTime);

        
    }
}
