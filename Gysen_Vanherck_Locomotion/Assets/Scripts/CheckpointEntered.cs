using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointEntered : MonoBehaviour
{



    private CheckpointRouteScript rs;

    // Start is called before the first frame update
    void Awake()
    {
        rs = gameObject.transform.parent.parent.GetComponent<CheckpointRouteScript>();
    }

    private void OnTriggerEnter()
    {
        rs.checkpointTrigger(); 
    }
}
