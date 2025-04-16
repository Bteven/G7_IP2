using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{

    private GameObject bossObject;
    public GameObject bossBarObj;
    private HealthController controller;
    private GameWinPanelScript GameWinPanel;
    public UnityEngine.UI.Image bossBar;
    private bool infoGot = false;

     

    // Update is called once per frame
    void Update()
    {



        if (infoGot)
        {


            bossBarObj.SetActive(true);
            bossBar.fillAmount = controller.currentHealth / 5000;   // bad code but i in rush


            if (controller.currentHealth <= 0 || bossObject == null)
            {

                GameWinPanel = FindObjectOfType<GameWinPanelScript>();

                GameWinPanel.WinGame();


            }
        }
    }

    public void getUFO(GameObject UFO)
    {

        bossObject = UFO;
        controller = bossObject.GetComponent<HealthController>();
        infoGot = true;

    }

}
