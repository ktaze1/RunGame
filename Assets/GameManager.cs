using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool GameStarted { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        Invoke("StartGame", 1f);   
    }

    void StartGame()
    {
        GameStarted = true;
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        Invoke("LoadScene", 1f);
    }
}
