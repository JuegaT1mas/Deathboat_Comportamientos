using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonDown : Button
{

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);

        MainMenu mainMenu = GameObject.FindGameObjectWithTag("MainMenu").GetComponent<MainMenu>();

        mainMenu.ClickSound();
        mainMenu.LoadScene(1);
        Cursor.lockState = CursorLockMode.Locked;

    }
}
