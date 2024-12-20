using System.Collections.Generic;
using PlanningPoker;
using TMPro;
using UnityEngine;

/// @class ResultController
/// @brief Permet de gérer l'affichage du résultat d'un vote
///
/// Cette classe est un composant qui s'occupe d'afficher la carte votée pour la fonctionnalité
/// Ce composant est activé uniquement lorsque le canva de résultat est affiché
public class ResultController : MonoBehaviour
{
    /// @brief Listes des composantes de déplacement de toutes les cartes
    public List<DragCard> cards;
    
    /// @brief Position de la carte votée
    public RectTransform posCard;
    
    /// @brief Position des autres cartes
    public RectTransform posCardBis;
    
    /// @brief La carte votée
    public Card resultat;
    
    /// @brief Taille des cartes
    public Vector2 cardSize;
    
    /// @brief Fonctionnalité notée
    public Fonctionnalite fonc; 

    /// @brief Variable de texte pour afficher le titre de la fonctionnalité votée
    public TMP_Text titre_fonc;

    /// @brief Au début du débat, initialise la position de chaque carte
    /// puis on les y déplace
    /// 
    /// Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        foreach (DragCard card in cards){
            if (card.cardValue==resultat.toInt()){
                card.handPos = posCard.anchoredPosition;
                card.GetComponent<RectTransform>().sizeDelta = cardSize;
                card.normalSize = cardSize;
                card.moveToHand();
            }
            else{
                card.handPos = posCardBis.anchoredPosition;
                card.GetComponent<RectTransform>().sizeDelta = cardSize;
                card.normalSize = cardSize;
                card.moveToHand();
            }
        }
        titre_fonc.text = fonc.getNom();
    }
}
