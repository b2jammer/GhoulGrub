﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientPanel : MonoBehaviour
{
    public void OpenPanel() {
        gameObject.SetActive(true);
    }

    public void ClosePanel() {
        gameObject.SetActive(false);
    }
}
