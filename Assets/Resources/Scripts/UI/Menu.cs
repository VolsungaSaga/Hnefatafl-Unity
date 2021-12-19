using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Menu : MonoBehaviour
{

    public bool Opened = false;


    public UnityEvent<GameObject> e_onFadeOut;

    public UnityEvent<GameObject> e_onFadeIn;


    // Start is called before the first frame update
    void Start()
    {
        if(!Opened){
            SetAlphaAllChildren(0.0f);
            SetAllChildButtons(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void Open(){
        SetAllChildButtons(true);
        FadeInAllChildren(0.5f);
        Opened = true;
    }

    public void Close(){
        SetAllChildButtons(false);
        FadeOutAllChildren(0.5f);
        Opened = false;
    }




    private void SetAllChildButtons(bool on){
        Button[] childButtons = gameObject.GetComponentsInChildren<Button>(false);

        foreach (Button button in childButtons)
        {
            button.interactable = on;
        }
    }


    private void SetAlphaAllChildren(float alpha){
        Image[] childrenImages = gameObject.GetComponentsInChildren<Image>(false);
        Text[] childrenText = gameObject.GetComponentsInChildren<Text>(false);

        foreach (Image childImage in childrenImages){
            Color currColor = childImage.color;
            currColor.a = alpha;
            childImage.color = currColor;
            }

        foreach(Text childText in childrenText){
            Color currColor = childText.color;
            currColor.a = alpha;
            childText.color = currColor;
        }
    }

    private void FadeOutAllChildren(float timeToFade){

        IEnumerator fadeOut = c_fadeOutAllChildren(timeToFade);
        StartCoroutine(fadeOut);
    }

    private void FadeInAllChildren(float timeToFade){
        IEnumerator fadeIn = c_fadeInAllChildren(timeToFade);
        StartCoroutine(fadeIn);
    }


    private IEnumerator c_fadeInAllChildren(float timeToFade){
        //First, we get all the image and text components of the game object.
        Image[] childrenImages = gameObject.GetComponentsInChildren<Image>(false);
        Text[] childrenText = gameObject.GetComponentsInChildren<Text>(false);

        float currentTimeFromStart = 0.0f;
        float targetAlpha = 1.0f;
        float originAlpha = 0.0f;

        while(currentTimeFromStart <= timeToFade){

            foreach (Image childImage in childrenImages){
                Color currColor = childImage.color;
                currColor.a = Mathf.Lerp(originAlpha, targetAlpha, currentTimeFromStart / timeToFade);
                childImage.color = currColor;
            }

            foreach(Text childText in childrenText){
                Color currColor = childText.color;
                currColor.a = Mathf.Lerp(originAlpha, targetAlpha, currentTimeFromStart / timeToFade);
                childText.color = currColor;
            }

            currentTimeFromStart += Time.deltaTime;

            yield return null;

        }

        //Finish
        foreach (Image childImage in childrenImages){
            Color currColor = childImage.color;
            currColor.a = targetAlpha;
            childImage.color = currColor;
        }

        foreach (Text childText in childrenText){
            Color currColor = childText.color;
            currColor.a = targetAlpha;
            childText.color = currColor;
        }

        //Invoke fade-in event
        e_onFadeIn.Invoke(gameObject);

    }

    /*
    Lerps the alphas of all child images and text to 0.0, assuming that their alphas are at 1.0 to begin with.
    */
    private IEnumerator c_fadeOutAllChildren(float timeToFade){

        //First, we get all the image and text components of the game object.
        Image[] childrenImages = gameObject.GetComponentsInChildren<Image>(false);
        Text[] childrenText = gameObject.GetComponentsInChildren<Text>(false);

        float currentTimeFromStart = 0.0f;
        float targetAlpha = 0.0f;
        float originAlpha = 1.0f;

        while(currentTimeFromStart <= timeToFade){

            foreach (Image childImage in childrenImages){
                Color currColor = childImage.color;
                currColor.a = Mathf.Lerp(originAlpha, targetAlpha, currentTimeFromStart / timeToFade);
                childImage.color = currColor;
            }

            foreach(Text childText in childrenText){
                Color currColor = childText.color;
                currColor.a = Mathf.Lerp(originAlpha, targetAlpha, currentTimeFromStart / timeToFade);
                childText.color = currColor;
            }

            currentTimeFromStart += Time.deltaTime;

            yield return null;

        }

        //Finish
        foreach (Image childImage in childrenImages){
            Color currColor = childImage.color;
            currColor.a = targetAlpha;
            childImage.color = currColor;
        }

        foreach (Text childText in childrenText){
            Color currColor = childText.color;
            currColor.a = targetAlpha;
            childText.color = currColor;
        }

        //Invoke fade-out event
        e_onFadeOut.Invoke(gameObject);


    }
}
