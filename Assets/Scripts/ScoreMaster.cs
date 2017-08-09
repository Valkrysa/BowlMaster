using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMaster {

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
        int summation = 0;
        int framePart = 0;

        foreach (int roll in rolls) {
            framePart++;
            summation += roll;

            Debug.Log("-------");
            Debug.Log(framePart);
            Debug.Log(roll);
            Debug.Log(summation);

            if (framePart == 3) {
                Debug.Log("Ending on frame 3, adding result: " + summation);
                frameList.Add(summation);
                framePart = 1;
                summation = roll;
            } // ADD THING TO CHECK FIRST INDEX FOR STRIKE BONUS, IF 10 THEN

            if (framePart == 2) {
                if (summation >= 10) {
                    // keep going
                } else {
                    Debug.Log("Ending on frame 2, adding result: " + summation);
                    frameList.Add(summation);
                    framePart = 0;
                    summation = 0;
                }
            }



        }

        return frameList;
    }
}
