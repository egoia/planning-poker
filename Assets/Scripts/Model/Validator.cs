using System.Collections.Generic;
using UnityEngine;
using PlanningPoker;

/// @class Validator
/// @brief Equivalent aux modes de jeu 
///
/// Cette classe propose plusieurs méthodes statiques de validation qui 
/// appliquent différentes règles pour valider ou non un vote 
/// (moyenne, médiane, majorité absolue, majorité relative)  
namespace PlanningPoker{
    public class Validator 
    {
        /// @brief Délégué définissant la signature d'une fonction de validation
        /// @param cartes: Tableau de cartes correspondant aux votes
        /// @return: La carte résultante selon la règle de validation
        /// ou null si le vote n'est pas concluant
        public delegate Card Validate(Card[] cartes);

        /// @brief Règle stricte : valide uniquement si toutes les cartes sont identiques
        /// @details Si toutes les cartes sont identiques, retourne cette carte. Sinon, retourne null
        public static readonly Validate strict = (Card[] cartes) => {
            for(int i = 1; i<cartes.Length; i++) {
                if(cartes[i] != cartes[i-1]) return null;
            }
            return cartes[0];
        };

        /// @brief Règle de la moyenne : retourne la carte la plus proche de la moyenne des valeurs
        /// @details
        /// - Si la moitié ou plus des cartes sont des "cafés", retourne la carte "café"
        /// - Si toutes les cartes sont des jokers, retourne le joker
        /// @param cartes: Tableau de cartes correspondant aux votes
        /// @return: La carte la plus proche de la moyenne des valeurs
        public static readonly Validate moyenne = (Card[] cartes) => {
            int somme = 0;
            int nb_cartes = cartes.Length;
            int nb_cafes = 0;
            int nb_jokers = 0;
            for(int i = 0; i<nb_cartes; i++){
                int val = cartes[i].toInt();
                switch(val) {
                    case -2 : nb_cafes++; break;
                    case -1 : nb_jokers++; break;
                    default : somme+=val; break;
                }
            }
            if(nb_cafes >= nb_cartes/2) return Card.cafe;
            if(nb_jokers == nb_cartes) return Card.joker;
            nb_cartes-=(nb_jokers+nb_cafes);

            float moyenne = somme/nb_cartes;
            return Card.numberToCard(moyenne);
        };

        /// @brief Règle de la médiane : retourne la carte la plus proche de la médiane des valeurs
        /// @details
        /// - Si les cafés sont majoritaires, retourne "café"
        /// - Si aucune carte valide n'est présente, retourne un joker
        /// @param cartes: Tableau de cartes correspondant aux votes
        /// @return: La carte correspondant à la médiane 
        public static readonly Validate mediane = (Card[] cartes) => {
            int nb_cafes = 0;
            int nb_jokers = 0;
            List<int> cartes_tmp = new List<int>();
            foreach (Card c in cartes) {
                if(c==Card.cafe)nb_cafes++;
                else if (c==Card.joker)nb_jokers++;
                else cartes_tmp.Add(c.toInt());
            }
            int nb_cartes = cartes_tmp.Count;
            cartes_tmp.Sort();
            int mediane;
            if(nb_cafes>nb_cartes)return Card.cafe;
            if(nb_cartes==0) return Card.joker;
            if(nb_cartes%2==1) mediane = (int)(cartes_tmp[((nb_cartes/2)+1)]);
            else{
                int v1 = (int)(cartes_tmp[(nb_cartes/2)+1]);
                int v2 = (int)(cartes_tmp[(nb_cartes/2)]);
                mediane = (v1+v2)/2;
            }
            return Card.numberToCard(mediane);
        };

        /// @brief Règle de la majorité absolue : valide si une carte est choisie par plus de la moitié des participants
        /// @param cartes: Tableau de cartes correspondant aux votes
        /// @return: La carte ayant obtenu la majorité absolue, ou null si aucune majorité n'existe
        public static readonly Validate majorite_absolue = (Card[] cartes) => {
            Dictionary<Card, int> map = new Dictionary<Card, int>();
            Card max_card= cartes[0];
            foreach (Card c in cartes) {
                if(map.ContainsKey(c)){
                    map[c] += 1;
                }
                else map[c]=1;
                
                if(max_card!=c){
                    max_card = map[max_card] < map[c] ? c : max_card; 
                }
            }
            int nb_cartes = cartes.Length;
            if(map[max_card] > nb_cartes/2) {
                return max_card;
            }
            return null;
        };

        /// @brief Règle de la majorité relative : valide la carte ayant obtenu le plus de votes
        /// @details
        /// - Retourne null si deux cartes ou plus obtiennent le même nombre de votes
        /// - Ignore les jokers
        /// @param cartes: Tableau de cartes correspondant aux votes
        /// @return: La carte ayant obtenu la majorité relative, ou null en cas d'égalité
        public static readonly Validate majorite_relative = (Card[] cartes) => {
            Dictionary<Card, int> map = new Dictionary<Card, int>();
            Card max_card= cartes[0];
            foreach (Card c in cartes) {
                if(c!=Card.joker){
                    if(map.ContainsKey(c)){
                        map[c] += 1;
                    }
                    else map[c]=1;
                    if(max_card!=c){
                    max_card = map[max_card] < map[c] ? c : max_card; 
                    }
                } 
            }
            int max_rec = map[max_card];
            map.Remove(max_card);
            if(map.ContainsValue(max_rec)) return null;
            return max_card;
        };
    }
}
