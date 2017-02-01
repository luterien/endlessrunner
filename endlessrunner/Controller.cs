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

    public float laneDistance = 0.3f;

    Lane targetLane = null;

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
        
        rb = GetComponent<Rigidbody>();
        stats = GetComponent<Stats>();

        Load();

    }

    abstract protected void Load();

    void FixedUpdate() {

        if (GameManager.Instance.IsPlaying() && state != PlayerState.Dead) {

            switch (state) {

                case PlayerState.Running:
                    Move();
                    CheckForLaneChangeRequest();
                    if (Input.GetKeyDown(KeyCode.Space)) {
                        Jump();
                    }
                    break;

                case PlayerState.SwitchLane:
                    MoveTowards();
                    CheckIfLaneReached();
                    break;

                case PlayerState.Jumping:
                    Move();
                    break;

                case PlayerState.Falling:
                    Move();
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
            ChangeState(PlayerState.Running);
        }
    }

    void CheckForLaneChangeRequest() {
        h = Input.GetAxis("Horizontal");
        if (h > 0) {
            SetTargetLane(1);
        } else if (h < 0) {
            SetTargetLane(-1);
        }
    }

    void SetTargetLane(int i) {
        try {
            targetLane = lanes[activeLaneIndex + i];
            if (targetLane != null) ChangeState(PlayerState.SwitchLane);
        } catch {
            targetLane = null;
        }
    }

    void Move() {
        transform.position = Vector3.Lerp(
            transform.position,
            new Vector3(transform.position.x, transform.position.y, transform.position.z + Time.deltaTime * stats.verticalSpeed),
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
        if (GameManager.Instance.state == GameManager.GameState.Playing) {
            if (state == PlayerState.Jumping && collision.gameObject.tag == "Ground") {
                StopJump();
            }
        } else if (GameManager.Instance.state == GameManager.GameState.Starting) {
            ChangeState(PlayerState.Idle);
        }
    }

    void OnGameStart() {
        ChangeState(PlayerState.Running);
    }

    void TakeDamage() {
        ChangeState(PlayerState.Dead);
        GameManager.Instance.SendMessage("OnPlayerDead");
    }

    void ChangeState(PlayerState newState) {
        state = newState;
        Animate();
    }

    public void Die() {
        
    }

}
