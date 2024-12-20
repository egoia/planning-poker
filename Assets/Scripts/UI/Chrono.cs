using UnityEngine;
using TMPro;

public class Chrono : MonoBehaviour
{
    public float temps = 60f; // Temps du chronomètre en secondes
    public TMP_Text chronometerText; // Texte pour l'affichage du chronomètre
    public UnityEngine.Events.UnityEvent onChronometerEnd;

    private float timeRestant;
    private bool isRunning = false;

    void OnEnable()
    {
        StartChronometer(); // Démarre automatiquement le chronomètre
    }

    void Update()
    {
        if (isRunning)
        {
            if (timeRestant > 0)
            {
                timeRestant -= Time.deltaTime; // Réduit le temps restant
                UpdateUI(); // Met à jour l'affichage
            }
            else
            {
                EndChronometer(); // Arrête le chronomètre et exécute la fonction
            }
        }
    }

    public void StartChronometer()
    {
        timeRestant = temps;
        isRunning = true;
    }

    private void UpdateUI()
    {
        int minutes = Mathf.FloorToInt(timeRestant / 60); // Calcul des minutes
        int seconds = Mathf.FloorToInt(timeRestant % 60); // Calcul des secondes
        chronometerText.text = $"{minutes:00}:{seconds:00}"; // Format MM:SS
    }

    private void EndChronometer()
    {
        isRunning = false;
        timeRestant = 0;
        chronometerText.text = "00:00"; // Affiche 0 une fois terminé
        onChronometerEnd.Invoke();
    }
}