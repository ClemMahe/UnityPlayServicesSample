using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonTextColorScript : Button
{
    public Color defaultTextColor;
    public Color disabledTextColor;

    void Update()
    {
        Button button = this.GetComponent<Button>();
        Text text = (Text)button.GetComponentInChildren<Text>();
        if (this.currentSelectionState == SelectionState.Disabled)
         {
             text.color = disabledTextColor;
         }
         else if (this.currentSelectionState == SelectionState.Disabled)
         {
             text.color = defaultTextColor;
         }
    }
}
