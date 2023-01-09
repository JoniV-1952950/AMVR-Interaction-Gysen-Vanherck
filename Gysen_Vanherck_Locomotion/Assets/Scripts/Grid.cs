using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField]
    private int gridSize = 6;


    private List<GameObject> collisions = new List<GameObject>();

    private void refreshGrid()
    {
        int i = 0;
        float width = GetComponent<RectTransform>().sizeDelta.x - 50;
        float height = GetComponent<RectTransform>().sizeDelta.y - 50;
        foreach (Transform child in transform)
        {
            child.localPosition = new Vector3((width/gridSize)*(i%gridSize) - width/2, -(height/gridSize)*(i/gridSize) + height/2, 0);
            i++;
        }
    }

    public void addToGrid(GameObject go)
    {
        Debug.Log(GetComponent<RectTransform>().sizeDelta);
        gameObject.SetActive(true);

        var clone = Instantiate(go, gameObject.transform, false);
        clone.transform.localScale = new Vector3(20, 20, 20);
        clone.layer = LayerMask.NameToLayer("UI");
        collisions.Add(clone);
        refreshGrid();
    }

    public void removeFromGrid(GameObject go)
    {
        foreach(Transform t in transform)
        {
            if(go.GetComponent<ItemID>().uniqueID == t.gameObject.GetComponent<ItemID>().uniqueID)
            {
                collisions.Remove(t.gameObject);
                Destroy(t.gameObject);
                break;
            }
        }
        refreshGrid();
        if (collisions.Count <= 0)
        {
            gameObject.SetActive(false);
        }
    }
   
}
