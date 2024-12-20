using TMPro;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public Joueur player;
    public TMP_Text nom;


    public void updateContent(){
        nom.text = player.name;
    }
    public void onDelete(){
        GameObject.Find("MenuManager").GetComponent<MenuController>().joueurs.Remove(player);
        Destroy(gameObject);
    }
}