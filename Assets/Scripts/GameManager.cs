﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !!_isGameOver)
            SceneManager.LoadScene(1);  //main/current Game Scene
    }

    public void GameOver()
    {
        _isGameOver = true;
    }
}
