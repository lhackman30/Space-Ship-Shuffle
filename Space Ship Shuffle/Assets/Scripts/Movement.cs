using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float maxHeight = 36.0f;
    private float minHeight = 0;
    private bool isMoving = false;
    [SerializeField]
    private float moveStep = 6;
    private int direction;
    private string axis;
    private float shipVelocity = 10.0f;
    private float shipAcceleration = 1;
    private float maxSpeed = 50.0f;
    [SerializeField]
    private Text Score;
    [SerializeField]
    private Text Speed;
    private int score;
    private int scoreIncreaseRate = 2;
    private bool isCameraMoving = false;
    private Vector3 startingPosition;
    [SerializeField]
    private Stats stats;
    [SerializeField]
    private BlockManager blockManager;

    private void Start()
    {
        startingPosition = this.transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        if (isCameraMoving) return;
        RaycastHit hit;
        if(Physics.Raycast(this.transform.position, Vector3.right, out hit, 3)){
            if(hit.collider.CompareTag("Blockage"))
            {
                stats.ChangeLives(-1);
                blockManager.ActivateAll();
            }
        }
        this.transform.Translate(shipVelocity * shipAcceleration * Time.deltaTime, 0, 0);
        score += Mathf.RoundToInt(shipVelocity * scoreIncreaseRate * Time.deltaTime);
        Score.text = score.ToString();
        Speed.text = (shipVelocity * 10.0f).ToString();
    }
    public void Move( int dir, string ax )
    {
        if (isMoving) return;
        if (ax == "Vertical" && dir == 1 && transform.position.y >= maxHeight) return;
        if (ax == "Vertical" && dir == -1 && transform.position.y <= minHeight) return;
        if (ax == "Horizontal" && dir == 1 && transform.position.z >= maxHeight) return;
        if (ax == "Horizontal" && dir == -1 && transform.position.z <= minHeight) return;

        direction = dir;
        axis = ax;

        StartCoroutine("MoveShip");
    }
    public bool GetIsMoving()
    {
        return isMoving;
    }
    public float GetMaxHeight()
    {
        return maxHeight;
    }
    public void ChangeSpeed( float speedBoost)
    {
        shipVelocity += speedBoost;
        if(shipVelocity <= 0)
        {
            shipVelocity = 5;
        }
        if(shipVelocity >= maxSpeed)
        {
            shipVelocity = maxSpeed;
        }
    }
    public void setIsCameraMoving(bool isCameraMoving)
    {
        this.isCameraMoving = isCameraMoving;
    }
    IEnumerator MoveShip()
    {
        isMoving = true;
        float nextStep = (6 / moveStep) * direction;
        for ( int i = 0; i < moveStep; i++)
        {
            
            if( axis == "Horizontal")
            {
                transform.Translate(0, 0, nextStep);
            }
            else if ( axis == "Vertical")
            {
                transform.Translate(0, nextStep, 0);

            }
            yield return new WaitForSecondsRealtime(0.001f);
        }
        isMoving = false;
    }
    public Vector3 getStartingPostition()
    {
        return startingPosition;
    }
}
