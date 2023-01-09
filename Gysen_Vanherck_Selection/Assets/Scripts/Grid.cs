using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField]
    private int gridSize = 6;
    [SerializeField]
    private float scaleFactor;
    [SerializeField]
    private float offset;
    [SerializeField]
    private string layer;

    private List<GameObject> collisions = new List<GameObject>();

    private void refreshGrid()
    {
        int i = 0;
        float width = GetComponent<RectTransform>().sizeDelta.x - offset;
        float height = GetComponent<RectTransform>().sizeDelta.y - offset;
        foreach (Transform child in transform)
        {
            child.localPosition = new Vector3((width/gridSize)*(i%gridSize) - width/2 + child.localScale.x/2, -(height/gridSize)*(i/gridSize) + height/2 - child.localScale.y/2, 0);
            i++;
        }
    }

    public void addToGrid(GameObject go)
    {
        gameObject.SetActive(true);

        var clone = Instantiate(go, gameObject.transform, false);
        clone.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
        clone.layer = LayerMask.NameToLayer(layer);
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
