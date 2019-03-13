using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    private int health = 100;
    private int initialHealth = 100;
    private int lives = 3;
    private bool immune = false;
    private MeshRenderer meshRenderer;
    private bool rendering = true;
    [SerializeField]
    private Text Health;
    [SerializeField]
    private Text Lives;
    private void Start()
    {
        meshRenderer = this.gameObject.GetComponent<MeshRenderer>();
    }
    public void ChangeHealth( int delta)
    {
        if (immune) return;
        Debug.Log("Changehealth  : " + health);
        health += delta;
        if( delta < 0 ) StartCoroutine("Immune");
        if (health <= 0)
        {
            ChangeLives(-1);
            health = initialHealth;

        }
        Health.text = health.ToString() + "%";
        Lives.text = lives.ToString();
        
    }
    public void ChangeLives( int delta)
    {
        lives += delta;
        if(lives <= 0)
        {
            EndGame();
        }
        Lives.text = lives.ToString();
    }
    private void EndGame()
    {

    }
    IEnumerator Immune()
    {
        Debug.Log("Immune_Start");
        immune = true;
        for( int i = 0; i < 24; i++)
        {
            if(rendering)
            {
                MeshRenderer[] mr = this.GetComponentsInChildren<MeshRenderer>();
                foreach( MeshRenderer m in mr)
                {
                    m.enabled = false;
                }
                rendering = false;
            }
            else
            {
                MeshRenderer[] mr = this.GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer m in mr)
                {
                    m.enabled = true;
                }
                rendering = true;
            }
            yield return new WaitForSeconds(0.1f);
        }
        immune = false;
        Debug.Log("Immune_Stop");
    }
}
