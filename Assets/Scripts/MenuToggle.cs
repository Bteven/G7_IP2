using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuToggle : MonoBehaviour
{
    public GameObject menuPanel;
    public void ToggleMenu()
    {
        menuPanel.SetActive(!menuPanel.activeSelf);
    }
}
