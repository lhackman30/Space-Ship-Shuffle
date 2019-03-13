using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehavior : MonoBehaviour
{
    [SerializeField]
    private enum BlockType
    {
        Breakable,
        Unbreakable
    };
    [SerializeField]
    private BlockType blocktype;
    [SerializeField]
    private GameObject bm;
    private BlockManager BM;

    private void Start()
    {
        BM = bm.GetComponent<BlockManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered : " + other.tag + " : " + blocktype);
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Stats>().ChangeHealth(-10);
        }
        if (other.CompareTag("bullet") && blocktype == BlockType.Breakable)
        {
            //Debug.Log("Breakable!");
            //BM.removeBlock( this.gameObject );
            Destroy(this.gameObject);
        }
    }
}
