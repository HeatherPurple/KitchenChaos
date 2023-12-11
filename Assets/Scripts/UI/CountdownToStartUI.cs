using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownToStartUI : MonoBehaviour {
    
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

    private void Start() {
        GameHandler.Instance.OnStateChanged += GameHandler_OnStateChanged;
        
        Hide();
    }

    private void GameHandler_OnStateChanged(object sender, EventArgs e) {
        if (GameHandler.Instance.IsCountdownToStart()) {
            Show();
        } else {
            Hide();
        }
    }


    private void Update() {
        textMeshProUGUI.text = Mathf.Ceil(GameHandler.Instance.GetCountdownToStartTimer()).ToString();
    }


    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void OnDestroy() {
        GameHandler.Instance.OnStateChanged -= GameHandler_OnStateChanged;
    }
}
