using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : BaseButton
{
    protected override void OnClickAction() {
        Loader.LoadScene(Loader.Scene.GameScene);
    }
    
    private void OnEnable() {
        button.Select();
    }
}
