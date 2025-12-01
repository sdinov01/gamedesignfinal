using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class instruction : MonoBehaviour
{
    public GameObject eye1;
    public GameObject eye2;
    
    public GameObject clickText1;
    public GameObject clickText2;
    public GameObject readyText;
    
    private Animator eye1Animator;
    private Animator eye2Animator;
    
    void Start()
    {
        // 初始化组件
        eye1Animator = eye1.GetComponent<Animator>();
        eye2Animator = eye2.GetComponent<Animator>();
        
        // 初始状态设置
        clickText1.SetActive(false);
        clickText2.SetActive(false);
        readyText.SetActive(false);

        StartCoroutine(TutorialSequence());
    }
    
    IEnumerator TutorialSequence()
    {
        //first eye
        eye1Animator.Play("eye_animation", -1, 0f);
        yield return new WaitForSecondsRealtime(1.04f);
        // show click hint
        clickText1.SetActive(true);
        
        yield return new WaitForSecondsRealtime(4f);
        clickText1.SetActive(false);
        eye2Animator.Play("eye_animation", -1, 0f);
        yield return new WaitForSecondsRealtime(1.04f);
        clickText2.SetActive(true);


        yield return new WaitForSecondsRealtime(1f);
        
        // show ready
        clickText2.SetActive(false);
        readyText.SetActive(true);
        
        // 等待几秒后加载场景
        yield return new WaitForSecondsRealtime(2f);
        
        // 加载游戏场景
        // SceneManager.LoadScene("YourGameSceneName");
    }

    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.CompareTag("Player"))
    //         playerInside = true;
    // }
}