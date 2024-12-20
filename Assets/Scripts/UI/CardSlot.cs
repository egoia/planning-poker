
using UnityEngine;

public class CardSlot : MonoBehaviour
{
    public DragCard[] hand;
    public float spaceBeetweenCards;
    public RectTransform handPos;
    public float triggerDist;
    public Vector2 cardSize;
    public DragCard card=null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float pos = -5.5f*spaceBeetweenCards;
        foreach (DragCard c in hand){
            c.slotPos = GetComponent<RectTransform>().anchoredPosition;
            c.handPos = handPos.anchoredPosition + new Vector2(pos, 0);
            c.GetComponent<RectTransform>().anchoredPosition = c.handPos;
            pos+=spaceBeetweenCards;
            c.GetComponent<RectTransform>().sizeDelta = cardSize;
            c.normalSize = cardSize;
        }
        card = null;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (DragCard c in hand){
            if(Vector2.Distance(c.gameObject.GetComponent<RectTransform>().anchoredPosition, gameObject.GetComponent<RectTransform>().anchoredPosition) <= triggerDist && c.isDraged && c!=card){
                if(card!=null)
                {   
                    card.isSloted = false;
                    card.moveToHand();
                }
                c.isSloted = true;
                card = c;
            }
            else if(Vector2.Distance(c.gameObject.GetComponent<RectTransform>().anchoredPosition, gameObject.GetComponent<RectTransform>().anchoredPosition) > triggerDist && c.isSloted && c.isDraged && card!=null){
                c.isSloted = false;
                card=null;
            }
        }
    }
}
