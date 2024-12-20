using System.Collections.Generic;
using PlanningPoker;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebateController : MonoBehaviour
{
    public TMP_Text nomMin, nomMax, fonc_text;
    public List<DragCard> cards;
    public Fonctionnalite fonc;
    public RectTransform posMax, posMin, posCardEx;
    public Card min, max;
    public Vector2 cardSize;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
