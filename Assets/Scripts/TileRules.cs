using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRules : MonoBehaviour
{
}

// Side order: R > U > L > D
public class Tile_0 // Plain
{
    int[] sides = new int[] { 0, 0, 0, 0 };
}
public class Tile_1 // Vertical
{
    int[] sides = new int[] { 0, 1, 0, 1 };
}
public class Tile_2 // Horizontal
{
    int[] sides = new int[] { 1, 0, 1, 0 };
}
public class Tile_3 // Crossing
{
    int[] sides = new int[] { 1, 1, 1, 1 };
}
