using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OptionsUI : MonoBehaviour {
    
    public static OptionsUI Instance { get; private set; }
    
    [SerializeField] private TextMeshProUGUI soundVolumeText;
    [SerializeField] private TextMeshProUGUI musicVolumeText;
    [SerializeField] private GameObject pressToRebindKeyScreen;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        GameHandler.Instance.OnGameUnpaused += GameHandler_OnGameUnpaused;
        MusicManager.Instance.OnVolumeChanged += MusicManager_OnVolumeChanged;
        SoundManager.Instance.OnVolumeChanged += SoundManager_OnVolumeChanged;
        GameInput.Instance.OnInteractiveRebindStarted += GameInput_OnInteractiveRebindStarted;
        GameInput.Instance.OnInteractiveRebindEnded += GameInput_OnInteractiveRebindEnded;
        
        UpdateVisual();
        
        HidePressToRebindKeyScreen();
        Hide();
    }

    private void GameInput_OnInteractiveRebindEnded(object sender, EventArgs e) {
        HidePressToRebindKeyScreen();
    }

    private void GameInput_OnInteractiveRebindStarted(object sender, EventArgs e) {
        ShowPressToRebindKeyScreen();
    }

    private void SoundManager_OnVolumeChanged(object sender, EventArgs e) {
        UpdateVisual();
    }

    private void MusicManager_OnVolumeChanged(object sender, EventArgs e) {
        UpdateVisual();
    }

    private void GameHandler_OnGameUnpaused(object sender, EventArgs e) {
        Hide();
    }

    private void UpdateVisual() {
        soundVolumeText.text = "Sound effects volume: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicVolumeText.text = "Music volume: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);
    }
    
    private void OnDestroy() {
        GameHandler.Instance.OnGameUnpaused -= GameHandler_OnGameUnpaused;
        MusicManager.Instance.OnVolumeChanged -= MusicManager_OnVolumeChanged;
        SoundManager.Instance.OnVolumeChanged -= SoundManager_OnVolumeChanged;
        GameInput.Instance.OnInteractiveRebindStarted -= GameInput_OnInteractiveRebindStarted;
        GameInput.Instance.OnInteractiveRebindEnded -= GameInput_OnInteractiveRebindEnded;
    }
    
    private void HidePressToRebindKeyScreen() {
        pressToRebindKeyScreen.SetActive(false);
    }
    
    private void ShowPressToRebindKeyScreen() {
        pressToRebindKeyScreen.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
    
    public void Show() {
        gameObject.SetActive(true);
    }
    
}
