using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaflPiece : MonoBehaviour
{
    private int BoardX, BoardY;
    
    //Warrior or King?
    public string Type;

    //1 or 2, White or Red
    [Range(1,2)] public int Team;


    public float Speed;
    private bool isMoving = false;
    private bool isSelected = false;

    public void initTaflPiece(int boardX, int boardY){
        BoardX = boardX;
        BoardY = boardY;
    }

    // Start is called before the first frame update
    void Start()
    {   
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //This function will translate the piece towards a specific location.
    public void MoveTo(Vector3 loc){
        StartCoroutine("c_MoveTo", loc);
    }

    IEnumerator c_MoveTo(Vector3 loc){
        while(Vector3.Distance(loc, transform.position) < 0.001){
            Vector3 newPosition = Vector3.MoveTowards(gameObject.transform.position, loc, Speed * Time.deltaTime);
            gameObject.transform.position = newPosition;

            yield return null;
        }
    }
}
