using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPanelsManager : MonoBehaviour {
    public IngredientPanel ingredientPanel;
    public RecipePanel recipePanel;

    [HideInInspector]
    public static PlayerPanelsManager Instance;

    private PlayerPanels lastPanel;
    private bool isHidden;

    private void Awake() {
        lastPanel = PlayerPanels.Ingredient;
        isHidden = false;
    }

    // Start is called before the first frame update
    void Start() {
        recipePanel.ClosePanel();
        Instance = this;
    }

    public void SwitchPanel(int panel) {
        ClosePanels();

        switch ((PlayerPanels)panel) {
            case PlayerPanels.Ingredient:
                lastPanel = PlayerPanels.Ingredient;
                ingredientPanel.OpenPanel();
                isHidden = false;
                break;
            case PlayerPanels.Recipe:
                lastPanel = PlayerPanels.Recipe;
                recipePanel.OpenPanel();
                isHidden = false;
                break;
            default:
                break;
        }
    }

    public void TogglePanels() {
        if (isHidden) {
            OpenPanels();
        }
        else {
            ClosePanels();
            isHidden = true;
        }
    }

    private void ClosePanels() {
        ingredientPanel.ClosePanel();
        recipePanel.ClosePanel();
    }

    private void OpenPanels() {
        SwitchPanel((int)lastPanel);
    }
}

public enum PlayerPanels {
    Ingredient,
    Recipe
}
