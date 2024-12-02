using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System;

[System.Serializable]
public class Fonctionnalite 
{
    [SerializeField]private string nom;
    [SerializeField]private string description;
    [SerializeField]private string note;

    public string getNom() {
        return this.nom;
    }

    public void setNom(string nom) {
        this.nom = nom;
    }

    public string getDescription() {
        return this.description;
    }

    public void setDescription(string description) {
        this.description = description;
    }

    public string getNote() {
        return this.note;
    }

    public void setNote(Card note) {
        this.note = note.toString();
    }

}