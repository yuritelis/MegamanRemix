using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class IntroScript : MonoBehaviour
{
    void Start()
    {
        Invoke("ChangeScene",8*Time.timeScale);
    }

    void ChangeScene()
    {
        SceneManager.LoadScene("Menu");
    }

    void Update()
    {
        
    }
}
