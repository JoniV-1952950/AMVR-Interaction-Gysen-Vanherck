using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTriggerEntered : MonoBehaviour
{

    public FollowRouteScript frs;

    private void OnTriggerEnter(Collider other)
    {
        frs.boundsTriggerEnter();
    }

    private void OnTriggerExit(Collider other)
    {
        frs.boundsTriggerExit();
    }
}
