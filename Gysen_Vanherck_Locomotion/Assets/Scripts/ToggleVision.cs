using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ToggleVision : MonoBehaviour
{
    private UnityEngine.Rendering.Universal.UniversalAdditionalCameraData cameraData;
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private GameObject postProcessing;
    private int rendererIndex = 0;
    public Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        cameraData = camera.GetComponent<UnityEngine.Rendering.Universal.UniversalAdditionalCameraData>();
        target.GetComponent<Light>().enabled = false;
        postProcessing.SetActive(false);
        cameraData.SetRenderer(0);
    }

    void Vision()
    {
        cameraData = camera.GetComponent<UnityEngine.Rendering.Universal.UniversalAdditionalCameraData>();
        if (rendererIndex == 0)
        {
            target.GetComponent<Light>().enabled = true;
            postProcessing.SetActive(true);
            cameraData.SetRenderer(1);
            rendererIndex = 1;
        }
        else
        {
            target.GetComponent<Light>().enabled = false;
            postProcessing.SetActive(false);
            cameraData.SetRenderer(0);
            rendererIndex = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetUp(OVRInput.RawButton.B))
        {
            Invoke("Vision", 0.1f);

        }
    }
}
