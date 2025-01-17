using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIButton : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Labirent");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
