using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using PlanningPoker;

/// @class AppManager
/// @brief Contient les méthodes nécessaires à la logique du jeu 
///
/// Cette classe est responsable de la gestion des fonctionnalités, de la sauvegarde et du processus
/// de jeu, en utilisant différents modes de jeu 

namespace PlanningPoker{
    public class AppManager
    {
        /// @brief Tableau des fonctionnalités à estimer
        public Fonctionnalite[] fonctionnalites;

        /// @brief Chemin du fichier contenant les données des fonctionnalités
        private string file;

        /// @brief Index de la fonctionnalité en cours d'estimation
        private int current_fonc = 0;

        /// @brief Numéro du tour en cours
        public int tour {get;set;}

        /// @brief Mode de jeu
        private Validator.Validate mode;

        /// @brief Constructeur de la classe AppManager
        /// @param file: Chemin du fichier contenant les fonctionnalités en JSON
        /// @param mode: Fonction de validation utilisée en fonction du mode de jeu
        ///
        /// Initialise les fonctionnalités à partir du fichier JSON, et sélectionne le mode de jeu 
        public AppManager(string file, Validator.Validate mode) 
        {
            tour = 1;
            if (File.Exists(file))
            {
                this.file=file;
                string jsonData = File.ReadAllText(file);

                FonctionnaliteWrapper wrapper = JsonUtility.FromJson<FonctionnaliteWrapper>(jsonData);
                if (wrapper == null)
                {
                    Debug.LogError("Le JSON est mal formé ou incompatible avec FonctionnaliteWrapper.");
                }
                else if (wrapper.fonctionnalites == null || wrapper.fonctionnalites.Length == 0)
                {
                    Debug.LogError("Le fichier JSON est valide mais ne contient aucune fonctionnalité.");
                }
                else
                {
                    fonctionnalites = wrapper.fonctionnalites;
                    Debug.Log("Fonctionnalités chargées : " + fonctionnalites.Length);
                }

            }
            else Debug.Log("fichier inexistant");
            
            int i =0;
            foreach (Fonctionnalite f in  fonctionnalites) {
                if(f.getNote()==null || f.getNote()=="")break;
                else i++;
            }
            current_fonc = i; 
            this.mode = mode;
        }

        /// @brief Sauvegarde les données actuelles des fonctionnalités dans le fichier JSON
        ///
        /// Convertit les données des fonctionnalités en JSON et les écrit dans le fichier
        public void save() {
            try 
            {
            string newJsonData = JsonUtility.ToJson(new FonctionnaliteWrapper { fonctionnalites = fonctionnalites }, true);
            File.WriteAllText(this.file, newJsonData);
            Debug.Log("Sauvegarde effectuée avec succès.");
            }
            catch (IOException e)
            {
                Debug.LogError("Erreur lors de la sauvegarde du fichier: " + e.Message);
            }
        }

        /// @brief Obtient la fonctionnalité actuellement en cours d'estimation
        /// @return: La fonctionnalité en cours, ou null si toutes les fonctionnalités sont estimées
        public Fonctionnalite getCurrent() {
            if(current_fonc>= fonctionnalites.Length) return null;
            return fonctionnalites[current_fonc];
        }

        /// @brief Joue un tour en évaluant les cartes soumises par les joueurs
        /// @param cartes: Tableau des cartes soumises par les joueurs pour ce tour
        /// @param c: carte résultant du vote
        /// @return: Un code indiquant le résultat du tour :
        /// - '1' : Le tour s'est bien passé
        /// - '0' : Aucune carte validée
        /// - '-1' : Résultat joker
        /// - '-2' : Résultat "café"
        /// - '-3' : Aucune fonctionnalité restante à estimer
        ///
        /// La fonction utilise une règle stricte pour le premier tour, puis applique la règle de validation sélectionnée pour les tours suivants
        public int joue_tour(Card[] cartes, out Card c) {
            c=null;
            if(current_fonc>=fonctionnalites.Length)return -3;
            Card carte;
            if(this.tour == 1){
                carte = Validator.strict(cartes);
            }
            else{
                carte = this.mode(cartes);
            }
            c = carte;
            
            if(carte == Card.cafe) return -2;
            this.tour++;
            if(carte == null) return 0;
            if(carte == Card.joker) return -1;
            this.fonctionnalites[current_fonc].setNote(carte);
            this.current_fonc++;
            this.tour=1;
            return 1;
        }
    }
}
