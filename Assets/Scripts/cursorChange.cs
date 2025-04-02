using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class cursorChange : MonoBehaviour
{
    // Start is called before the first frame update

    public Button btn;
    public Texture2D upgradeCursor;
    private TurretUpgradeManager TUManager;
    private bool upgradeActive;

    private void Start()
    {
        TUManager = FindAnyObjectByType<TurretUpgradeManager>();
        TUManager.enabled = false;
        upgradeActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        btn.onClick.AddListener(ChangeCursor);

        if(upgradeActive)
        {
            btn.onClick.AddListener(Revert);
        }
    }

    void ChangeCursor()
    {
        upgradeActive = true;
        Cursor.SetCursor(upgradeCursor, Vector2.zero, CursorMode.ForceSoftware);
        TUManager.enabled = true;
    }

    void Revert()
    {
        upgradeActive = false;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
        TUManager.enabled = false;
    }
}
