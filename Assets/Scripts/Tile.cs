using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ZoneType
{
    Daylight,
    Middle,
    Abyss
}


public class Tile : MonoBehaviour
{
    public ZoneType zoneType;
    public Tile[] upNeighbours;
    public Tile[] rightNeighbours;
    public Tile[] downNeighbours;
    public Tile[] leftNeighbours;
}