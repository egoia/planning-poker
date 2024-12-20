using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// @class DragCard
/// @brief Permet de gérer les mouvements d'une carte
///
/// Cette classe gère le déplacement d'une carte (drag and drop) ainsi que son agrandissement
public class DragCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    /// @brief Valeur numérique de la carte à déplacer
    public int cardValue;
    
    /// @brief Facteur de déplacement vertical de la carte lors de l'agrandissement
    public float upScaleOffset;
    
    /// @brief Facteur d'agrandissement de la carte
    public float upScalingFactor;
    
    /// @brief Temps pour agrandissement complet de la carte
    public float upScaleAnimTime;
    
    /// @brief Vitesse de retour auto à la position d'origine
    public float backToHandSpeed;
    
    /// @brief Position dans la main
    public Vector2 handPos;
    
    /// @brief Position du cadre (slot) 
    public Vector2 slotPos;
    
    /// @brief Taille normal de la carte
    [HideInInspector]public Vector2 normalSize;
    
    /// @brief La carte est elle déplacée
    [HideInInspector]public bool isDraged;
    
    /// @brief La carte est elle encrée dans le slot 
    [HideInInspector]public bool isSloted;


    /// @brief Fonction d'évènement souris
    /// 
    /// Agrandit la carte lorsque la souris passe sur elle
    public void OnPointerEnter(PointerEventData eventData){
        StartCoroutine(ResizePanelCoroutine(upScalingFactor*normalSize, upScaleAnimTime));
    }
    
    /// @brief Fonction d'évènement souris
    /// 
    /// Rapetisse la carte lorsque la souris n'est plus sur elle
    public void OnPointerExit(PointerEventData eventData){
        StartCoroutine(ResizePanelCoroutine(normalSize, upScaleAnimTime));
    }

    /// @brief Fonction d'évènement
    /// 
    /// Permet à la carte de suivre la souris lorsqu'on la déplace
    public void OnDrag(PointerEventData eventData)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                GameObject.FindGameObjectWithTag("GameCanva").transform as RectTransform,
                eventData.position,
                GameObject.FindGameObjectWithTag("GameCanva").GetComponent<Canvas>().worldCamera,
                out localPoint);
            rectTransform.anchoredPosition = localPoint;
        }
    }

    /// @brief Fonction d'évènement 
    /// 
    /// Appelée lorsqu'on lâche la carte et elle la fait revenir à un emplacement défini
    public void OnEndDrag(PointerEventData eventData)
    {
        isDraged = false;
        if (isSloted){
            StartCoroutine(MoveCoroutine(slotPos, backToHandSpeed));
            StartCoroutine(ResizePanelCoroutine(normalSize, upScaleAnimTime));
        } 
        else{
            StartCoroutine(MoveCoroutine(handPos, backToHandSpeed));
        }
    }

    /// @brief Remet la carte dans la position de la main du joueur
    public void moveToHand()
    {
        StartCoroutine(MoveCoroutine(handPos, backToHandSpeed));
    }

    /// @brief Fonction d'évènement 
    /// 
    /// Met la variable isDraged à true lorsqu'elle est déplacée
    public void OnBeginDrag(PointerEventData eventData)
    {
        isDraged = true;
    }

    /// @brief Fonction qui agrandit ou rétrécit la carte
    /// @param newSize: la taille à atteindre
    /// @param duration: le temps d'animation
    /// 
    /// Agrandit la carte lorsque la souris passe sur elle
    public IEnumerator ResizePanelCoroutine(Vector2 newSize, float duration)
    {
        Vector2 originalSize = GetComponent<RectTransform>().rect.size;
        
        // Calcul du ratio de redimensionnement
        float widthRatio = newSize.x / originalSize.x;
        float heightRatio = newSize.y / originalSize.y;

        // Durée initiale
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Calcul de l'interpolation linéaire
            float t = elapsedTime / duration;

            // Interpolation de la taille du panel
            Vector2 currentSize = Vector2.Lerp(originalSize, newSize, t);
            GetComponent<RectTransform>().sizeDelta = currentSize;

            // Incrémentation du temps écoulé
            elapsedTime += Time.unscaledDeltaTime;

            // Attente du prochain frame
            yield return null;
        }


        // on s'assure qu'il a bien la taille voulue
        GetComponent<RectTransform>().sizeDelta = newSize;
    }

    /// @brief Fonction qui déplace la carte
    /// @param pos: position de la destination
    /// @param speed: vitesse de déplacement
    /// 
    /// Déplace la carte vers la position passée en paramètre 
    public IEnumerator MoveCoroutine(Vector2 pos, float speed)
    {
        Vector2 originalPos =  GetComponent<RectTransform>().anchoredPosition;

        // Durée initiale
        float elapsedTime = 0f;

        // Calcul de la distance et du temps
        float distance = Vector2.Distance(originalPos, pos);
        float duration = distance / speed;

        while (elapsedTime < duration)
        {
            // Calcul de l'interpolation linéaire
            float t = elapsedTime / duration;

            Vector2 currentPosition = Vector2.Lerp(originalPos, pos, t);
            
            GetComponent<RectTransform>().anchoredPosition = currentPosition;


            // Incrémentation du temps écoulé
            elapsedTime += Time.unscaledDeltaTime;

            // Attente du prochain frame
            yield return null;
        }


        // on s'assure qu'il a bien la taille voulue
        GetComponent<RectTransform>().anchoredPosition = pos;
    }
}
