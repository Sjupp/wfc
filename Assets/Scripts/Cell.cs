using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    // Hard coded to four tiles
    bool[] domain;

    public void Awake()
    {
        InitializeCells();
    }

    public void InitializeCells()
    {
        domain = new bool[] { true, true, true, true };
    }

    public void CollapseCellTo(int inputNumber)
    {
        if (inputNumber > domain.Length) return;

        for (int i = 0; i < 4; i++)
        {
            _ = inputNumber == i ? domain[i] = true : domain[i] = false;
        }
    }

    public void RemoveOne(int inputNumber)
    {
        if (inputNumber > domain.Length) return;

        for (int i = 0; i < 4; i++)
        {
            if (inputNumber == i) { 
                domain[i] = false;
                gameObject.GetComponent<CellVisualizer>().RemoveOne(i);
            }
        }
    }
}
