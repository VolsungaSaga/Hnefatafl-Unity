using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public AudioClip[] playlist;

    private AudioClip currentPlayingClip;
    private int currentPlayingIndex;

    private bool playing = false;

    public AudioSource musicSource;

    // Start is called before the first frame update
    void Start()
    {
        currentPlayingIndex = 0;
        ShufflePlaylist();
        currentPlayingClip = playlist[currentPlayingIndex];
        PlaySong(currentPlayingClip);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!musicSource.isPlaying && playing == true){
            GoToNext();
        }
        
    }


    void Mute(){
        musicSource.mute = true;
    }

    void GoToNext(){
        currentPlayingIndex = (currentPlayingIndex + 1) % playlist.Length;
        currentPlayingClip = playlist[currentPlayingIndex];
        PlaySong(currentPlayingClip);
    }

    void PlaySong(AudioClip song){
        musicSource.Stop();
        musicSource.clip = song;
        musicSource.Play();
        playing = true;
    }

    void StopCurrentSong(){
        musicSource.Stop();
        playing = false;
    }

    void PauseCurrentSong(){
        musicSource.Pause();
        playing = false;
    }



    void ShufflePlaylist(){
        for(int i = 0; i < playlist.Length; i++){
            var tmp = playlist[i];
            int randIndex = Random.Range(0, playlist.Length - 1);
            playlist[i] = playlist[randIndex];
            playlist[randIndex] = tmp;
        }
    }

    void StartPlaylist(){
        currentPlayingIndex = 0;
        currentPlayingClip = playlist[currentPlayingIndex];
        PlaySong(currentPlayingClip);
    }

    void ResumePlaylist(){
        
    }

    void StopPlaylist(){

    }



}
