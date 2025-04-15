using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeIconScript : MonoBehaviour
{
    public TurretUpgradeManager turretUpgradeManager;
    public List<Sprite> icons;
    public Image finalIcon;

    // Start is called before the first frame update

    private void Awake()
    {
        ChangeIcon();
    }

    // Update is called once per frame
    void Update()
    {

        ChangeIcon();
    }

    public void ChangeIcon()
    {
        for (int i = 0; i < icons.Count; i++)

          

        if (i == turretUpgradeManager.turretTypeIndicator)
            {
                  finalIcon.sprite = icons[i];
            }

    }
       

}
