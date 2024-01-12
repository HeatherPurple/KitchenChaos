using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MainMenuButton : BaseButton
{
    protected override void OnClickAction() {
        GameHandler.Instance.TogglePauseGame();
        Loader.LoadScene(Loader.Scene.MainMenu);
    }
}
