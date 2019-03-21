using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndMenuBehavior : MonoBehaviour
{

    [SerializeField]
    private Button restart;
    [SerializeField]
    private Button quit;
    // Start is called before the first frame update
    void Start()
    {
        restart.onClick.AddListener(RestartGame);
    }
    private void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }
    private void QuitGame()
    {
        Application.Quit();
    }
    
}
