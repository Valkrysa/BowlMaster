using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject resetButton;

    private List<int> bowls = new List<int>();
    private PinSetter pinSetter;
    private Ball ball;
    private ScoreDisplay scoreDisplay;
    
	void Start () {
        pinSetter = GameObject.FindObjectOfType<PinSetter>();
        ball = GameObject.FindObjectOfType<Ball>();
        scoreDisplay = GameObject.FindObjectOfType<ScoreDisplay>();
    }
	
	public void Bowl (int pinFall) {
        try {
            bowls.Add(pinFall);
            ActionMaster.Action nextAction = ActionMaster.NextAction(bowls);
            if (nextAction == ActionMaster.Action.EndGame) {
                resetButton.SetActive(true);
            } else {
                ball.Reset();
                pinSetter.PerformAction(nextAction);
            }
        } catch {
            Debug.LogWarning("Something went horribly wrong in GameManager.Bowl performing bowl action");
        }
        try {
            scoreDisplay.FillScoreBoxes(bowls);
            scoreDisplay.FillFrameScores(ScoreMaster.ScoreCumulative(bowls));
        } catch {
            Debug.LogWarning("Something went horribly wrong in GameManager.Bowl with scoreDisplay.FillScoreBoxes");
        }
    }

    public void ResetGame () {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
