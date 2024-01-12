using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusicVolumeButton : BaseButton {
    
    protected override void OnClickAction() {
        MusicManager.Instance.ChangeVolume();
    }
    
    
}
