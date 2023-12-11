using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class SoundManager : MonoBehaviour {

    public static SoundManager Instance { get; private set; }
    
    [SerializeField] private SoundRefsSO soundRefsSO;
    

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        DeliveryManager.Instance.OnDeliverySuccess += DeliveryManager_OnDeliverySuccess;
        DeliveryManager.Instance.OnDeliveryFailed += DeliveryManagerOnDeliveryFailed;
        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
        BaseCounter.OnAnyObjectPlaced += BaseCounter_OnAnyObjectPlaced;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectPlaced;
    }

    private void TrashCounter_OnAnyObjectPlaced(object sender, EventArgs e) {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(soundRefsSO.trashSoundArray, trashCounter.transform.position);
    }

    private void BaseCounter_OnAnyObjectPlaced(object sender, EventArgs e) {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(soundRefsSO.dropSoundArray, baseCounter.transform.position);
    }

    private void Player_OnPickedSomething(object sender, EventArgs e) {
        Player player = sender as Player;
        PlaySound(soundRefsSO.pickupSoundArray, player.transform.position);
    }

    private void DeliveryManagerOnDeliveryFailed(object sender, EventArgs e) {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(soundRefsSO.deliveryFailedSoundArray, deliveryCounter.transform.position);
    }
    
    private void DeliveryManager_OnDeliverySuccess(object sender, EventArgs e) {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(soundRefsSO.deliverySuccessSoundArray, deliveryCounter.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, EventArgs e) {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(soundRefsSO.cutSoundArray, cuttingCounter.transform.position);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f) {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }
    
    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f) {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    private void OnDisable() {
        CuttingCounter.OnAnyCut -= CuttingCounter_OnAnyCut;
        DeliveryManager.Instance.OnDeliverySuccess -= DeliveryManager_OnDeliverySuccess;
        DeliveryManager.Instance.OnDeliveryFailed -= DeliveryManagerOnDeliveryFailed;
        Player.Instance.OnPickedSomething -= Player_OnPickedSomething;
        BaseCounter.OnAnyObjectPlaced -= BaseCounter_OnAnyObjectPlaced;
        TrashCounter.OnAnyObjectTrashed -= TrashCounter_OnAnyObjectPlaced;
    }

    public void PlayFootstepsSound(Vector3 position, float volume = 1f) {
        PlaySound(soundRefsSO.footstepSoundArray, position, volume);
    }
}
