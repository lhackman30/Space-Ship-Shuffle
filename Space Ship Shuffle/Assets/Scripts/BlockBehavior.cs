using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehavior : MonoBehaviour
{
    [SerializeField]
    private enum BlockType
    {
        Breakable,
        Unbreakables
    };
    private void OnTriggerEnter(Collider other)
    {
        //if(other.CompareTag("Player"))
            
    }
}
