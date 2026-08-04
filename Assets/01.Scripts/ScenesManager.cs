using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public void LoadInGameScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("InGame");
    }
    public void LoadTitleScene()
    {
        SceneManager.LoadScene("Title");
    }
}
