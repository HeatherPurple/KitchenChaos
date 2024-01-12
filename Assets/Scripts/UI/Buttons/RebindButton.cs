using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RebindButton : BaseButton {
    
    
    [SerializeField] private GameInput.Binding binding;
    
    protected override void OnClickAction() {
        GameInput.Instance.RebindBinding(binding);
    }

    public GameInput.Binding GetBinding() {
        return binding;
    }
}