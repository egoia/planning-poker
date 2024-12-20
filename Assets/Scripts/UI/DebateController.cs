using System.Collections.Generic;
using PlanningPoker;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// @class DebateController
/// @brief Permet de gérer l'affichage lors du débat
///
/// Cette classe est un composant qui s'occupe d'afficher les cartes extrêmes 
/// ainsi que les noms des joueurs qui ont voté avec ces cartes.
/// Ce composant est activé uniquement lorsque le canva débat est affiché

public class DebateController : MonoBehaviour
{
    /// @brief Variables d'affichage texte
    public TMP_Text nomMin, nomMax, fonc_text;

    /// @brief Listes des composantes de déplacement de toutes les cartes
    public List<DragCard> cards;

    /// @brief Fonctionnalité sur laquelle débattre 
    public Fonctionnalite fonc;

    /// @brief Respectivement la position des cartes de valeur max, min et celles non utilisées pour le débat
    public RectTransform posMax, posMin, posCardEx;

    /// @brief Les cartes de valeurs extrêmes
    public Card min, max;
    
    /// @brief Taille souhaitée des cartes
    public Vector2 cardSize;

    /// @brief Au début du débat, initialise la position de chaque carte
    /// puis on les y déplace
    /// 
    /// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (DragCard card in cards){
            if (card.cardValue==min.toInt()){
                card.handPos = posMin.anchoredPosition;
            }
            else if (card.cardValue==max.toInt()){
                card.handPos = posMax.anchoredPosition;
            }
            else{
                card.handPos = posCardEx.anchoredPosition;
            }
            card.GetComponent<RectTransform>().sizeDelta = cardSize;
            card.normalSize = cardSize;
            card.moveToHand();
        }
        fonc_text.text = fonc.getNom();
    }

}
