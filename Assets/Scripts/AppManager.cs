using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class AppManager
{
    private Fonctionnalite[] fonctionnalites;
    private string file;
    private int current_fonc = 0;
    private int tour = 1;
    private Validator.Validate mode;

    
    public AppManager(string file, Validator.Validate mode) {
         if (File.Exists(file)){
            this.file=file;
            string jsonData = File.ReadAllText(file);

            this.fonctionnalites = JsonUtility.FromJson<Fonctionnalite[]>(jsonData);

        }
        else Debug.Log("fichier inexistant");
        int i =0;
        foreach (Fonctionnalite f in  fonctionnalites) {
            if(f.getNote()==null)break;
            else i++;
        }
        current_fonc = i; 
        this.mode = mode;
    }


    public void save() {
        string newJsonData = JsonUtility.ToJson(fonctionnalites, true);
        File.WriteAllText(this.file, newJsonData);
    }

    public Fonctionnalite getCurrent() {
        if(current_fonc>= fonctionnalites.Length)return null;
        return fonctionnalites[current_fonc];
    }

    public int joue_tour(Card[] cartes) {
        if(current_fonc>=fonctionnalites.Length)return -3;
        Card carte;
        if(this.tour == 1){
            carte = Validator.strict(cartes);
        }
        else{
            carte = this.mode(cartes);
        }
        if(carte == null) return 0;
        if(carte == Card.cafe) return -2;
        if(carte == Card.joker) return -1;
        this.fonctionnalites[current_fonc].setNote(carte);
        Debug.Log("" + carte.ToString());
        this.current_fonc++;
        this.tour++;
        return 1;
    }
}
