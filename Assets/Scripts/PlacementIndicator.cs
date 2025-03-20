using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementIndicator : MonoBehaviour
{
    private Renderer rend;
    public Material validColor;
    public Material invalidColor;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        SetValidPlacement(true); 
    }

    public void SetValidPlacement(bool isValid)
    {
        rend.material = isValid ? validColor : invalidColor;
    }
}
