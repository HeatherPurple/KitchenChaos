using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class SoundManager : MonoBehaviour {

    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";
    
    public static SoundManager Instance { get; private set; }
    
    public event EventHandler OnVolumeChanged;
    
    [SerializeField] private SoundRefsSO soundRefsSO;

    private float volume = 1f;
    

    private void Awake() {
        Instance = this;

        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);
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

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volumeMultiplier = 1f) {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volumeMultiplier);
    }
    
    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f) {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
    }

    private void OnDestroy() {
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
    
    public void ChangeVolume() {
        volume += 0.1f;
        if (volume >= 1.1f) {
            volume = 0f;
        }
        
        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);
        PlayerPrefs.Save();
        
        OnVolumeChanged?.Invoke(this, EventArgs.Empty);
    }

    public float GetVolume() {
        return volume;
    }
}
