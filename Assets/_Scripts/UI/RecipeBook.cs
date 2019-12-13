using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeBook : MonoBehaviour
{
    public GameObject recipeBookIngredients, recipeBookIntermediateOne, recipeBookIntermediateTwo, recipeBookMealsOne, recipeBookMealsTwo;

    public void IngredientsBookON()
    {
        recipeBookIngredients.SetActive(true);
    }

    public void IngredientsBookOFF()
    {
        recipeBookIngredients.SetActive(false);
    }

    public void InterOneBookON()
    {
        recipeBookIntermediateOne.SetActive(true);
    }

    public void InterOneBookOFF()
    {
        recipeBookIntermediateOne.SetActive(false);
    }

    public void InterTwoBookON()
    {
        recipeBookIntermediateTwo.SetActive(true);
    }

    public void InterTwoBookOFF()
    {
        recipeBookIntermediateTwo.SetActive(false);
    }

    public void MealsOneBookON()
    {
        recipeBookMealsOne.SetActive(true);
    }

    public void MealsOneBookOFF()
    {
        recipeBookMealsOne.SetActive(false);
    }
    public void MealsTwoBookON()
    {
        recipeBookMealsTwo.SetActive(true);
    }

    public void MealsTwoBookOFF()
    {
        recipeBookMealsTwo.SetActive(false);
    }
}