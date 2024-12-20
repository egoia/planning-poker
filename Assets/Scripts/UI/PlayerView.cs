using TMPro;
using UnityEngine;

/// @class PlayerView
/// @brief Gère l'affichage du nom d'un joueur 
///
/// Gestion de l'affichage d'un joueur dans la liste de joueurs dans le menu
public class PlayerView : MonoBehaviour
{
    /// @brief Joueur ajouté
    public Joueur player;
    
    /// @brief Composant de texte contenant le nom du joueur
    public TMP_Text nom;

    /// @brief Mise à jour de l'affichage
    public void updateContent(){
        nom.text = player.name;
    }
    
    /// @brief Méthode appelée lors de la suppression d'un joueur
    public void onDelete(){
        GameObject.Find("MenuManager").GetComponent<MenuController>().joueurs.Remove(player);
        Destroy(gameObject);
    }
}