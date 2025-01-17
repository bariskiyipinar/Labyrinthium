using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Labirent : MonoBehaviour
{
 
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch =Input.GetTouch(0);

            
            if(touch.position.x <Screen.width/2)
            {
                transform.Rotate(0, -45*Time.deltaTime, 0);
            }
            else
            {
                transform.Rotate(0,45*Time.deltaTime, 0);
            }

           
        }
    }


    
}
