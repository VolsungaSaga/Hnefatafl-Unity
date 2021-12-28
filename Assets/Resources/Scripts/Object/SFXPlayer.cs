using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{

    public AudioClip[] clips;
    public AudioSource source;

    private bool playing;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PlaySound(AudioClip clip){
        source.PlayOneShot(clip);
    }

    public void PlayRandomSound(){
        int randIndex = Random.Range(0, clips.Length - 1);
        PlaySound(clips[randIndex]);
    }
}
