using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    public static PauseUI Instance { get; private set; }
    
    private void Awake() {
        Instance = this;
    }
    
    private void Start() {
        GameHandler.Instance.OnGamePaused += GameHandler_OnGamePaused;
        GameHandler.Instance.OnGameUnpaused += GameHandler_OnGameUnpaused;
        
        Hide();
    }

    private void GameHandler_OnGamePaused(object sender, EventArgs e) {
        Show();
    }
    
    private void GameHandler_OnGameUnpaused(object sender, EventArgs e) {
        Hide();
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    private void OnDestroy() {
        GameHandler.Instance.OnGamePaused -= GameHandler_OnGamePaused;
        GameHandler.Instance.OnGameUnpaused -= GameHandler_OnGameUnpaused;
    }
}
