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
    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private AudioClip speedBoostSFX;
    [SerializeField]
    private AudioClip shieldBoostSFX;
    [SerializeField]
    private AudioClip lifeBoostSFX;


    private void Start()
    {
        originalPos = this.transform.position;
    }
    
    //Simple animations for the powerup prefabs. Make them bounce up and down while rotating slowly
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
                source.PlayOneShot(speedBoostSFX, 0.5f);
                other.GetComponent<Movement>().ChangeSpeed(5);
                StartCoroutine("timer");
                break;
            case "shieldBoost":
                source.PlayOneShot(shieldBoostSFX, 0.5f);
                other.GetComponent<Stats>().ChangeHealth(20);
                StartCoroutine("timer");
                break;
            case "lifeBoost":
                source.PlayOneShot(lifeBoostSFX, 0.5f);
                other.GetComponent<Stats>().ChangeLives(1);
                StartCoroutine("timer");
                break;
            case "scoreBoost":
                break;
        }
    }
    IEnumerator timer()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }
}
