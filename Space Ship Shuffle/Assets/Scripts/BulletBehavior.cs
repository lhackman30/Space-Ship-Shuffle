using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed = 50;
    [SerializeField]
    private float bulletacceleration = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bulletSpeed = bulletSpeed + bulletacceleration;
        this.transform.Translate(new Vector3(bulletSpeed * Time.deltaTime, 0, 0));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) return;
        Destroy(this.gameObject);
    }
   
}
