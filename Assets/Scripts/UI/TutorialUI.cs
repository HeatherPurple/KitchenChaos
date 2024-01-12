using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    private void Start() {
        GameHandler.Instance.OnStateChanged += GameHandler_StateChanged;

        Show();
    }

    private void GameHandler_StateChanged(object sender, EventArgs e) {
        if (GameHandler.Instance.IsCountdownToStart()) {
            Hide();    
        }
    }
    
    private void Hide() {
        gameObject.SetActive(false);
    }
    
    private void Show() {
        gameObject.SetActive(true);
    }
    
    private void OnDestroy() {
        GameHandler.Instance.OnGameUnpaused -= GameHandler_StateChanged;
    }
}
