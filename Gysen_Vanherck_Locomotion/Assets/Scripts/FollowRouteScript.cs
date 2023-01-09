using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRouteScript : MonoBehaviour
{
    [SerializeField]
    private GameObject followObjectPrefab;
    [SerializeField]
    private float speed = 2;
    [SerializeField]
    private float boundsScale = 3;

    private int currentCheckpoint = 0;
    private Vector3 checkpoint;
    private GameObject followObject;
    private bool move = false;

    // Start is called before the first frame update
    void Start()
    {
        checkpoint = gameObject.transform.GetChild(++currentCheckpoint).position;
        followObject = Instantiate(followObjectPrefab, gameObject.transform.parent, false);
        followObject.transform.position = gameObject.transform.GetChild(0).position; 
        GameObject boundary = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        boundary.transform.parent = followObject.transform;
        boundary.transform.localPosition = new Vector3(0, 0, 0);
        boundary.GetComponent<MeshRenderer>().enabled = false;
        boundary.transform.localScale = new Vector3(boundsScale, boundsScale, boundsScale);
        boundary.GetComponent<SphereCollider>().isTrigger = true;
        boundary.AddComponent<FollowTriggerEntered>().frs = this; 
    }

    public void boundsTriggerEnter()
    {
        move = true;
    }

    public void boundsTriggerExit()
    {
        move = false;
    }

    private void Update()
    {
        if (move)
        {
            followObject.transform.position = Vector3.MoveTowards(followObject.transform.position, checkpoint, speed * Time.deltaTime);
            if (followObject.transform.position == checkpoint)
            {
                if (currentCheckpoint < gameObject.transform.childCount - 1)
                    checkpoint = gameObject.transform.GetChild(++currentCheckpoint).position;
                else
                    checkpoint = followObject.transform.position;
            }
        }
    }
}
