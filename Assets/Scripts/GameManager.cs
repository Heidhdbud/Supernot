using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Linq;
public class GameManager : Singleton<GameManager>
{
    [Header("Menu")]
    [SerializeField] CanvasGroup menuUI;
    [SerializeField] CinemachineVirtualCamera playerCam;
    [SerializeField] CinemachineVirtualCamera MenuCam;
    
    [Header("Win Control")]
    [SerializeField] GameObject LoseUI;
    [SerializeField] GameObject WinUI;
    private bool gameFinished = false;
    [Space]
    public GameObject player;
    public List<GameObject> enemies;

    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList();
    }
    void Update()
    {
        if (gameFinished && Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex,LoadSceneMode.Single); // reload scene
        }
    }

    public void EndGame(bool won)
    {
        if (won)
        {
            WinUI.SetActive(true);
        }
        else
        {
            LoseUI.SetActive(true);
        }
        
        gameFinished = true;
    }
    public void StartButton()
    {
        menuUI.DOFade(0f, 1f);
        player.SetActive(true);

        MenuCam.Priority = 1;
        MenuCam.gameObject.SetActive(false);
        playerCam.Priority = 10;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}
