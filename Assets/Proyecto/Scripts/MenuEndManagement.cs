using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuEndManagement : MonoBehaviour
{
    public TextMeshProUGUI finalMensaje;
    public TextMeshProUGUI puntuacion;

    private void Awake()
    {
        //HaGanado:
        if (PlayerPrefs.GetInt("HaGanado") == 1)
        {
            finalMensaje.text = "VICTORIA";
            puntuacion.text = "Puntuacion: " + PlayerPrefs.GetInt("Puntuacion").ToString();
        }
        //HaPerdido:
        if (PlayerPrefs.GetInt("HaGanado") == 0)
        {
            finalMensaje.text = "DERROTA";
        }
    }
    public void GameMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
