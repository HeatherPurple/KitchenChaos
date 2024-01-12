using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour {

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;
    
    public static GameHandler Instance { get; private set; }
    
    [SerializeField] private float countdownToStartTimer = 3f;
    [SerializeField] private float gamePlayingTimerMax = 10f;
    
    private enum State {
        WaitingForStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    private State currentState;
    private float gamePlayingTimer;
    private bool isGameOnPause;
    
    private void Awake() {
        Instance = this;
        
        currentState = State.WaitingForStart;
    }

    private void Start() {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e) {
        if (currentState is State.WaitingForStart) {
            currentState = State.CountdownToStart;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e) {
        TogglePauseGame();
    }

    private void Update() {
        switch (currentState) {
            case State.WaitingForStart:
                break;
            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0) {
                    currentState = State.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0) {
                    currentState = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }
    }


    private void OnDestroy() {
        GameInput.Instance.OnPauseAction -= GameInput_OnPauseAction;
        GameInput.Instance.OnInteractAction -= GameInput_OnInteractAction;
    }

    public void TogglePauseGame() {
        isGameOnPause = !isGameOnPause;

        if (isGameOnPause) {
            Time.timeScale = 0f;
            
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        } else { 
            Time.timeScale = 1f;
            
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool IsGamePlaying() {
        return currentState == State.GamePlaying;
    }

    public bool IsCountdownToStart() {
        return currentState == State.CountdownToStart;
    }

    public bool IsGameOver() {
        return currentState == State.GameOver;
    }

    public float GetCountdownToStartTimer() {
        return countdownToStartTimer;
    }
    
    public float GetGamePlayingTimerNormalized() {
        return gamePlayingTimer / gamePlayingTimerMax;
    }
    
}
