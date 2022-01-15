using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//ecran de fin
public class gameOverScreen : MonoBehaviour
{
    public Text pointsText;
    public static gameOverScreen instance;

    //tentative de gestion de score (manquée) + apparition écran de fin
    public void Setup(int kills)
    {
        gameObject.SetActive(true);
        pointsText.text = kills.ToString() + " Kills";
    }

    //restart
    public void RestartButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

}
