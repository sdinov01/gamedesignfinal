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
        public GameObject pauseAnimObject;
        public GameObject image;
        public GameObject pauseButton;

        public GameObject Button1;
        public GameObject Button2;
        public GameObject Button3;
        public GameObject Button4;
        public GameObject Button5;
        public GameObject Button6;


        void Awake(){
                SetVolume (volumeLevel);
                if (sliderVolumeCtrl != null && musicSource != null) {
                        sliderVolumeCtrl.value = musicSource.volume;
                        sliderVolumeCtrl.onValueChanged.AddListener(SetVolume);
                }
        }

        void Start(){
                pauseMenuUI.SetActive(false);
                pauseAnimObject.SetActive(false);
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
                        image.SetActive(false);
                        pauseButton.SetActive(false);
                        pauseMenuUI.SetActive(true);
                        Button1.SetActive(false);
                        Button2.SetActive(false);
                        Button3.SetActive(false);
                        Button4.SetActive(false);
                        Button5.SetActive(false);
                        Button6.SetActive(false);

                        pauseAnimObject.SetActive(true);
                        StartCoroutine(ShowButtonAfterDelay());

                        Time.timeScale = 0f;
                        AudioListener.pause = true;
                        GameisPaused = true;}
                else { Resume (); }
                //NOTE: This function is for the pause button
        }
        IEnumerator ShowButtonAfterDelay()
        {
                yield return new WaitForSecondsRealtime(1.5f);
                Debug.Log("delay");
                Button1.SetActive(true);
                Button2.SetActive(true);
                Button3.SetActive(true);
                Button4.SetActive(true);
                Button5.SetActive(true);
                Button6.SetActive(true);
        }

        public void Resume(){
                //Debug.Log("Clicked resume button");
                pauseAnimObject.SetActive(false);
                pauseMenuUI.SetActive(false);
                image.SetActive(true);
                pauseButton.SetActive(true);
                Time.timeScale = 1f;
                AudioListener.pause = false;
                GameisPaused = false;
        }

        public void SetVolume (float sliderValue){
                if (musicSource != null) {
                        volumeLevel = sliderValue;
                        musicSource.volume = sliderValue;
                        //Debug.Log("audio changed");
                }
                //Debug.Log(sliderValue);
        }

        public void RestartGame(){
                //Debug.Log("Clicked restart button");
                Time.timeScale = 1f;
                SceneManager.LoadScene("Menu_Scene");
                /* Unpauses audio */
                AudioListener.pause = false;
                // Please also reset all static variables here, for new games!
        }

        public void QuitLevelGame(){
                //Debug.Log("Clicked restart button");
                Time.timeScale = 1f;
                SceneManager.LoadScene("Level_Select_Scene");
                /* Unpauses audio */
                AudioListener.pause = false;
                // Please also reset all static variables here, for new games!
        }

        public void QuitGame(){
                //Debug.Log("Clicked quit button");
                #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                #else
                Application.Quit();
                #endif
        }
}