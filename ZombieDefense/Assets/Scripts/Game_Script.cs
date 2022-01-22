using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Game_Script : MonoBehaviour
{
    public GameObject player;
    public GameObject zombiePrefab;
    public GameObject pauseUI;
    public GameObject gameUI;

    public TMP_Text waveText; // UI for Wave Count
    public TMP_Text newWaveText; //UI for new Wave
    public TMP_Text killText; // UI for Kill Count

    private float beginWave = 4f; // Seconds before the next wave begins
    private float waveLevel = 1; // Level for Wave
    public static int killCounter = 0; // Counter for Kills, used to track when wave level should increase
    private int baseKills = 0; // Logic needed to start the secondary wave

    private bool gameStart = false; // Bool to initiate the first wave
    private bool waveCooldown = false; // Bool for wave cooldown

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {   
        // INCREMENTS KILL COUNTER, SHOULD BE DONE ONCE PLAYER DESTROYS ZOMBIE
        if (Input.GetKeyDown("escape")) {
            pauseUI.SetActive(!pauseUI.activeSelf);
            gameUI.SetActive(!gameUI.activeSelf);
            if (pauseUI.activeSelf) {
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else {
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        // INITIATE THE FIRST WAVE
        if (!gameStart) {
            beginWave -= Time.deltaTime;
            if (beginWave < 0) {
                print("Game Begin");
                createWave(waveLevel);
                killText.text = "Kill Counter: " + killCounter;
                beginWave = 4f;
                gameStart = true;
            }
        }

        // MOVES TO NEXT WAVE
        if (killCounter == (baseKills + (5 * waveLevel))) {
            print("WAVE ON COOLDOWN");
            beginWave -= Time.deltaTime;
            newWaveText.text = "New Wave arriving in ... " + beginWave;
            if (beginWave < 0) {
                print("New Wave Begin");
                beginWave = 4f;
                baseKills = baseKills + (int)(5 * waveLevel);
                waveCooldown = true;
            }

            if (waveCooldown == true) {
                waveLevel++;
                createWave(waveLevel);
                newWaveText.text = "";
                waveCooldown = false;
            }
        }

        // INCREMENTS KILL COUNTER, SHOULD BE DONE ONCE PLAYER DESTROYS ZOMBIE
        if (Input.GetKeyDown("q")) {
            killCounter++;
            killText.text = "Kill Counter: " + killCounter;
        }
    }

    // CREATES EACH WAVE, ZOMBIE SPAWN = 5 * WAVELEVEL
    public void createWave(float waveLevel) {
        waveText.text = "Wave " + waveLevel;

        for (int i = 0; i < (5 * waveLevel); i++) {
            var spawnPoint = Random.Range(1, 5);
            switch(spawnPoint) {
                case 1:
                    Instantiate(zombiePrefab, new Vector3(Random.Range(-5, 5), 1.5f, Random.Range(30, 40)), Quaternion.identity);
                    break;

                case 2:
                    Instantiate(zombiePrefab, new Vector3(Random.Range(30, 40), 1.5f, Random.Range(-5, 5)), Quaternion.identity);
                    break;

                case 3:
                    Instantiate(zombiePrefab, new Vector3(Random.Range(-5, 5), 1.5f, Random.Range(-40, -30)), Quaternion.identity);
                    break;

                case 4:
                    Instantiate(zombiePrefab, new Vector3(Random.Range(-40, -30), 1.5f, Random.Range(-5, 5)), Quaternion.identity);
                    break;

                default:
                    print("Broke for some reason");
                    break;
            }
        }
    }

    public void resumeGame() {
        pauseUI.SetActive(!pauseUI.activeSelf);
        gameUI.SetActive(!gameUI.activeSelf);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void quitGame() {
        SceneManager.LoadScene("MenuScene");
    }
}
