using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    // Hard coded to four tiles
    [SerializeField]
    public bool[] domain;
    private void Awake()
    {
        domain = new bool[4] { true, true, true, true };
    }

    public void CollapseCellTo(int inputNumber)
    {
        if (inputNumber >= domain.Length)
        {
            Debug.LogWarning("Input number out of bounds");
            return;
        }

        for (int i = 0; i < 4; i++)
        {
            //_ = inputNumber == i ? domain[i] = true : domain[i] = false;

            if (inputNumber == i)
            {
                domain[i] = true;
            }
            else
            {
                domain[i] = false;
                gameObject.GetComponent<CellVisualizer>().RemoveOne(i);
            }
        }
    }

    public void RemoveOne(int inputNumber)
    {
        Debug.Log("removeOne: " + inputNumber);
        if (inputNumber >= domain.Length) return;

        domain[inputNumber] = false;
        gameObject.GetComponent<CellVisualizer>().RemoveOne(inputNumber);
    }
}
