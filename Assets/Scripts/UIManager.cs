using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private Sprite[] _lifeSprites;
    [SerializeField]
    private Text _gameOver;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "SCORE: " + 0;
        _gameOver.gameObject.SetActive(false);
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdatesLives(int currentLives)
    {
        _livesImg.sprite = _lifeSprites[currentLives];

        if (currentLives < 1)
        {
            _gameOver.gameObject.SetActive(true);
            StartCoroutine(GameOverFlickerRoutine());
        }
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while(true)
        {
            _gameOver.text = "";
            yield return new WaitForSeconds(0.3f);
            _gameOver.text = "GAME OVER";
            yield return new WaitForSeconds(0.3f);
        }
    }
}
