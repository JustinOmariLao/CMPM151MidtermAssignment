using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Script : MonoBehaviour
{
    public GameObject menuCam;
    public GameObject menuUI;
    
    private float animCounter = 3f;

    private bool playPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playPressed == true) {
            animCounter -= Time.deltaTime;
            if (animCounter < 0) {
                SceneManager.LoadScene("GameScene");
            }
        }
    }

    public void playGame() {
        menuUI.SetActive(false);
        playPressed = true;
        var camAnim = menuCam.GetComponent<Animator>();
        camAnim.Play("menuCam");
    }

    public void settingsMenu() {
        
    }
    
    public void quitGame() {
        Application.Quit();
    }
}
