using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TargetBehaviour : MonoBehaviour
{
    [SerializeField]
    private string filePath;
    [SerializeField]
    private string nextLevel;
    private string user = ", user4";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<OVRGrabbable>().isGrabbed)
        {
            Invoke("startNextLevel",3f);
        }
    }

    void startNextLevel()
    {
        string data = (Time.timeSinceLevelLoad - 3).ToString();
        writeToFFile(data);
        if (nextLevel != "done")
            SceneManager.LoadScene(nextLevel, LoadSceneMode.Single);
        
    }

    private void writeToFFile(string data)
    {

        // This text is added only once to the file.
        if (!File.Exists(filePath))
        {
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(filePath))
            {
                sw.WriteLine(data + user);
            }
        }
        else
        {
            // This text is always added, making the file longer over time
            // if it is not deleted.
            using (StreamWriter sw = File.AppendText(filePath))
            {
                sw.WriteLine(data + user);
            }
        }
    }
}
