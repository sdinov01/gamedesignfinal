using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenuHandler : MonoBehaviour {

        public bool GameisPaused = false;
        public GameObject pauseMenuUI;
        public AudioSource musicSource;
        public float volumeLevel = 1.0f;
        public Slider sliderVolumeCtrl;

        void Awake(){
                SetVolume (volumeLevel);
                if (sliderVolumeCtrl != null && musicSource != null) {
                        sliderVolumeCtrl.value = musicSource.volume;
                        sliderVolumeCtrl.onValueChanged.AddListener(SetVolume);
                }
        }

        void Start(){
                pauseMenuUI.SetActive(false);
                GameisPaused = false;
        }

        void Update(){
                if (Input.GetKeyDown(KeyCode.Escape)){
                        if (GameisPaused){ Resume(); }
                        else{ Pause(); }
                }
        }

        public void Pause(){
                if (!GameisPaused){
                        pauseMenuUI.SetActive(true);
                        Time.timeScale = 0f;
                        AudioListener.pause = true;
                        GameisPaused = true;}
             else { Resume (); }
             //NOTE: This function is for the pause button
        }

        public void Resume(){
                Debug.Log("Clicked resume button");
                pauseMenuUI.SetActive(false);
                Time.timeScale = 1f;
                AudioListener.pause = false;
                GameisPaused = false;
        }

        public void SetVolume (float sliderValue){
                if (musicSource != null) {
                        volumeLevel = sliderValue;
                        musicSource.volume = sliderValue;
                        Debug.Log("audio changed");
                }
                Debug.Log(sliderValue);
        }

        public void RestartGame(){
                Debug.Log("Clicked restart button");
                Time.timeScale = 1f;
                SceneManager.LoadScene("Menu_Scene");
                // Please also reset all static variables here, for new games!
        }

        public void QuitGame(){
                Debug.Log("Clicked quit button");
                #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                #else
                Application.Quit();
                #endif
        }
}