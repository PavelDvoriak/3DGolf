using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject ballPrefab;
    public CameraController cameraController;
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI strokeCountText;
    public GameObject level;

    private WaitForSeconds wait;
    private GameObject player;
    private BallControll ballControl;
    private int levelIndex;
    private int holeNumber;
    private ScoreCollider holeCollider;
    private Transform tSpot;
    private LevelData levelData;
    private bool roundPlaying;
    private int par;
    private int holeScore;
    private Dictionary<int, int> totalScore;
    private int scenesCount;

    // Start is called before the first frame update
    void Start()
    {
        wait = new WaitForSeconds(3f);

        totalScore = ScoreCounter.score;
        levelIndex = SceneManager.GetActiveScene().buildIndex;
        scenesCount = SceneManager.sceneCountInBuildSettings;

        SetUpLevel();
        SpawnBall();
        SetCamera();

        roundPlaying = false;

        StartCoroutine(HoleCoroutine());
    }

    private void SpawnBall()
    {
        player = Instantiate(ballPrefab, tSpot) as GameObject;
        ballControl = player.GetComponent<BallControll>();
    }

    private void SetCamera()
    {

        cameraController.target = player.transform;
        cameraController.SetOffset();
    }

    private IEnumerator HoleCoroutine()
    {
        yield return StartCoroutine(HoleStart());
        yield return StartCoroutine(HoleInProgress());
        yield return StartCoroutine(HoleFinished());

        if(levelIndex < scenesCount - 1)
        {
            LoadNextLevel();
        } else
        {
            yield return StartCoroutine(GameOver());
        }
    }

    private IEnumerator HoleStart()
    {
        ballControl.ResetStrokeCounter();
        DisablePlayerControll();
        holeCollider.enabled = false;
        messageText.text = "Hole " + holeNumber;
        yield return wait;
    }

    private IEnumerator HoleInProgress()
    {
        roundPlaying = true;
        EnablePlayerControl();
        holeCollider.enabled = true;

        messageText.text = string.Empty;
        messageText.enabled = false;

        while (roundPlaying)
        {
            UpdateUI();
            yield return null;
        }
    }

    private IEnumerator HoleFinished()
    {
        DisablePlayerControll();

        holeScore = ballControl.strokeCount - par;
        WriteScore();

        messageText.enabled = true;
        messageText.text = GetEndHoleMessage();

        while(!Input.anyKeyDown)
        {
            yield return null;
        }
    }

    private void DisablePlayerControll()
    {
        ballControl.enabled = false;
    }

    private void EnablePlayerControl()
    {
        ballControl.enabled = true;
    }

    private void UpdateUI()
    {
        strokeCountText.text = "Strokes: " + ballControl.strokeCount;
    }

    private void SetUpLevel()
    {
        levelData = level.GetComponent<LevelData>();

        holeNumber = levelIndex;
        par = levelData.par;

        holeCollider = level.GetComponentInChildren<ScoreCollider>();

        tSpot = levelData.tSpot;
    }

    public void EndTurn()
    {
        roundPlaying = false;
    }

    private string GetEndHoleMessage()
    {
        string result = "Hole " + holeNumber + " completed! \n" + "Took you " + ballControl.strokeCount + " strokes to finish it.\n\n" + "Your score so far is:\n";

        var keys = totalScore.Keys;
        
        foreach(int key in keys)
        {
            result += "Hole" + key + " : " + totalScore[key] + " | ";
        }

        result += "\n\n\n press any key to continue...";

        return result;
    }

    private void WriteScore()
    {
        totalScore.Add(holeNumber, holeScore);
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(levelIndex + 1);
    }

    private IEnumerator GameOver()
    {
        var values = totalScore.Values;
        int resultScore = 0;

        foreach(int val in values)
        {
            resultScore += val;
        }

        string scoreText;

        if(resultScore < 0)
        {
            scoreText = "Congratulations, what a score! You are a legend of the game!";
        } else if(resultScore == 0)
        {
            scoreText = "You did great, but there\'s still room for improvement!";
        } else {
            scoreText = "Try harder to achieve better score next time!";
        }

        messageText.text = "You have completed the course!\n\nYour total score is: " + resultScore + "\n\n" + scoreText;

        ScoreCounter.score.Clear(); 

        yield return wait;
        SceneManager.LoadScene(0);
    }
}
