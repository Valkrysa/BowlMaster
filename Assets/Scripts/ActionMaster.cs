﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMaster {

    public enum Action { Tidy, Reset, EndTurn, EndGame };

    private int bowl = 1;
    private int[] bowls = new int[21];

    public Action Bowl(int pins) {
        if (pins < 0 || pins > 10) {
            throw new UnityException("Invalid pin amount " + pins + " was passed.");
        }

        bowls[bowl - 1] = pins;

        if (bowl == 21) {
            return Action.EndGame;
        }

        // handles special cases in the last frame
        if (bowl == 19 && pins == 10) {
            bowl += 1;

            return Action.Reset;
        } else if (bowl == 20) {
            bowl += 1;

            if (bowls[19 - 1] == 10 && bowls[20 - 1] == 0) {
                return Action.Tidy;
            } else if ((bowls[19 - 1] + bowls[20 - 1]) % 10 == 0) {
                return Action.Reset;
            } else if (Bowl21Awarded()) {
                return Action.Tidy;
            } else {
                return Action.EndGame;
            }
        }

        if (pins == 10) {
            bowl += 2;
            return Action.EndTurn;
        }

        if (bowl % 2 != 0) {
            bowl += 1;
            return Action.Tidy;
        } else if (bowl % 2 == 0) {
            bowl += 1;
            return Action.EndTurn;
        }

        throw new UnityException("Not sure what action to return!");
    }

    private bool Bowl21Awarded () {
        return (bowls[19 - 1] + bowls[20 - 1] >= 10);
    }
}
