using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour {

    public float horizontalSpeed;
    public float verticalSpeed;

    public void ApplyPowerUp(PowerUp powerUp) {

        switch (powerUp.targetStat) {

            case PowerUp.TargetStat.VerticalSpeed:
                verticalSpeed *= powerUp.statMultiplier;
                break;

            case PowerUp.TargetStat.HorizontalSpeed:
                horizontalSpeed *= powerUp.statMultiplier;
                break;

        }

    }

    public void UnapplyPowerUp(PowerUp powerUp) {

        switch (powerUp.targetStat) {

            case PowerUp.TargetStat.VerticalSpeed:
                verticalSpeed *= 1 / powerUp.statMultiplier;
                break;

            case PowerUp.TargetStat.HorizontalSpeed:
                horizontalSpeed *= 1 / powerUp.statMultiplier;
                break;

        }

    }

}
