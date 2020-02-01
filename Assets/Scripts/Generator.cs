using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject genObj;
    public int xSize;
    public int ySize;

    public float colliderSize;

    void Start()
    {
        Generate();
    }

    private void Generate()
    {
        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                Vector3 spawnPos = new Vector3(x * colliderSize, y * colliderSize, 0);
                GameObject clone = Instantiate(genObj, spawnPos, Quaternion.identity);
            }
        }
    }
}
