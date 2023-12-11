using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryMangerUI : MonoBehaviour {

    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;

    private void Awake() {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start() {
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeDelivered += DeliveryManager_OnRecipeDelivered;
        
        UpdateUI();
    }

    private void DeliveryManager_OnRecipeDelivered(object sender, EventArgs e) {
        UpdateUI();
    }
    
    private void DeliveryManager_OnRecipeSpawned(object sender, EventArgs e) {
        UpdateUI();
    }

    private void UpdateUI() {
        foreach (Transform child in container) {
            if (child == recipeTemplate) continue;
            
            Destroy(child.gameObject);
        }

        foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList()) {
            Transform recipe = Instantiate(recipeTemplate, container);
            recipe.gameObject.SetActive(true);
            recipe.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSO);
        }
    }

    private void OnDisable() {
        DeliveryManager.Instance.OnRecipeSpawned -= DeliveryManager_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeDelivered -= DeliveryManager_OnRecipeDelivered;
    }
}
