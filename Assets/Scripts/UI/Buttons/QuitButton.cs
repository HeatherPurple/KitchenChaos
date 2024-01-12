using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : BaseButton
{
    protected override void OnClickAction() {
        Application.Quit();
    }
}
