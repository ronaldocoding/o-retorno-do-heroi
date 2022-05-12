using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Camera MainCam;
    public GameObject player;
    private float playerPosX;
    
 
    private void Update()
    {
        playerPosX = player.transform.position.x;
        if(playerPosX >= 0.05 && playerPosX <= 318)
        {
            MainCam.transform.position = new Vector3
            (
                player.transform.position.x, 
                0, 
                MainCam.transform.position.z
            );
        }
    }
}
