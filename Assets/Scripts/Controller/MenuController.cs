using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// @class MenuController
/// @brief Gère l'affichage du menu et la transmission des paramètres du jeu
///
/// Cette classe permet l'ajout et l'affichage de la liste des joueurs
public class MenuController : MonoBehaviour
{
    /// @brief Variables d'input pour ajouter un joueur
    public TMP_InputField ajoutJoueur;
    
    /// @brief Fichier JSON à traiter
    [HideInInspector]public string file="";
    
    /// @brief Liste des joueurs
    public List<Joueur> joueurs = new List<Joueur>();
    
    /// @brief Liste déroulante des modes de jeu
    public TMP_Dropdown mode;
    
    /// @brief Bouton commencer
    public Button start;
    
    /// @brief Objet contenant visuellement la liste des joueurs
    public Transform contentNoms;
    
    /// @brief Objet visuel pour afficher un nom de joueur
    public GameObject namePrefab; 

    /// @brief Méthode appelée lorsqu'on appuie sur le bouton start
    /// 
    /// Elle vérifie la validité des paramètres avant de lancer la partie de jeu
    public void onStart()
    {
        if(checkParameters()){
            GameParameters.mode = mode.options[mode.value].text;
            GameParameters.file = file;
            GameParameters.joueurs= joueurs;
            SceneManager.LoadScene("GameScene");
        }
        else{
            Image backgroundImage = start.GetComponentInChildren<Image>();
            backgroundImage.color = Color.red;
        }
    }

    /// @brief Méthode permettant de vérifier la validité des paramètres
    public bool checkParameters(){
        if(joueurs.Count<=1)return false;
        if(!File.Exists(file)) return false;
        if(mode.value==-1) return false;
        return true;
    }

    /// @brief Méthode appelée lors de l'ajout d'un joueur
    /// 
    /// Met à jour l'inputField et la liste des joueurs
    public void onAjoutJoueur() {
        if(!string.IsNullOrWhiteSpace(ajoutJoueur.text)
            &&!alreadyExists(ajoutJoueur.text.Trim()) ){
            joueurs.Add(new Joueur(ajoutJoueur.text));
            Debug.Log(ajoutJoueur.text.Trim());
            AddName(ajoutJoueur.text.Trim());
            ajoutJoueur.text=string.Empty;
            ajoutJoueur.ForceLabelUpdate();
            Image backgroundImage = ajoutJoueur.GetComponentInChildren<Image>();
            backgroundImage.color = Color.white;
        }
        else{
            Image backgroundImage = ajoutJoueur.GetComponentInChildren<Image>();
            backgroundImage.color = Color.red;
        }
    }

    /// @brief Méthode vérifiant l'existance d'un joueur dans la liste
    public bool alreadyExists(string joueur){
        foreach ( Joueur j in joueurs){
            if(j.name.Equals(joueur) ) return true;
        }
        return false;
    }

    /// @brief Arrêter la partie
    /// 
    /// Permet de fermer le jeu lorsqu'on appuie sur le bouton "quitter"
    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit(); 
        #endif
    }

    /// @brief Ajout visuel d'un joueur sur l'interface
    /// 
    /// Permet d'afficher la liste des noms des joueurs ajoutés ainsi 
    /// qu'un bouton pour (éventuellemnet) les supprimer
     public void AddName(string name)
    {
        // Instancier un nouvel élément de la liste
        GameObject newNameItem = Instantiate(namePrefab, contentNoms);
        newNameItem.GetComponent<PlayerView>().player = joueurs[joueurs.Count-1];
        newNameItem.GetComponent<PlayerView>().updateContent();
    }

}
