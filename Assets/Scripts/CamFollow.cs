using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{

    [SerializeField]
    private Transform anchor;
    [SerializeField]
    private Vector3 offset;

    private void Update()
    {

        transform.position = new Vector3(anchor.position.x, 0, anchor.position.z) + offset;

    }

}
