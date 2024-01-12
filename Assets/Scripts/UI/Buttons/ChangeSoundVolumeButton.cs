using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSoundVolumeButton : BaseButton {
    
    protected override void OnClickAction() {
        SoundManager.Instance.ChangeVolume();
    }
}