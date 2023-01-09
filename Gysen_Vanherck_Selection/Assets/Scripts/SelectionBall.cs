using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SelectionBall : MonoBehaviour
{
    [SerializeField]
    private float slowFactor = 0.5f;

    [SerializeField]
    private GameObject bubbelGrid;

    // Update is called once per frame
    void Update()
    {
        var joystickInput = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick);
        if (Math.Abs(joystickInput.x) > Math.Abs(joystickInput.y))
        {
            float scaleSlowFactor = 1f * slowFactor;
            if(gameObject.transform.localScale.x < 1f)
                scaleSlowFactor = gameObject.transform.localScale.x * slowFactor;
            if (gameObject.transform.localScale.x + joystickInput.x*scaleSlowFactor > 0)
                gameObject.transform.localScale += new Vector3(joystickInput.x, joystickInput.x, joystickInput.x) * scaleSlowFactor;
        }
        else
        {
            if(gameObject.transform.localPosition.z + joystickInput.y*slowFactor > 0)
                gameObject.transform.localPosition += new Vector3(0, 0, joystickInput.y * slowFactor);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        bubbelGrid.GetComponent<Grid>().addToGrid(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        bubbelGrid.GetComponent<Grid>().removeFromGrid(other.gameObject);
    }
}
