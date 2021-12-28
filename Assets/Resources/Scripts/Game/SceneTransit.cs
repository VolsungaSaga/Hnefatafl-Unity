using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  
using UnityEngine.UI;
using UnityEngine.Events;

public class SceneTransit : MonoBehaviour
{

    [Tooltip("The panel to save between scenes and fade in/out as required.")]
    public Image sceneCover;

    public UnityEvent e_FadeOutFinished;
    public UnityEvent e_FadeInFinished;

    [Tooltip("The name of the scene to go to after fade out completes.")]
    public string sceneTarget;

    public float FadeRate = 1.0f;


    void Awake(){



        //Hook up event listeners
        e_FadeInFinished.AddListener(onFadeInFinished);
        e_FadeOutFinished.AddListener(onFadeOutFinished);

        //Upon scene loading, we'll want to start a fade in.
        EnterScene();
        

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



  
    //The outward facing function, meant to be invoked by other components to go from one scene to another.
    public void GoToScene(string sceneName){
        SetSceneTarget(sceneName);
        FadeOut(FadeRate);
    }

    //Called on Awake; provides a fade-in effect when the scene is loaded.
    void EnterScene(){
        SetSceneCoverAlpha(1.0f);

        FadeIn(FadeRate);
    }

    //Once fading out is finished, load whatever target we've set in GoToScene()
    void onFadeOutFinished(){
        SceneManager.LoadSceneAsync(sceneTarget);
    }

    void onFadeInFinished(){

    }

    void SetSceneTarget(string sceneName){
        this.sceneTarget = sceneName;
    }

    void SetSceneCoverAlpha(float alpha){
        Color coverImageColor = sceneCover.color;
        coverImageColor.a = alpha;
        sceneCover.color = coverImageColor;
    }

    void FadeOut(float speed){
        IEnumerator fadeOut = c_FadeTo(speed, 1.0f, e_FadeOutFinished);
        StartCoroutine(fadeOut);
    }

    void FadeIn(float speed){
        IEnumerator fadeIn = c_FadeTo(speed, 0.0f, e_FadeInFinished);
        StartCoroutine(fadeIn);

    }

    private IEnumerator c_FadeTo(float speed, float alpha, UnityEvent callback){
        float targetAlpha = alpha;
        while(!Mathf.Approximately(sceneCover.color.a, targetAlpha)){

            float newAlpha = Mathf.MoveTowards(sceneCover.color.a, targetAlpha, speed * Time.deltaTime);
            Color panelImageColor = sceneCover.color;
            panelImageColor.a = newAlpha;
            sceneCover.color = panelImageColor;
            yield return null;
        }

        callback.Invoke();
        

    }

}
