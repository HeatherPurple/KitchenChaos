using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class RebindButtonTextUI : MonoBehaviour {
    
    private TextMeshProUGUI bindingText;
    private GameInput.Binding binding;

    private void Awake() {
        bindingText = GetComponentInChildren<TextMeshProUGUI>();
        binding = GetComponent<RebindButton>().GetBinding();
    }

    private void Start() {
        UpdateVisual();
        
        GameInput.Instance.OnInteractiveRebindEnded += GameInput_OnInteractiveRebindEnded;
    }

    private void GameInput_OnInteractiveRebindEnded(object sender, EventArgs e) {
        UpdateVisual();
    }

    private void UpdateVisual() {
        bindingText.text = GameInput.Instance.GetBindingText(binding);
    }

    private void OnDestroy() {
        GameInput.Instance.OnInteractiveRebindEnded -= GameInput_OnInteractiveRebindEnded;
    }
}
