using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Camera MainCam;
     public GameObject player;
 
     private void Update()
     {
        MainCam.transform.position = new Vector3
        (
            player.transform.position.x, 
            0, 
            MainCam.transform.position.z
        );
     }
}
