using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ToggleDown : Toggle
{
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);

        if (isOn)
        {
            Screen.fullScreen = false;
        }
        else
        {
            Screen.fullScreen = true;
        }
    }
}
