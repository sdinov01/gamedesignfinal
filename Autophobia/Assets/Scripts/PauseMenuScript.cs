using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenuHandler : MonoBehaviour {

        public static bool GameisPaused = false;
        public GameObject pauseMenuUI;
        public AudioSource audio;
        public static float volumeLevel = 1.0f;
        public Slider sliderVolumeCtrl;

        void Awake(){
                SetVolume (volumeLevel);
                if (sliderVolumeCtrl != null && audio != null) {
                        sliderVolumeCtrl.value = audio.volume;
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
                if (audio != null) {
                        volumeLevel = sliderValue;
                        audio.volume = sliderValue;
                }
                Debug.Log(sliderValue);
        }

        public void RestartGame(){
                Time.timeScale = 1f;
                SceneManager.LoadScene("MainMenu");
                // Please also reset all static variables here, for new games!
        }

        public void QuitGame(){
                #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                #else
                Application.Quit();
                #endif
        }
}