using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public List<GameObject> blockList = new List<GameObject>();

    private void Start()
    {
        foreach( GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
        {
            if (go.CompareTag("block"))
            {
                blockList.Add(go);
            }
        }
        ActivateAll();
    }

    public void addBlock( GameObject blockIn)
    {
        blockList.Add(blockIn);
    }

    public void ActivateAll()
    {
        //CleanList();
        foreach( GameObject go in blockList)
        {
            if (go != null)
            {
                go.SetActive(true);
            }
        }
    }
    public void removeBlock( GameObject blockIn)
    {
        //Debug.Log("Removing Block! : " + blockIn.name + " : " + blockList.IndexOf(blockIn.gameObject));
        blockList.Remove(blockIn);
    }
    public void deactivateRange( string axis, float range )
    {
        ActivateAll();
        switch (axis)
        {
            case "Z":
                foreach(GameObject go in blockList)
                {
                    if (go != null)
                    {
                        if (go.transform.position.z < range)
                        {
                            go.SetActive(false);
                        }
                    }
                }
                break;
            case "Y":
                foreach (GameObject go in blockList)
                {
                    if (go != null)
                    {
                        if (go.transform.position.y > range)
                        {

                            go.SetActive(false);
                        }
                    }
                }
                break;
            default:
                Debug.Log("BlockManager: deactivateRange: axis undefined");
                break;

        }
    }
    private void CleanList()
    {
        foreach(GameObject go in blockList)
        {
            if( go == null)
            {
                blockList.Remove(go);
            }
        }
    }
 
}
