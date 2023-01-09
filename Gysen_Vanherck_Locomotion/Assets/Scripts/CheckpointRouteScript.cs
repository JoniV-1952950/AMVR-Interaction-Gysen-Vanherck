using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointRouteScript : MonoBehaviour
{
    [SerializeField]
    private GameObject checkpointPrefab;

    private GameObject checkpointObject;
    private int currentCheckpoint = 0;

    private void Start() 
    {
        Transform checkpoint = gameObject.transform.GetChild(currentCheckpoint);
        checkpointObject = Instantiate(checkpointPrefab, checkpoint, false);
        checkpointObject.transform.localPosition = new Vector3(0, 0, 0);
    }

    public void checkpointTrigger()
    {
        GameObject thisCheckpoint = gameObject.transform.GetChild(currentCheckpoint).gameObject;
        thisCheckpoint.SetActive(false);

        if (currentCheckpoint < gameObject.transform.childCount - 1)
        {
            currentCheckpoint++;
            Transform checkpoint = gameObject.transform.GetChild(currentCheckpoint);
            checkpointObject.transform.parent = checkpoint;
            checkpointObject.transform.localPosition = new Vector3(0, 0, 0);
            if(currentCheckpoint == gameObject.transform.childCount - 1)
                checkpointObject.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
        }
        else
            Destroy(checkpointObject); 
    }
}
