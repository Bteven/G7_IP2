using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cursorChange : MonoBehaviour
{
    // Start is called before the first frame update

    public Button btn;
    public Texture2D upgradeCursor;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        btn.onClick.AddListener(ChangeCursor);
    }

    void ChangeCursor()
    {
        Cursor.SetCursor(upgradeCursor, Vector2.zero, CursorMode.ForceSoftware);
    }
}
