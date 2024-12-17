using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System;
using PlanningPoker;

/// @class Fonctionnalite
/// @brief Représente une fonctionnalité à estimer
///
/// Une fonctionnalité contient un nom, une description, et une note attribuée via le processus d'estimation

namespace PlanningPoker{
    [System.Serializable]
    public class Fonctionnalite 
    {
        /// @brief Nom de la fonctionnalité
        [SerializeField]private string nom;

        /// @brief Description détaillée de la fonctionnalité
        [SerializeField]private string description;

        /// @brief Note associée à la fonctionnalité, sous forme de chaîne de caractères
        [SerializeField]private string note;

        /// @brief Getter pour le nom de la fonctionnalité
        /// @return: Le nom de la fonctionnalité
        public string getNom() 
        {
            return this.nom;
        }

        /// @brief Setter pour le nom de la fonctionnalité
        /// @param nom: Le nom à attribuer
        public void setNom(string nom) 
        {
            this.nom = nom;
        }

        /// @brief Getter pour la description de la fonctionnalité
        /// @return La description de la fonctionnalité
        public string getDescription() 
        {
            return this.description;
        }

        /// @brief Setter pour la description de la fonctionnalité
        /// @param description: La description à attribuer
        public void setDescription(string description) 
        {
            this.description = description;
        }

        /// @brief Getter pour la note associée à la fonctionnalité
        /// @return: La note sous forme de chaîne de caractères, ou null si aucune note n'a été attribuée
        public string getNote() 
        {
            return this.note;
        }

        /// @brief Setter pour la note associée à la fonctionnalité
        /// @param note: La note à attribuer (instance de Card)
        public void setNote(Card note) 
        {
            this.note = note != null ? note.toString() : null;
        }

    }

    [System.Serializable]
    public class FonctionnaliteWrapper
    {
        public Fonctionnalite[] fonctionnalites;
    }
}
