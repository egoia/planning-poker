using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public TMP_InputField ajoutJoueur;
    [HideInInspector]public string file="";
    public List<Joueur> joueurs = new List<Joueur>();
    public TMP_Dropdown mode;
    public Button start;


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

    public bool checkParameters(){
        if(joueurs.Count<=1)return false;
        if(!File.Exists(file)) return false;
        if(mode.value==-1) return false;
        return true;
    }

    public void onAjoutJoueur() {
        if(!string.IsNullOrWhiteSpace(ajoutJoueur.text)
            &&!alreadyExists(ajoutJoueur.text.Trim()) ){
            joueurs.Add(new Joueur(ajoutJoueur.text));
            Debug.Log(ajoutJoueur.text.Trim());
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


    public bool alreadyExists(string joueur){
        foreach ( Joueur j in joueurs){
            if(j.name.Equals(joueur) ) return true;
        }
        return false;
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit(); 
        #endif
    }

}
