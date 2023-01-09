using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemID : MonoBehaviour
{
    public int uniqueID = -1;
    public static int currentUniqueID = 0;

    void Awake()
    {
        if (uniqueID < 0)
        {
            uniqueID = currentUniqueID;
            currentUniqueID++;
        }
    }

}
