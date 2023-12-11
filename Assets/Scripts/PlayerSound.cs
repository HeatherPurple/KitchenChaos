using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour {

    private Player player;
    
    private float footStepTimer;
    private float footStepTimerMax = 0.2f;

    private void Awake() {
        player = GetComponent<Player>();
    }

    private void Update() {
        footStepTimer -= Time.deltaTime;
        if (footStepTimer <= 0) {
            footStepTimer = footStepTimerMax;
            
            float volume = 1f;            
            if (player.IsWalking()) {
                SoundManager.Instance.PlayFootstepsSound(transform.position, volume);
            }
        }
    }
}
