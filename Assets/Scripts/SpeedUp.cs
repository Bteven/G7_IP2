using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SpeedUp : MonoBehaviour
{
  public Button speedUpButton;

    private void Update()
    {
        speedUpButton.onClick.AddListener(ChangeSpeedUp);

        if(Time.timeScale > 1)
        {
            speedUpButton.onClick.AddListener(ChangeSpeedDown);
        }
    }

    private void ChangeSpeedUp()
    {
        Time.timeScale = 2;
    }

    private void ChangeSpeedDown()
    {
        Time.timeScale = 1;
    }
}
