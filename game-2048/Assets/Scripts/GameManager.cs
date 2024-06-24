using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TileBoard board;
    public CanvasGroup gameOver;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hightScoreText;

    private int score;

    private void Start()
    {
        NewGame();
        PlayAgain();
    }

    public void PlayAgain()
    {
        // reset score and save hight score when click Try Again 
        SetScore(0);
        hightScoreText.text = LoadHightScore().ToString();

        // appear game over screen
        gameOver.alpha = 0f;
        gameOver.interactable = false;

        // update board state
        board.ClearBoard();
        board.CreateTile();
        board.CreateTile();
        board.enabled = true;
    }

    public void NewGame()
    {
        // reset score when click New Game
        SetScore(0);

        // update board state
        board.ClearBoard();
        board.CreateTile();
        board.CreateTile();
        board.enabled = true;
    }

    public void GameOver()
    {
        board.enabled = false;
        gameOver.interactable = true;

        StartCoroutine(Fade(gameOver, 1f, 1f));
    }

    private IEnumerator Fade(CanvasGroup canvasGroup, float to, float delay = 0f)
    {
        yield return new WaitForSeconds(delay);

        float elapsed = 0f;
        float duration = 0.5f;
        float from = canvasGroup.alpha;

        while (elapsed < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = to;
    }

    public void IncreaseScore(int points)
    {
        SetScore(score + points);
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();

        SaveHightScore();
    }

    private void SaveHightScore()
    {
        int hightScore = LoadHightScore();

        if (score > hightScore) 
        {
            PlayerPrefs.SetInt("hightScore", score);
        }
    }

    private int LoadHightScore()
    {
        return PlayerPrefs.GetInt("hightScore", 0);
    }

}
