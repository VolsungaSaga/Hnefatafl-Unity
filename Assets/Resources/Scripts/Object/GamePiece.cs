using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class GamePiece : MonoBehaviour
{
    public int BoardX, BoardZ;
    
    //Warrior or King?
    public string Type;

    //1 or 2, White or Red
    [Range(1,2)] public int Team;


    public float Speed;
    public bool isMoving {get; set;} = false;
    public bool isSelected {get; set;} = false;

    private TMP_Text _debugText;

    private GameBoard _myBoard;


    public UnityEvent<GamePiece> e_MoveBegin;
    public UnityEvent<GamePiece> e_MoveFinished;

    public void init(int boardX, int boardZ){
        BoardX = boardX;
        BoardZ = boardZ;
    }

    void Awake(){
        if(e_MoveFinished == null){
            e_MoveFinished = new UnityEvent<GamePiece>();
        }

        if(e_MoveBegin == null){
            e_MoveBegin = new UnityEvent<GamePiece>();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        _myBoard = GameObject.Find("TaflBoard").GetComponent<GameBoard>();

        _debugText = transform.Find("DebugText").gameObject.GetComponent<TMP_Text>();

        

    }

    // Update is called once per frame
    void Update()
    {
        // if(_debugText){
        //     _debugText.text = $"{BoardX},{BoardZ}";
        // }  
        
    }



    //Changes the piece's color.
    public void SetColor(Color color){
        var material = gameObject.GetComponentInChildren<Renderer>().material;
        material.color = color;
    }

    public void ResetColor(){
        var material = gameObject.GetComponentInChildren<Renderer>().material;
        material.color = Color.white;
    }

    public bool isKing(){
        return Type == "King";
    }


    //This function will translate the piece towards a specific location over time by using a coroutine.
    public void MoveTo(Vector3 loc){
        IEnumerator moveTo = c_MoveTo(loc);
        StartCoroutine(moveTo);
    }

    private IEnumerator c_MoveTo(Vector3 loc){
        isMoving = true;
        e_MoveBegin.Invoke(this);
        while(Vector3.Distance(loc, transform.position) > 0.001){
            //Debug.Log($"{gameObject.ToString()}'s position is {transform.position.ToString()}");
            Vector3 newPosition = Vector3.MoveTowards(gameObject.transform.position, loc, Speed * Time.deltaTime);
            gameObject.transform.position = newPosition;

            yield return null;
        }
        isMoving = false;
        gameObject.transform.position = loc; //Finish it up.
        e_MoveFinished.Invoke(this);

    }




}
