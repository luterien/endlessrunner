using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KasideGameManager : MonoBehaviour {

    public static KasideGameManager Instance;

    AudioSource audioSource;

    public float startAfterSeconds = 2f;
    public float deathTimer = 2f;
    float counter;

    public GameObject deathUI;

    PlayerController player;

    public enum GameState {
        Starting,
        Playing,
        Dead
    }
    public GameState state = GameState.Starting;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }

	// Use this for initialization
	void Start () {
        counter = startAfterSeconds;
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		
        switch (state) {

            case GameState.Starting:

                if (counter > 0f) {
                    counter -= Time.deltaTime;
                } else {
                    StartGame();
                }

                break;

            case GameState.Dead:

                if (counter > 0f) {
                    counter -= Time.deltaTime;
                } else {
                    DisplayDeathUI();
                }

                break;

        }

	}

    void StartGame() {
        state = GameState.Playing;
        player.SendMessage("OnGameStart");
    }

    void OnPlayerDead() {
        state = GameState.Dead;
        counter = deathTimer;
    }

    void GoBackToMenu() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("menu");
    }

    void DisplayDeathUI() {
        deathUI.SetActive(true);
    }

    public bool Active() {
        return state != GameState.Dead;
    }

    public void PlaySound() {
        audioSource.Play();
    }

    public bool IsPlaying() {
        return state == GameState.Playing;
    }

}
