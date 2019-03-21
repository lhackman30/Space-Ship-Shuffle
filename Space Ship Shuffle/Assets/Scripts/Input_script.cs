using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_script : MonoBehaviour
{
    public bool cameraIsRotating = false;
    [SerializeField]
    private Camera mainCamera;
    private Movement movement;
    private float cameraDistance = 45;
    private float xCameraDelta = 40.0f;

    private bool CameraSwitchTimer = false;
    private bool bulletTimer = false;
    [SerializeField]
    private AudioClip laser;
    [SerializeField]
    private AudioClip move;
    [SerializeField]
    private AudioClip death;
    [SerializeField]
    private AudioClip screenRotation;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private GameObject cameraPositionZ;
    [SerializeField]
    private GameObject cameraPositionY;
    [SerializeField]
    private GameObject cameraFocusPoint;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private GameObject BlockManager;
    private BlockManager block_Manager;

    private enum CameraState
    {
        LookingUpZ, //camera is looking up the Z axis
        LookingDownY  //camera is looking down the y axis
    };
    private CameraState CS = CameraState.LookingUpZ;

    private void Start()
    {
        movement = this.GetComponent<Movement>();
        block_Manager = BlockManager.GetComponent<BlockManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraIsRotating) return;   //is the camera currently rotating around the player?
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (movement.GetIsMoving()) return; //is the ship in the middle of a move? 

            if( CS == CameraState.LookingDownY )//move camera to z plane
            {
                block_Manager.deactivateRange("Z", this.transform.position.z - 1);
                StartCoroutine("RotateCameraDown");
                CS = CameraState.LookingUpZ;
            }
            else //move camera to y plane
            {
                block_Manager.deactivateRange("Y", this.transform.position.y + 1);
                StartCoroutine("RotateCameraUp");
               
                CS = CameraState.LookingDownY;
            }
        } 
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if( CS == CameraState.LookingUpZ)
            {
                if (WillCollideWithObject(Vector3.up)) return;
                movement.Move(1, "Vertical");
               
            }
            else if (CS == CameraState.LookingDownY)
            {
                if (WillCollideWithObject(Vector3.forward)) return;
                movement.Move(1, "Horizontal");
            }
            audioSource.PlayOneShot(move);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (CS == CameraState.LookingUpZ)
            {
                if (WillCollideWithObject(Vector3.down)) return;
                movement.Move(-1, "Vertical");
            }
            else if (CS == CameraState.LookingDownY)
            {
                if (WillCollideWithObject(Vector3.back)) return;
                movement.Move(-1, "Horizontal");
            }
            audioSource.PlayOneShot(move);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (bulletTimer) return;
            GameObject.Instantiate(bulletPrefab, transform.position, transform.rotation);
            audioSource.PlayOneShot(laser);
            StartCoroutine("bTimer");
        }
        if(CS == CameraState.LookingDownY)
        {
            mainCamera.transform.position = cameraPositionY.transform.position;
        }
        else
        {
            mainCamera.transform.position = cameraPositionZ.transform.position;
        }
       
    }
    IEnumerator bTimer()
    {
        bulletTimer = true;
        yield return new WaitForSeconds(0.2f);
        bulletTimer = false;
    }
    //slerp camera into new position
    IEnumerator RotateCameraUp()
    {

        cameraIsRotating = true;
        movement.setIsCameraMoving(true);
        audioSource.PlayOneShot(screenRotation, 0.5f);
        for(float i = 1; i <= 10; i+=1)
        {
            //Debug.Log("RotateCameraUp : i = " + i = " : i/10 = " + i / 10);
            mainCamera.transform.position = Vector3.Lerp(cameraPositionZ.transform.position, cameraPositionY.transform.position, i / 10);
            mainCamera.transform.LookAt(cameraFocusPoint.transform);
            yield return new WaitForSeconds(0.03f);
        }
        yield return new WaitForSeconds(0.2f);
        cameraIsRotating = false;
        movement.setIsCameraMoving(false);
    }
    IEnumerator RotateCameraDown()
    {
        cameraIsRotating = true;
        movement.setIsCameraMoving(true);
        audioSource.PlayOneShot(screenRotation, 0.5f);
        for (float i = 1; i <= 10; i += 1)
        {
            //Debug.Log("RotateCameraUp : i = " + i = " : i/10 = " + i / 10);
            mainCamera.transform.position = Vector3.Lerp(cameraPositionY.transform.position, cameraPositionZ.transform.position, i / 10);
            mainCamera.transform.LookAt(cameraFocusPoint.transform);
            yield return new WaitForSeconds(0.03f);
        }
        yield return new WaitForSeconds(0.2f);
        cameraIsRotating = false;
        movement.setIsCameraMoving(false);
    }
    private bool WillCollideWithObject(Vector3 direction)
    {
        RaycastHit hit;
        if( Physics.Raycast(this.transform.position, direction, out hit, 6)){
            if(hit.collider.CompareTag("Blockage"))
            {
                return true;
            }
        }
        return false;
    }
}
