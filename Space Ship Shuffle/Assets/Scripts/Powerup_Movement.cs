using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_Movement : MonoBehaviour
{   
    [SerializeField]
    private Vector3 originalPos;
    private float rotationRate = 1;
    private float bounceRate = 0.05f;
    private bool movingUp = false;
    private float maxBounce = 1;
    private float deltaTime = 0.5f;
    private void Start()
    {
        originalPos = this.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (movingUp)
        {
            this.transform.Translate(new Vector3(0, 0, this.transform.position.z + bounceRate));
        }
        else
        {
            this.transform.Translate(new Vector3(0, 0, this.transform.position.z - bounceRate));
        }
        this.transform.Rotate(Vector3.forward, rotationRate);
        deltaTime += Time.deltaTime;
        if( deltaTime > 1)
        {
            movingUp = movingUp ? false: true;
            deltaTime = 0.0f;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        string tag = this.gameObject.tag;
        switch (tag)
        {
            case "speedBoost":
                other.GetComponent<Movement>().ChangeSpeed(5);
                Destroy(this.gameObject);
                break;
            case "shieldBoost":
                other.GetComponent<Stats>().ChangeHealth(20);
                Destroy(this.gameObject);
                break;
            case "lifeBoost":
                other.GetComponent<Stats>().ChangeLives(1);
                Destroy(this.gameObject);
                break;
            case "scoreBoost":
                break;
        }
    }
}
