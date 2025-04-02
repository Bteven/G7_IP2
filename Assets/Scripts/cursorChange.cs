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

    private void Start()
    {
        TUManager = FindAnyObjectByType<TurretUpgradeManager>();
        TUManager.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        btn.onClick.AddListener(ChangeCursor);
        TUManager.enabled = true;
    }

    void ChangeCursor()
    {
        Cursor.SetCursor(upgradeCursor, Vector2.zero, CursorMode.ForceSoftware);
    }
}
