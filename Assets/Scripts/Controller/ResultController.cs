using System.Collections.Generic;
using PlanningPoker;
using TMPro;
using UnityEngine;

public class ResultController : MonoBehaviour
{

    public List<DragCard> cards;
    public RectTransform posCard;
    public RectTransform posCardBis;
    public Card resultat;
    public Vector2 cardSize;
    public Fonctionnalite fonc; 
    public TMP_Text titre_fonc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
