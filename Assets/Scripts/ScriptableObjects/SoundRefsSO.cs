using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class SoundRefsSO : ScriptableObject {
    
    public AudioClip[] cutSoundArray;
    public AudioClip[] deliverySuccessSoundArray;
    public AudioClip[] deliveryFailedSoundArray;
    public AudioClip[] footstepSoundArray;
    public AudioClip[] dropSoundArray;
    public AudioClip[] pickupSoundArray;
    public AudioClip panSound;
    public AudioClip[] trashSoundArray;
    public AudioClip[] warningSoundArray;
}
