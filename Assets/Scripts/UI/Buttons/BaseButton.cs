using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BaseButton : MonoBehaviour {
    
    protected Button button;
    
    protected void Awake() {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickAction);
    }

    protected virtual void OnClickAction() {
    }

    protected void OnDestroy() {
        button.onClick.RemoveListener(OnClickAction);
    }
}
