using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_script : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;
    private Movement movement;
    private float lastYposition = 0;
    private float lastZposition = 0;
    private float cameraDistance = 30;

    private bool CameraSwitchTimer = false;

    private enum CameraState
    {
        Horizontal,
        Vertical
    };
    private CameraState CS = CameraState.Vertical;

    private void Start()
    {
        movement = this.GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (movement.GetIsMoving()) return;  
            //if (CameraSwitchTimer) return;

            if( CS == CameraState.Horizontal )//move camera to z plane
            {
                //move camera into position
                mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (-cameraDistance));

                //store z position and move ship to y plane
                lastZposition = transform.position.z;
                transform.position = new Vector3(transform.position.x, lastYposition, 0);

                mainCamera.transform.LookAt(this.transform);
                CS = CameraState.Vertical;
            }
            else //move camera to y plane
            {
                //move camera into position
                mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y + cameraDistance, transform.position.z);

                //store Y position and move ship to z plane
                lastYposition = transform.position.y;
                transform.position = new Vector3(transform.position.x, movement.GetMaxHeight(), lastZposition);

                mainCamera.transform.LookAt(this.transform);
                CS = CameraState.Horizontal;
            }
        } 
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if( CS == CameraState.Vertical)
            {
                movement.Move(1, "Vertical");
            }
            else if (CS == CameraState.Horizontal)
            {
                movement.Move(1, "Horizontal");
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (CS == CameraState.Vertical)
            {
                movement.Move(-1, "Vertical");
            }
            else if (CS == CameraState.Horizontal)
            {
                movement.Move(-1, "Horizontal");
            }
        }
    }
}
