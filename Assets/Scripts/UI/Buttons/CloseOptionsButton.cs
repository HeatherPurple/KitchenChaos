using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseOptionsButton : BaseButton
{
    protected override void OnClickAction() {
        OptionsUI.Instance.Hide();
        PauseUI.Instance.Show();
    }
    
    private void OnEnable() {
        button.Select();
    }
}
