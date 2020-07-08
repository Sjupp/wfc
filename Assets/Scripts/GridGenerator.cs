using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        //coroutine = DelayedFunction(0.5f);
        //StartCoroutine(coroutine);
        //Debug.Log("Starting Coroutine");
        timeStamp = Time.time;
    }

    void Update()
    {
        if (Time.time - timeStamp > 0.1f)
        {
            gridArray[RNR(gridArray.Length)].GetComponent<Cell>().RemoveOne(RNR(4));
            timeStamp = Time.time;
        }
    }

    private IEnumerator DelayedFunction(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Debug.Log("RemoveOne");
        //gridArray[0].GetComponent<Cell>().RemoveOne(1);
        for (int i = 0; i < 20; i++)
        {
            gridArray[RNR(gridArray.Length)].GetComponent<Cell>().RemoveOne(RNR(4));
        }
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
                var obj = gridArray[i] = Instantiate(cellPrefab, new Vector3(x, y, 0), Quaternion.identity);
            }
        }
    }

}
