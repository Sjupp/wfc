using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; // For "Selection.activeGameObject"

public class GridGenerator : MonoBehaviour
{
    public int gridSize = 3;
    public GameObject[] gridArray;
    public GameObject cellPrefab;
    public CellVisualizer cellVisualizer;
    public Tile[] tiles;

    private IEnumerator coroutine;
    //private float timeStamp;

    void Start()
    {
        tiles = new Tile[4] { new Tile("plain", new int[] { 0, 0, 0, 0 }),
                              new Tile("vertical", new int[] { 0, 1, 0, 1 }),
                              new Tile("horizontal", new int[] { 1, 0, 1, 0 }),
                              new Tile("crossing", new int[] { 1, 1, 1, 1 }),};

        gridArray = new GameObject[gridSize * gridSize];
        GenerateGrid();

        //timeStamp = Time.time;

        coroutine = DelayedFunction(1.0f);
        StartCoroutine(coroutine);
    }

    private IEnumerator DelayedFunction(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        Debug.Log("Start");
        // Pick a cell and collapse it
        gridArray[0].GetComponent<Cell>().CollapseCellTo(3);

        //for (int i = 0; i < gridArray.Length; i++)
        //{
        //    Foo(i);
        //}

        Foo(1);

        // Collapse One "random" Cell
        //gridArray[4].GetComponent<Cell>().CollapseCellTo(4);

        //for (int i = 0; i < 4; i++)
        //{
        //    var neighborTargetPos = GetNeighborForPosition(4, i);
        //    Debug.Log(neighborTargetPos);
        //    if (neighborTargetPos >= 0)
        //    {
        //        Selection.activeGameObject = gridArray[neighborTargetPos];
        //    }
        //    yield return new WaitForSeconds(0.1f);
        //}
    }

    void Foo(int gridPos)
    {
        Debug.Log("Foo " + gridPos);
        // Check next cell in array
        var nextCell = gridPos;
        // Get neighbor of cell (in this case, the one we just collapsed)
        for (int dir = 0; dir < 4; dir++)
        {
            Debug.Log("direction: " + dir);
            var directionChecked = dir;

            var nbr = GetNeighborForPosition(nextCell, directionChecked);
            if (nbr <= -1)
            {
                Debug.Log("bad neighbor, returning");
                return;
            };

            // Get our domain for future reference
            var ourCell = gridArray[nextCell].GetComponent<Cell>();

            // Check neighbor's domain
            var nbrDomain = gridArray[nbr].GetComponent<Cell>().domain;

            // Check stuff
            for (int i = 0; i < nbrDomain.Length; i++)
            {
                if (nbrDomain[i]) // true == tile still a possibility
                {
                    // If domain 0 is true, that means tile 0 is a possibility.
                    // Since we checked from direction directionChecked, we are interested in the opposite dir to match sides.
                    var opp = (directionChecked + 2) % 4;

                    // Check each Tile[i].side[opp] up against our domain's Tile[j] sided [directionChecked]
                    for (int j = 0; j < ourCell.domain.Length; j++) // our domain vs nbrDomain should always be same size
                    {
                        // TileRule[i] should correspond to nbrDomain[i]
                        if (tiles[i].m_sides[opp] == tiles[j].m_sides[directionChecked])
                        {
                            // We have a match! You get to live.
                            Debug.Log($"{tiles[i].m_name} matches with {tiles[j].m_name}");
                        } 
                        else
                        {
                            // No match >:(
                            Debug.Log($"{tiles[i].m_name} does not match with {tiles[j].m_name}");
                            ourCell.RemoveOne(j);
                        }
                    }
                }
            }
        }
    }

    //void Update()
    //{
    //    if (Time.time - timeStamp > 0.5f)
    //    {
    //        gridArray[RNR(gridArray.Length)].GetComponent<Cell>().RemoveOne(RNR(4));

    //        timeStamp = Time.time + 10000.0f;
    //    }
    //}

    int GetNeighborForPosition(int gridPosition, int direction)
    {
        if (gridPosition >= gridSize * gridSize)
        {
            Debug.Log("Position is out of bounds!");
            return -2;
        }

        int targetPos;
            switch (direction)
            {
                case 0: // Right
                    {
                        if (gridPosition % 3 == gridSize - 1) return -1; // We are at right edge, thus no right side to check
                        targetPos = gridPosition + 1;
                        return targetPos;
                    }
                case 1: // Up
                    {
                        if (gridPosition >= (gridSize * gridSize) - gridSize) return -1; // We are at top edge ...
                        targetPos = gridPosition + gridSize;
                        return targetPos;
                }
                case 2: // Left
                    {
                        if (gridPosition % 3 == 0) return -1; // We are at left edge ...
                        targetPos = gridPosition - 1;
                        return targetPos;
                }
                case 3: // Down
                    {
                        if (gridPosition < gridSize) return -1; // We are at bottom edge ...
                        targetPos = gridPosition - gridSize;
                        return targetPos;
                }
            }
        return -1;
    }

    int RNR(int maxNr)
    {
        return Random.Range(0, maxNr);
    }

    void GenerateGrid()
    {
        for (int i = 0, y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++, i++)
            {
                gridArray[i] = Instantiate(cellPrefab, new Vector3(x, y, 0), Quaternion.identity);
            }
        }
    }

}
