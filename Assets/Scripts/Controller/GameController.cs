using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PlanningPoker;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    public TMP_Text nom_joueur, titre_func, desc_func, text_tour;
    private List<Joueur> joueurs;
    private int curr_player_id = 0;
    private AppManager appManager; 
    private Card[] cards;
    public CardSlot cardSlot;
    public Canvas normalCanva, resultCanva, finCanva, cafeCanva, debateCanva;
    public ResultController resultManager;
    public DebateController debateController;

    public void OnEnable()
    {
        string mode_name = GameParameters.mode;
        Validator.Validate mode= mode_name switch{
            "Strict" => Validator.strict,
            "Moyenne"=> Validator.moyenne,
            "Médiane"=> Validator.mediane,
            "Majorité relative" => Validator.majorite_relative,
            "Majorité absolue" => Validator.majorite_absolue,
            _ => throw new Exception("Erreur de mode de jeu"),
        };

        string file = GameParameters.file;
        joueurs = GameParameters.joueurs;
        appManager = new AppManager(file, mode);
        
        if(appManager.getCurrent() == null)
        {
            resultCanva.gameObject.SetActive(false);
            finCanva.gameObject.SetActive(true);
            return;
        }
        cards = new Card[joueurs.Count];

        majFonctionnalite();

        Joueur curr_player = joueurs[curr_player_id];
        nom_joueur.text = curr_player.name;

        text_tour.text = "Tour "+appManager.tour;
    }

    public void onValideCard() {
        Card carte = Card.numberToCard(cardSlot.card.cardValue);
        cards[curr_player_id]= carte;
        curr_player_id++;

        if(curr_player_id == joueurs.Count) 
        {
            Fonctionnalite fonc = appManager.getCurrent();
            Card c;
            int res = appManager.joue_tour(cards, out c);
            text_tour.text = "Tour "+appManager.tour;
            curr_player_id = 0;
            if(res == 1)// si on s'est mis d'accord
            {
                appManager.save();
                resultManager.resultat = c;
                resultManager.fonc = fonc;
                normalCanva.gameObject.SetActive(false);
                resultCanva.gameObject.SetActive(true);
                return;
            }
            if(res == -2)// si on veut boire un café
            {
                appManager.save();
                normalCanva.gameObject.SetActive(false);
                cafeCanva.gameObject.SetActive(true);
                return;
            }
            else if(res!=-1){//si on ne s'est pas mis d'accord place au débat
                Card min = Card.cent;
                Card max = Card.cafe;
                string joueursMin="";
                string joueursMax="";
                for(int i = 0; i<joueurs.Count; i++){
                    if(cards[i]==Card.cafe ||cards[i]==Card.joker)continue;
                    //min
                    if(cards[i].toInt()<min.toInt()){
                        min = cards[i];
                        joueursMin = joueurs[i].name;
                    }
                    else if(cards[i].toInt()==min.toInt()){
                        joueursMin += ", "+joueurs[i].name;
                    }
                    //max
                    if(cards[i].toInt()>max.toInt()){
                        max = cards[i];
                        joueursMax = joueurs[i].name+" ";
                    }
                    else if(cards[i].toInt()==max.toInt()){
                        joueursMax += ", "+joueurs[i].name;
                    }

                }
                if(min!=max&&max!=Card.cafe){
                    debateController.nomMin.text = joueursMin;
                    debateController.nomMax.text= joueursMax;
                    debateController.min = min;
                    debateController.max = max;
                    debateController.fonc = fonc;
                    normalCanva.gameObject.SetActive(false);
                    debateCanva.gameObject.SetActive(true);
                    return;
                }
            }
        }
        Joueur curr_player = joueurs[curr_player_id];
        nom_joueur.text = curr_player.name;

        cardSlot.card.isSloted = false;
        cardSlot.card.moveToHand();
        cardSlot.card = null;
    }


    public void backToNormalCanva(){

        if(appManager.getCurrent() == null)
        {
            resultCanva.gameObject.SetActive(false);
            finCanva.gameObject.SetActive(true);
            return;
        }

        normalCanva.gameObject.SetActive(true);
        resultCanva.gameObject.SetActive(false);
        Joueur curr_player = joueurs[curr_player_id];
        nom_joueur.text = curr_player.name;

        majFonctionnalite();

        cardSlot.card.isSloted = false;
        cardSlot.card.moveToHand();
        cardSlot.card = null;
    }

    public void majFonctionnalite(){
        Fonctionnalite fonc_courante = appManager.getCurrent();
        titre_func.text = fonc_courante.getNom();
        desc_func.text= fonc_courante.getDescription();
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit(); 
        #endif
    }

    public void retourMenu() {
        SceneManager.LoadScene("MenuScene");
    }

    public void backFromCanva(Canvas canva) {
        normalCanva.gameObject.SetActive(true);
        canva.gameObject.SetActive(false);
        Joueur curr_player = joueurs[curr_player_id];
        nom_joueur.text = curr_player.name;

        cardSlot.card.isSloted = false;
        cardSlot.card.moveToHand();
        cardSlot.card = null;
    }

    public void backFromCafe(){
        backFromCanva(cafeCanva);
    }

    public void backFromeDebate(){
        backFromCanva(debateCanva);
    }

}
