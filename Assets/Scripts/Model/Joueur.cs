using PlanningPoker;
using UnityEngine;

/// @class Joueur
/// @brief Permet d'attribuer un nom à un joueur
///
/// Cette classe est inutile à l'instant T mais elle a été créée
/// dans la perspective d'ajouter un mode réseau au projet.
/// Elle peut être facilement remplacée par un string
public class Joueur 
{
    public string name{get;}

    public Joueur(string name){
        this.name = name;
    }
    
}