using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ControlHUD : MonoBehaviour
{
    public TextMeshProUGUI vidaTxt;
    public TextMeshProUGUI puntuacionTxt;
    public TextMeshProUGUI tiempoTxt;

    public void SetVidas(int vidas)
    {
        vidaTxt.text = "Vidas: " + vidas;
    }

    public void SetPuntuacion(int cuantos)
    {
        puntuacionTxt.text = "Objetos: " + cuantos;
    }

    public void SetTiempo(int tiempo)
    {
        int segundos = tiempo % 60;
        int minutos = tiempo / 60;
        tiempoTxt.text = minutos.ToString("00") + ":" + segundos.ToString("00");
    }
}
