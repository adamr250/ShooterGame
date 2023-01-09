using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;

    TMPro.TMP_Dropdown dropdown;
    public static float defaultDifficultyMultiplier = 1.0f;
    
    
    void Start()
    {
        dropdown = GetComponentInChildren<TMPro.TMP_Dropdown>();

        dropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(dropdown); 
        });

        dropdown.value = 1;
    }

    void DropdownValueChanged(TMPro.TMP_Dropdown change)
    {
        float val = change.value;

        if(val == 0)
        {
            defaultDifficultyMultiplier = 0.8f;
        }
        else if(val == 1)
        {
            defaultDifficultyMultiplier = 1.0f;
        } 
        else if (val == 2)
        {
            defaultDifficultyMultiplier = 1.2f;
        }

        Debug.Log("difficulty: " + defaultDifficultyMultiplier);
    }

    public void goBack()
    {
        mainMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
