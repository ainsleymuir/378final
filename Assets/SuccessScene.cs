using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SuccessScene : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("StartMenuScene");
    }
}