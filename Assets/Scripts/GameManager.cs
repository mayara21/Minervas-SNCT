using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	[Serializable]
	public struct PlayerScore
	{
		private float score;
		public TextMeshProUGUI scoreText;

		public float Score
		{
			get { return score; }
			set
			{
				score = value;
				scoreText.text = score.ToString();
			}
		}
	}

	[SerializeField] private MyPlayer playerFemale;
	[SerializeField] private MyPlayer playerMale;

	[Space, SerializeField] private PlayerScore maleScore; 
	[SerializeField] private PlayerScore femaleScore;

    [SerializeField] private int finalSceneIndex = 2;
    private int finishCount = 0;
	
	private void Start () 
	{
		playerFemale.OnGetCoin += ()=> OnGetCoin(playerFemale);
		playerMale.OnGetCoin += ()=> OnGetCoin(playerMale);
        playerFemale.SaveScore += ()=> SaveScore(playerFemale);
        playerMale.SaveScore += ()=> SaveScore(playerMale);
        playerFemale.OnHitObstacle += () => OnHitObstacle(playerFemale);
        playerMale.OnHitObstacle += () => OnHitObstacle(playerMale); 
	}

	private void OnGetCoin(MyPlayer player)
	{
		if (player == playerFemale) femaleScore.Score += 75f;
		else maleScore.Score += 100;
	}

    private void OnHitObstacle(MyPlayer player) {
        if (player == playerFemale) {
            if (femaleScore.Score - 20 >= 0) femaleScore.Score -= 20f;
            else femaleScore.Score = 0f;
        }
        else {
            if (maleScore.Score - 10 >= 0) maleScore.Score -= 10f;
            else maleScore.Score = 0f;
        }
    }

    private void SaveScore(MyPlayer player) {
        finishCount += 1;
        if (player == playerFemale)
        {
            PlayerPrefs.SetFloat("Female Score", femaleScore.Score);
        }
        else
        {
            PlayerPrefs.SetFloat("Male Score", maleScore.Score);
        }

        if (finishCount == 2) {
            StartCoroutine(EndGame());
        }
    }

    private IEnumerator EndGame() {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(finalSceneIndex);
    }
}
