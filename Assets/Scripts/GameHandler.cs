using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour {

    public event EventHandler OnStateChanged; 
    
    public static GameHandler Instance { get; private set; }
    
    [SerializeField] private float waitingForStartTimer = 3f;
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
    
    private void Awake() {
        Instance = this;
        
        currentState = State.WaitingForStart;
    }

    private void Update() {
        switch (currentState) {
            case State.WaitingForStart:
                waitingForStartTimer -= Time.deltaTime;
                if (waitingForStartTimer < 0) {
                    currentState = State.CountdownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
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
