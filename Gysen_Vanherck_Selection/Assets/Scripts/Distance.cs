using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distance : MonoBehaviour
{

    [SerializeField]
    GameObject bean;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Vector3.Distance(bean.transform.position, transform.position));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
