using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
	
	private void Start () 
	{
		playerFemale.OnGetCoin += ()=> OnGetCoin(playerFemale);
		playerMale.OnGetCoin += ()=> OnGetCoin(playerMale);
	}

	private void OnGetCoin(MyPlayer player)
	{
		if (player == playerFemale) femaleScore.Score += .75f;
		else maleScore.Score += 1;
	}
}
