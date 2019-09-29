using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator wiz;

    private void Start()
    {
        wiz.SetTrigger("IdleBlink");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("level1");
    }
}
