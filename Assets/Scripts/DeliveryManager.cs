using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour {
    
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeDelivered;
    public event EventHandler OnDeliverySuccess;
    public event EventHandler OnDeliveryFailed;
    public static DeliveryManager Instance { get; private set; }
    
    [SerializeField] private RecipeListSO recipeListSO;
    
    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;
    private int successfullDeliveries;
    
    private void Awake() {
        Instance = this;

        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update() {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0) {
            spawnRecipeTimer = spawnRecipeTimerMax;
            
            if (waitingRecipeSOList.Count < waitingRecipesMax) {
                RecipeSO waitingRecipe = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
                
                waitingRecipeSOList.Add(waitingRecipe);
                
                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject) {
        
        // Iterating through all waiting recipes
        for (int i = 0; i < waitingRecipeSOList.Count; i++) {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];
            if (plateKitchenObject.GetKitchenObjectSOList().Count == waitingRecipeSO.kitchenObjectSOList.Count) {
                bool plateContentsMatchesRecipe = true;
                
                // Iterating through all ingredients in waiting recipe
                foreach (KitchenObjectSO recipeIngredient in waitingRecipeSO.kitchenObjectSOList) {
                    bool ingredientFound = false;
                    
                    // Iterating through all ingredients in plate recipe
                    foreach (KitchenObjectSO plateIngredient in plateKitchenObject.GetKitchenObjectSOList()) {
                        if (recipeIngredient == plateIngredient) {
                            ingredientFound = true;
                            break;
                        }
                    }

                    if (!ingredientFound) {
                        plateContentsMatchesRecipe = false;
                        break;
                    }
                }

                if (plateContentsMatchesRecipe) {
                    waitingRecipeSOList.RemoveAt(i);

                    successfullDeliveries++;
                    
                    OnRecipeDelivered?.Invoke(this, EventArgs.Empty);
                    OnDeliverySuccess?.Invoke(this, EventArgs.Empty);
                    
                    return;
                }
            }
        }
        
        Debug.Log("Fail");
        OnDeliveryFailed?.Invoke(this, EventArgs.Empty);
        
    }

    public int GetSuccessfulDeliveriesNumber() {
        return successfullDeliveries;
    }

    public List<RecipeSO> GetWaitingRecipeSOList() {
        return waitingRecipeSOList;
    }
}
