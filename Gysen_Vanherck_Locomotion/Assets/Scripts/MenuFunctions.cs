using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuFunctions : MonoBehaviour
{
    [SerializeField]
    private GameObject uiHelper;
    private bool raysActive = false;

    void Update()
    {
        if (OVRInput.GetUp(OVRInput.RawButton.A)) {
            raysActive = !raysActive;
            uiHelper.SetActive(raysActive);
        }

    }
    
    public void resetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
