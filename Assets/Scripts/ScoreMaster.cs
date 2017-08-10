using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreMaster {

    public static List<int> ScoreCumulative (List<int> rolls) {
        List<int> cumulativeScores = new List<int>();
        int runningTotal = 0;

        foreach (int frameScore in ScoreFrames(rolls)) {
            runningTotal += frameScore;
            cumulativeScores.Add(runningTotal);
        }

        return cumulativeScores;
    }

    public static List<int> ScoreFrames (List<int> rolls) {
        List<int> frameList = new List<int>();

        // i is the second bowl of a frame in this case
        for (int i = 1; i < rolls.Count; i += 2) {
            if (frameList.Count == 10) { // prevent 11th frame score
                break;
            }

            if (rolls[i - 1] + rolls[i] < 10) { // common frame
                frameList.Add(rolls[i - 1] + rolls[i]);
            }

            if (rolls.Count - i <= 1) { // nothing ahead in list
                break;
            }

            if (rolls[i - 1] == 10) { // strike bonus
                i--;
                frameList.Add(10 + rolls[i + 1] + rolls[i + 2]);
            } else if (rolls[i - 1] + rolls[i] == 10) { // spare bonus
                frameList.Add(10 + rolls[i + 1]);
            }
        }

        return frameList;
    }
}
