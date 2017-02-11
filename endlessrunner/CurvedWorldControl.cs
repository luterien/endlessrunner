using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VacuumShaders.CurvedWorld;

public class CurvedWorldControl : MonoBehaviour {

    public enum State {
        Wait,
        Targeting
    }
    public State state;

    enum DIRECTION {
        LEFT,
        RIGHT
    }
    DIRECTION direction;

    CurvedWorld_Controller controller;

    public float curveTurningSpeed = 0.2f;

    float[] curveValues = new float[] { -1f, -0.5f, 0f, 0.5f, 1f };
    public float targetCurveValue = 0f;

    public float currentCurve = 0f;

    public float maxCurvedTimer = 30f;
    public float minCurvedTimer = 10f;

    public float minWaitTimer = 5f;
    public float maxWaitTimer = 15f;

    public float counter = 0f;
    
	void Start () {
        controller = GetComponent<CurvedWorld_Controller>();
        state = State.Wait;
        counter = Random.Range(minWaitTimer, maxWaitTimer);
    }
	
	void Update () {

        if (KasideGameManager.Instance.IsPlaying()) {

            switch (state) {

                case State.Targeting:
                    BendWorld();
                    break;

                case State.Wait:
                    if (counter <= 0) {
                        ChangeState();
                    } else {
                        counter -= Time.deltaTime;
                    }
                    break;

            }

        }

	}

    void BendWorld() {

        if (direction == DIRECTION.RIGHT) {

            if (currentCurve >= targetCurveValue) ChangeState();

            controller._V_CW_Bend_Y = currentCurve;
            currentCurve += Time.deltaTime * curveTurningSpeed;

        } else if (direction == DIRECTION.LEFT) {

            if (currentCurve <= targetCurveValue) ChangeState();

            controller._V_CW_Bend_Y = currentCurve;
            currentCurve -= Time.deltaTime * curveTurningSpeed;

        }
        
    }

    void ChangeState() {

        if (state == State.Wait) {
            state = State.Targeting;
            targetCurveValue = curveValues[Random.Range(0, curveValues.Length)];
            direction = targetCurveValue > 0 ? DIRECTION.RIGHT : DIRECTION.LEFT;

        } else if (state == State.Targeting) {
            state = State.Wait;
            counter = Random.Range(minWaitTimer, maxWaitTimer);
        }

    }

}
