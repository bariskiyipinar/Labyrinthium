using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
  
    private int hiz=5;
   
    void Update()
    {
        transform.Rotate(0,Time.deltaTime*hiz,0);
    }
}
