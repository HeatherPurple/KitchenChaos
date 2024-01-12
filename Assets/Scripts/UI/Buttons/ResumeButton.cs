using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ResumeButton : BaseButton {

    
    protected override void OnClickAction() {
        GameHandler.Instance.TogglePauseGame();
    }

    private void OnEnable() {
        button.Select();
    }
}
