using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLabirent : MonoBehaviour
{
   
    public float rotationSpeed = 20f;


    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
