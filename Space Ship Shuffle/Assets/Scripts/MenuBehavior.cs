using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuBehavior : MonoBehaviour
{
    [SerializeField]
    private Button start;
    [SerializeField]
    private Scene startGame;

    // Start is called before the first frame update
    void Start()
    {
        start.onClick.AddListener(StartGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void StartGame()
    {
        Debug.Log("Start!!");
        SceneManager.LoadScene("Game");
    }
}
