using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsButton : BaseButton
{
    protected override void OnClickAction() {
        OptionsUI.Instance.Show();
        PauseUI.Instance.Hide();
    }
}
