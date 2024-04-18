using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FPS : MonoBehaviour
{
    public TextMeshProUGUI fpsText;
    public float updateInterval = 0.5f;

    private float accum = 0.0f;
    private int frames = 0;
    private float timeLeft;

    private void Start()
    {
        // Asegúrate de que el TextMeshProUGUI esté asignado en el inspector
        if (fpsText == null)
        {
            Debug.LogError("Falta asignar el componente TextMeshProUGUI en el inspector.");
        }

        timeLeft = updateInterval;
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        frames++;

        // Actualiza el texto del TextMeshProUGUI con los FPS promedio
        if (timeLeft <= 0.0f)
        {
            float fps = accum / frames;
            fpsText.text = "FPS: " + Mathf.RoundToInt(fps);

            timeLeft = updateInterval;
            accum = 0.0f;
            frames = 0;
        }
    }
}
