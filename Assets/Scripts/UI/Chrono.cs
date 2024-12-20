using UnityEngine;
using TMPro;

/// @class Chrono
/// @brief Elle permet la gestion du chronomètre lors du débat

public class Chrono : MonoBehaviour
{
    /// @brief Temps du chronomètre en secondes
    public float temps = 60f;  
    
     /// @brief Texte pour l'affichage du chronomètre
    public TMP_Text chronometerText; 
    
    /// @brief Evenement déclenché à la fin du chronomètre
    public UnityEngine.Events.UnityEvent onChronometerEnd;

    /// @brief Le temps restant avant la fin du chrono
    private float timeRestant;
    
    /// @brief Chrono actif ou inactif
    private bool isRunning = false;

    /// @brief Démarre le chronomètre lors de l'activation du canva
    void OnEnable()
    {
        StartChronometer(); 
    }

    /// @brief Met à jour l'affichage du chrono à chaque frame
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
                EndChronometer();
            }
        }
    }

    /// @brief Démarre le chronomètre
    public void StartChronometer()
    {
        timeRestant = temps;
        isRunning = true;
    }

    /// @brief Met à jour l'affichage du chrono 1 fois
    private void UpdateUI()
    {
        int minutes = Mathf.FloorToInt(timeRestant / 60); 
        int seconds = Mathf.FloorToInt(timeRestant % 60);
        chronometerText.text = $"{minutes:00}:{seconds:00}";
    }

    /// @brief Termine le chronomètre et déclenche l'évènement de retour au jeu
    private void EndChronometer()
    {
        isRunning = false;
        timeRestant = 0;
        chronometerText.text = "00:00"; 
        onChronometerEnd.Invoke();
    }
}