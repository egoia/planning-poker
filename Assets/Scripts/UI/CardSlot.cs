
using UnityEngine;

/// @class CardSlot
/// @brief Permet d'encrer les cartes à l'intérieur du cadre (slot)
public class CardSlot : MonoBehaviour
{
    /// @brief Liste des composantes de déplacement des cartes
    public DragCard[] hand;
    
    /// @brief Espace entre les cartes dans la main
    public float spaceBeetweenCards;
    
    /// @brief Position de la main 
    public RectTransform handPos;
    
    /// @brief Distance à laquelle une carte est encrée dans le slot
    public float triggerDist;
    
    /// @brief Taille des cartes
    public Vector2 cardSize;
    
    /// @brief La carte encrée dans l'emplacement
    public DragCard card=null;

    /// @brief Donne une position à chaque carte
    /// 
    /// Start is called once before the first execution of Update after the MonoBehaviour is created
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

    /// @brief Vérifie la distance de l'emplacement par rapport à chaque carte, à chaque frame
    /// 
    /// Si une carte est à bonne distance et que l'emplacement est vide : elle est encrée
    /// Si une carte est à bonne distance et que l'emplacement est rempli : elle remplace la carte encrée
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
