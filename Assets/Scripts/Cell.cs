using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class Cell : MonoBehaviour
{
    public bool collapsed;
    public Tile[] tileOptions;
    public ZoneType zoneType;

    public void CreateCell(bool collapseState, Tile[] tiles)
    {
        collapsed = collapseState;
        tileOptions = tiles;
    }

    public void RecreateCell(Tile[] tiles)
    {
        tileOptions = tiles;
    }
}

