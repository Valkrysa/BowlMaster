using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

    public Text[] scoreBoxes;
    public Text[] frameScores;
	
	public void FillScoreBoxes (List<int> rolls) {
        string formattedScores = FormatScoreBoxes(rolls);
        
        for (int i = 0; i < formattedScores.Length; i++) {
            scoreBoxes[i].text = formattedScores[i].ToString();
        }
    }

    public void FillFrameScores (List<int> frames) {
        for(int i = 0; i < frames.Count; i++) {
            frameScores[i].text = frames[i].ToString();
        }
    }

    public static string FormatScoreBoxes (List<int> rolls) {
        string output = "";

        for (int i = 0; i < rolls.Count; i++) {
            int box = output.Length + 1; // Box Number 1 through 21

            if (rolls[i] == 0) { // 0 in bowling is shown as -
                output += "-";
            } else if (box == 21 && rolls[i - 1] + rolls[i] == 10) { // spare in box 21
                output += "/";
            } else if (box % 2 == 0 && rolls[i - 1] + rolls[i] == 10) { // spare in bowling is shown as /
                output += "/";
            } else if (box >= 19 && rolls[i] == 10) { // strike in frame 10
                output += "X";
            } else if (rolls[i] == 10) { // strike in bowling is shown as X
                output += "X ";
            } else {
                output += rolls[i].ToString(); // normal bowl (1-9)
            }
        }

        return output;
    }
}
