using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// @class GameParameters
/// @brief Classe statique contenant les paramètres de la partie
///
/// Cette classe permet la transmission des paramètres de la partie entre la
/// scène du menu et celle du jeu
public static class GameParameters
{
    /// @brief Mode de jeu
    public static string mode;
    
    /// @brief Liste des joueurs
    public static List<Joueur> joueurs;
    
    /// @brief Fichier JSON contenant les fonctionnalités à traiter
    public static string file;
}