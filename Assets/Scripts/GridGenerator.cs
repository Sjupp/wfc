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

    private IEnumerator coroutine;
    private float timeStamp;

    void Start()
    {
        gridArray = new GameObject[gridSize * gridSize];
        GenerateGrid();

        //timeStamp = Time.time;

        coroutine = DelayedFunction(1.0f);
        StartCoroutine(coroutine);
    }

    private IEnumerator DelayedFunction(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        // Collapse One "random" Cell
        gridArray[4].GetComponent<Cell>().CollapseCellTo(4);

        for (int i = 0; i < 4; i++)
        {
            var neighborTargetPos = GetNeighborForPosition(4, i);
            Debug.Log(neighborTargetPos);
            if (neighborTargetPos >= 0)
            {
                Selection.activeGameObject = gridArray[neighborTargetPos];
            }
            yield return new WaitForSeconds(0.1f);
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
