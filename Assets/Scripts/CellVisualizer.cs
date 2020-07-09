using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellVisualizer : MonoBehaviour
{
    public GameObject quad;
    public Material[] tiles;

    private Vector3 tempV;

    public GameObject[] arrayWithObj;

    public void Start()
    {
        arrayWithObj = new GameObject[4] { null, null, null, null };
        FillCell();
    }

    public void FillCell()
    {
        for (int y = -1, i = 0; y < 2; y += 2) 
        {
            for (int x = -1; x < 2; x += 2, i++)
            {
                tempV.Set(gameObject.transform.position.x + x / 5.0f, gameObject.transform.position.y + y / 5.0f, gameObject.transform.position.z);
                var obj = Instantiate(quad, tempV, Quaternion.identity, gameObject.transform);
                obj.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
                obj.GetComponent<MeshRenderer>().material = tiles[i];
                arrayWithObj[i] = obj;
            }
        }
    }

    public void RemoveOne(int tileNumber = -1)
    {
        if (CheckIfSingular()) return;

        int number;

        _ = tileNumber != -1 ? number = tileNumber : number = Random.Range(0, 4);

        if (arrayWithObj[number])
        {
            Debug.Log("Destroying object " + number);
            DestroyImmediate(arrayWithObj[number]);
        }
        else
        {
            Debug.Log("Object is null");
        }

        if (CheckIfSingular()) PresentSingularTile();
    }

    public bool CheckIfSingular()
    {
        if (gameObject.transform.childCount == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void PresentSingularTile()
    {
        var child = gameObject.transform.GetChild(0);
        child.transform.position = gameObject.transform.position;
        child.transform.localScale = new Vector3(1, 1, 1);
    }

}
