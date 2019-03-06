using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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



    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(shipVelocity * shipAcceleration * Time.deltaTime, 0, 0);
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
            yield return new WaitForSecondsRealtime(0.000001f);
        }
        isMoving = false;
    }
}
