using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Final : MonoBehaviour {
    
    private string victory = "Vitória\n\n";
    private string defeat = "Derrota\n\n";
    private string tie = "Empate\n\n";
    private string p1 = "Jogador 1\n";
    private string p2 = "Jogador 2\n";

    [SerializeField] private TextMeshProUGUI player1;
    [SerializeField] private TextMeshProUGUI player2;

	private void Awake() {
        float femaleScore = PlayerPrefs.GetFloat("Female Score");
        float maleScore = PlayerPrefs.GetFloat("Male Score");

        if(femaleScore > maleScore) {
            player1.text =  victory + p1 + femaleScore.ToString() + " pontos";
            player2.text = defeat + p2 + maleScore.ToString() + " pontos";
        }
        else if(maleScore > femaleScore) {
            player1.text = defeat + p1 + femaleScore.ToString() + " pontos";
            player2.text = victory + p2 + maleScore.ToString() + " pontos";
        }
        else {
            player1.text = tie + p1 + femaleScore.ToString() + " pontos";
            player2.text = tie + p2 + maleScore.ToString() + " pontos";
        }

	}

    public void Restart() {
        SceneManager.LoadScene(0);
    }
}
