using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SpeedUp : MonoBehaviour
{
  public UnityEngine.UI.Button speedUpButton;
    public Sprite speedUpImage;
    public Sprite speedDownImage;

    public UnityEngine.UI.Image boostImage;

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
        Time.timeScale = 5;
        boostImage.sprite = speedDownImage;
    }

    private void ChangeSpeedDown()
    {
        Time.timeScale = 1;
        boostImage.sprite = speedUpImage;
    }
}
