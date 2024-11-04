using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private bool gameFinished = false;
    public List<GameObject> enemies;

    void Update()
    {
        if (gameFinished && Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // reload scene
        }
    }

    public void EndGame(bool won)
    {
        TextMeshProUGUI[] textlist = null;
        if (won)
        {
            GameObject parent = GameObject.FindWithTag("YouWin");
            textlist = parent.GetComponentsInChildren<TextMeshProUGUI>(true);
            
        }
        else
        {
            GameObject parent = GameObject.FindWithTag("GameOver");
            textlist = parent.GetComponentsInChildren<TextMeshProUGUI>(true);
        }

        foreach (TextMeshProUGUI text in textlist)
        {
            text.enabled = true;
        }
        
        gameFinished = true;
    }
}
