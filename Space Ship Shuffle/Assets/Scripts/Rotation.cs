using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    private Vector3 rotationAxis = new Vector3(0, 1, 0);
    private float rotationSpeed = 1.0f;

    void Update()
    {
        this.transform.RotateAround(this.transform.position, rotationAxis, rotationSpeed);
    }
}
