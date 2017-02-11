using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Controller : MonoBehaviour {

    public string run;
    public string jump;
    public string idle;
    public string fall;
    public string die;

    float jumpSpeed = 350f;

    Vector3 movement;
    Rigidbody rb;
    Stats stats;

    float h = 0;

    bool switchingLane = false;

    public float laneDistance = 0.3f;

    public Lane targetLane = null;

    public enum PlayerState {
        Falling,
        Idle,
        Running,
        Jumping,
        Dead,
        SwitchLane
    }
    public PlayerState state = PlayerState.Falling;

    // lanes
    [SerializeField]
    public List<Lane> lanes = new List<Lane>();
    public int activeLaneIndex = 1;

    void Start() {

        targetLane = lanes[activeLaneIndex];

        rb = GetComponent<Rigidbody>();
        stats = GetComponent<Stats>();

        Load();

    }

    abstract protected void Load();

    void Update() {

        if (KasideGameManager.Instance.IsPlaying() && state != PlayerState.Dead) {

            switch (state) {

                case PlayerState.Running:

                    Move();

                    if (!switchingLane) {
                        CheckForLaneChangeRequest();
                    } else {
                        CheckIfLaneReached();
                    }

                    if (Input.GetKeyDown(KeyCode.Space)) {
                        Jump();
                    }
                    break;

                case PlayerState.Jumping:
                    MoveWhileJumping();
                    break;

                case PlayerState.Falling:
                    MoveWhileJumping();
                    break;

            }

        }

    }

    void Jump() {
        rb.velocity = rb.velocity + new Vector3(0, jumpSpeed * Time.deltaTime, 0f);
        ChangeState(PlayerState.Jumping);
    }

    public void StopJump() {
        ChangeState(PlayerState.Running);
    }

    void CheckIfLaneReached() {
        if (targetLane != null && Mathf.Abs(targetLane.x - transform.position.x) < laneDistance) {
            activeLaneIndex = lanes.IndexOf(targetLane);
            switchingLane = false;
        }
    }

    void CheckForLaneChangeRequest() {
        /* 
        if (Input.touchCount == 0) return;
        if (Input.GetTouch(0).deltaPosition.x > 0) {
            Debug.Log("CheckForLaneChangeRequest");
            SetTargetLane(1);
        } else if (Input.GetTouch(0).deltaPosition.x < 0) {
            Debug.Log("CheckForLaneChangeRequest");
            SetTargetLane(-1);
        }*/
        h = Input.GetAxis("Horizontal");
        if (h > 0) {
            SetTargetLane(1);
        } else if (h < 0) {
            SetTargetLane(-1);
        }
    }

    void SetTargetLane(int i) {
        switchingLane = true;
        try {
            targetLane = lanes[activeLaneIndex + i];
            activeLaneIndex = activeLaneIndex + i;
        } catch (System.ArgumentOutOfRangeException) {
            Debug.Log("SetTargetLane out of range.");
        }
    }

    void Move() {
        transform.position = Vector3.Lerp(
            transform.position,
            new Vector3(targetLane.x, transform.position.y, transform.position.z + Time.deltaTime * stats.verticalSpeed),
            Time.deltaTime * 10
        );
    }

    void MoveWhileJumping() {
        transform.position = Vector3.Lerp(
            transform.position,
            new Vector3(transform.position.x, transform.position.y, transform.position.z + Time.deltaTime * stats.verticalSpeed * 2/3),
            Time.deltaTime * 10
        );
    }

    void MoveTowards() {
        if (targetLane != null) {
            transform.position = Vector3.Lerp(
                transform.position,
                new Vector3(targetLane.x, transform.position.y, transform.position.z + Time.deltaTime * stats.verticalSpeed),
                Time.deltaTime * 10
            );
        }

    }

    abstract protected void Animate();

    void OnCollisionEnter(Collision collision) {
        if (KasideGameManager.Instance.state == KasideGameManager.GameState.Playing) {
            if (state == PlayerState.Jumping && collision.gameObject.tag == "Ground") {
                StopJump();
            }
        } else if (KasideGameManager.Instance.state == KasideGameManager.GameState.Starting) {
            ChangeState(PlayerState.Idle);
        }
    }

    void OnGameStart() {
        ChangeState(PlayerState.Running);
    }

    void TakeDamage() {
        ChangeState(PlayerState.Dead);
        KasideGameManager.Instance.SendMessage("OnPlayerDead");
    }

    void ChangeState(PlayerState newState) {
        state = newState;
        Animate();
    }

    public void Die() {
        
    }

}
