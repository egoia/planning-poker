using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PlanningPoker;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

/// @class GameController
/// @brief Permet de gérer le fonctionnement du jeu en reliant le modèle aux différentes vues
///
/// Cette classe est reponsable de l'affiche des différents canvas et de la communication avec le modèle 
public class GameController : MonoBehaviour
{
     /// @brief Variables d'affichage pour différents textes
    public TMP_Text nom_joueur, titre_func, desc_func, text_tour;

     /// @brief Liste des joueurs
    private List<Joueur> joueurs;

     /// @brief L'id du joueur courant
    private int curr_player_id = 0;

     /// @brief Modèle utilisé
    private AppManager appManager; 

     /// @brief Tableau des cartes jouées pour le tour courant
    private Card[] cards;

     /// @brief Emplacement pour poser la carte choisie
    public CardSlot cardSlot;

     /// @brief Différentes vues en fonction de l'état du jeu 
    public Canvas normalCanva, resultCanva, finCanva, cafeCanva, debateCanva;
    
     /// @brief Manager qui gère l'affichage du résultat d'un vote
    public ResultController resultManager;

     /// @brief Manager qui gère l'affichage du canva de débat
    public DebateController debateController;

    /// @brief Objet contenant visuellement la liste des fonctionnalitées
    public Transform contentFonc;
    
    /// @brief Objet visuel pour afficher la fonctionnalité à la fin de la partie
    public GameObject foncPrefab; 

    /// @brief Initialise les variables lors de l'activation
    /// 
    /// Initialise les variables à partir des données entrées 
    /// dans le menu et met à jour l'affichage en conséquence
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

            foreach(Fonctionnalite fonc in appManager.fonctionnalites){
                // Instancier un nouvel élément de la liste
                Debug.Log("" + fonc.getNom());
                GameObject newFoncItem = Instantiate(foncPrefab, contentFonc);
                newFoncItem.transform.GetChild(0).GetComponent<TMP_Text>().text = fonc.getNom();
                newFoncItem.transform.GetChild(1).GetComponent<TMP_Text>().text = "Note : "+fonc.getNote();
            }
            return;
        }
        cards = new Card[joueurs.Count];

        majFonctionnalite();

        Joueur curr_player = joueurs[curr_player_id];
        nom_joueur.text = curr_player.name;

        text_tour.text = "Tour "+appManager.tour;
    }

    /// @brief Valide une carte
    /// 
    /// Met à jour l'affichage du nom du joueur et si le tour est fini active
    /// la vue de l'étape suivante
    public void onValideCard() {
        if(cardSlot.card == null)return;
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

    /// @brief Retour du canva de résultat
    /// 
    /// Permet de mettre à jour l'affichage au retour du canva resultat
    public void backToNormalCanva(){

        if(appManager.getCurrent() == null)
        {
            resultCanva.gameObject.SetActive(false);
            finCanva.gameObject.SetActive(true);
            Debug.Log("hahahah");
            foreach(Fonctionnalite fonc in appManager.fonctionnalites){
                // Instancier un nouvel élément de la liste
                Debug.Log("lalalal" + fonc.getNom());
                GameObject newFoncItem = Instantiate(foncPrefab, contentFonc);
                newFoncItem.transform.GetChild(0).GetComponent<TMP_Text>().text = fonc.getNom();
                newFoncItem.transform.GetChild(1).GetComponent<TMP_Text>().text = "Note : "+fonc.getNote();
            }
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

    /// @brief Mise à jour visuelle de la fonctionnalité traitée
    public void majFonctionnalite(){
        Fonctionnalite fonc_courante = appManager.getCurrent();
        titre_func.text = fonc_courante.getNom();
        desc_func.text= fonc_courante.getDescription();
    }

    /// @brief Arrêter la partie
    /// 
    /// Permet de fermer le jeu lorsqu'on appuie sur le bouton "quitter"
    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        UnityEngine.Application.Quit(); 
        #endif
    }

    /// @brief Retour au menu
    public void retourMenu() {
        SceneManager.LoadScene("MenuScene");
    }

    /// @brief Retour du canva passé en paramètre vers le canva jeu
    /// @param canva: Le canva dont on veut revenir
    private void backFromCanva(Canvas canva) {
        normalCanva.gameObject.SetActive(true);
        canva.gameObject.SetActive(false);
        Joueur curr_player = joueurs[curr_player_id];
        nom_joueur.text = curr_player.name;

        cardSlot.card.isSloted = false;
        cardSlot.card.moveToHand();
        cardSlot.card = null;
    }

    /// @brief Retour du canva de cafe
    public void backFromCafe(){
        backFromCanva(cafeCanva);
    }

    /// @brief Retour du canva de debat
    public void backFromeDebate(){
        backFromCanva(debateCanva);
    }

}
