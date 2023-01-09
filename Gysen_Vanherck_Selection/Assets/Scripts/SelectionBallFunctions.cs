using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class SelectionBallFunctions: MonoBehaviour
{
    [SerializeField]
    private GameObject selectionBall;

    private bool selectionBallActive = true;

    [SerializeField]
    private GameObject bubbelGrid;

    [SerializeField]
    private GameObject OVRCameraRig;

    [SerializeField]
    private GameObject expandSpawner;

    [SerializeField]
    GameObject expandGrid;

    [SerializeField]
    private GameObject beans;

    [SerializeField]
    private Material targetMat;
    [SerializeField]
    private Material goodMat;

    private GameObject expandCanvas;

    private static string pathToFile = "./timings/nils";
    private static string fileExtension = ".txt";

    private static int amountOfSelections = 0;
    private static int maxSelections = 16;

    private int targetID;
    public bool objectPressed = false;
    void Start()
    {
        expandCanvas = expandGrid.transform.parent.gameObject;
        targetID = Random.Range(0,beans.transform.childCount);
        foreach (Transform child in beans.transform)
        {
            if (child.gameObject.GetComponent<ItemID>().uniqueID == targetID)
            {
                child.gameObject.GetComponent<MeshRenderer>().material = targetMat;
            }
        }
    }

    public static void AddEventTriggerListener(EventTrigger trigger,
                                           EventTriggerType eventType,
                                           System.Action<BaseEventData> callback)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = eventType;
        entry.callback = new EventTrigger.TriggerEvent();
        entry.callback.AddListener(new UnityEngine.Events.UnityAction<BaseEventData>(callback));
        trigger.triggers.Add(entry);
    }



    void EnterTrigger(BaseEventData eventData)
    {
        PointerEventData pointerEventData = (PointerEventData)eventData;
    }

    void ExitTrigger(BaseEventData eventData)
    {
        PointerEventData pointerEventData = (PointerEventData)eventData;
        pointerEventData.pointerEnter.GetComponent<MeshRenderer>().material = targetMat;
    }

    void ClickTrigger(BaseEventData eventData)
    {
        PointerEventData pointerEventData = (PointerEventData)eventData;
        objectPressed = true;
        pointerEventData.pointerEnter.GetComponent<MeshRenderer>().material = goodMat;
        pointerEventData.pointerEnter.GetComponent<EventTrigger>().triggers.RemoveRange(0, pointerEventData.pointerEnter.GetComponent<EventTrigger>().triggers.Count);
    }

    void Update()
    {
        if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger))
        {
            selectionBallActive = !selectionBallActive;
            if (!selectionBallActive)
            {
                expandCanvas.transform.position = new Vector3(expandSpawner.transform.position.x,1.5f, expandSpawner.transform.position.z);
                expandCanvas.transform.rotation = expandSpawner.transform.rotation;
                
                selectionBall.SetActive(false);
                OVRCameraRig.GetComponent<OVRPhysicsRaycaster>().enabled = true;
                foreach (Transform child in bubbelGrid.transform)
                {
                    expandGrid.GetComponent<Grid>().addToGrid(child.gameObject);
                    bubbelGrid.GetComponent<Grid>().removeFromGrid(child.gameObject);
                }
                foreach (Transform child in expandGrid.transform)
                {
                    if(child.gameObject.GetComponent<ItemID>().uniqueID == targetID)
                    {
                        EventTrigger trigger = child.gameObject.GetComponent<EventTrigger>();
                        trigger.triggers.RemoveRange(0, trigger.triggers.Count);
                        AddEventTriggerListener(trigger, EventTriggerType.PointerEnter, EnterTrigger);
                        AddEventTriggerListener(trigger, EventTriggerType.PointerExit, ExitTrigger);
                        AddEventTriggerListener(trigger, EventTriggerType.PointerClick, ClickTrigger);
                    }
                }
                beans.SetActive(false);

            }
            else
            {
                Invoke("disableOVRPhysicsRayCaster", 0.2f);
                Invoke("resetScene", 3f);               
            }
        }
    }

    private void disableOVRPhysicsRayCaster()
    {
        OVRCameraRig.GetComponent<OVRPhysicsRaycaster>().eventMask = 0;
    }

    private void resetScene()
    {
        string data = (Time.timeSinceLevelLoad-3).ToString();
        writeToFFile(pathToFile + "_" + SceneManager.GetActiveScene().name + fileExtension, data);
        ItemID.currentUniqueID = 0;

        if (++amountOfSelections < maxSelections)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        else
        {
            amountOfSelections = 0;
            if (SceneManager.GetActiveScene().name == "selectionball_medium")
            {
                SceneManager.LoadScene("Assets/Scenes/basicselection_far.unity");
            }
            else if (SceneManager.GetActiveScene().name == "selectionball_short")
            {
                SceneManager.LoadScene("Assets/Scenes/basicselection_medium.unity");
            }
            else if (SceneManager.GetActiveScene().name == "selectionball_far")
            {
                SceneManager.LoadScene("Assets/Scenes/basicselection_short.unity");
            }
            else if (SceneManager.GetActiveScene().name == "basicselection_far")
            {
                SceneManager.LoadScene("Assets/Scenes/selectionball_short.unity");
            }
            else if (SceneManager.GetActiveScene().name == "basicselection_medium")
            {
                SceneManager.LoadScene("Assets/Scenes/selectionball_far.unity");
            }
            else if (SceneManager.GetActiveScene().name == "basicselection_short")
            {
                SceneManager.LoadScene("Assets/Scenes/selectionball_medium.unity");
            }
        }
    }

    private void writeToFFile(string path, string data)
    {
        Debug.Log(objectPressed);
        if (objectPressed)
            data += ",hit";
        else
            data += ",miss";
        // This text is added only once to the file.
        if (!File.Exists(path))
        {
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(path))
            {
            }
        }
        else
        {
            // This text is always added, making the file longer over time
            // if it is not deleted.
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(data);
            }
        }
    }
}
