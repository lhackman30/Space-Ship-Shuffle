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
    private bool bulletTimer = false;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private GameObject BlockManager;
    private BlockManager block_Manager;

    private enum CameraState
    {
        Horizontal,
        Vertical
    };
    private CameraState CS = CameraState.Vertical;

    private void Start()
    {
        movement = this.GetComponent<Movement>();
        block_Manager = BlockManager.GetComponent<BlockManager>();
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
                //lastZposition = transform.position.z;
                //transform.position = new Vector3(transform.position.x, lastYposition, 0);
                block_Manager.deactivateRange("Z", this.transform.position.z - 1);
                mainCamera.transform.LookAt(this.transform);
                CS = CameraState.Vertical;
            }
            else //move camera to y plane
            {
                //move camera into position
                mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y + cameraDistance, transform.position.z);

                //store Y position and move ship to z plane
                //lastYposition = transform.position.y;
                //transform.position = new Vector3(transform.position.x, movement.GetMaxHeight(), lastZposition);
                block_Manager.deactivateRange("Y", this.transform.position.y + 1);
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (bulletTimer) return;
            GameObject.Instantiate(bulletPrefab, transform.position, transform.rotation);
            StartCoroutine("bTimer");

        }
    }
    IEnumerator bTimer()
    {
        bulletTimer = true;
        yield return new WaitForSeconds(0.2f);
        bulletTimer = false;
    }
}
