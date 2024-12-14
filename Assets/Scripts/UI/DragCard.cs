using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public int cardValue;
    public float upScaleOffset;
    public float upScalingFactor;
    public float upScaleAnimTime;
    public float backToHandSpeed;
    public Vector2 handPos;
    public Vector2 slotPos;
    [HideInInspector]public Vector2 normalSize;
    [HideInInspector]public bool isDraged;
    [HideInInspector]public bool isSloted;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData){
        StartCoroutine(ResizePanelCoroutine(upScalingFactor*normalSize, upScaleAnimTime));
    }
    
    public void OnPointerExit(PointerEventData eventData){
        StartCoroutine(ResizePanelCoroutine(normalSize, upScaleAnimTime));
    }


    public void OnDrag(PointerEventData eventData)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            rectTransform.anchoredPosition += eventData.delta;
        }
    }

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

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDraged = true;
    }

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
