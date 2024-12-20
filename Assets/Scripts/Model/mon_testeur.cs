using UnityEngine;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System;
using System.IO;
using PlanningPoker;

public class mon_testeur : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string testFilePath = Path.Combine(Application.dataPath, "test_fonctionnalites.json");
        Fonctionnalite f1 = new Fonctionnalite();
        f1.setNom( "Fonctionnalité 1");
        f1.setDescription("Description 1");

        Fonctionnalite f2 = new Fonctionnalite();
        f2.setNom( "Fonctionnalité 2");
        f2.setDescription("Description 2");

        Fonctionnalite f3 = new Fonctionnalite();
        f3.setNom( "Fonctionnalité 3");
        f3.setDescription("Description 3");
        
        var fonctionnalites = new[]{f1,f2,f3};
        string jsonData = JsonUtility.ToJson(new FonctionnaliteWrapper { fonctionnalites = fonctionnalites }, true);
        File.WriteAllText(testFilePath, jsonData);

        /*
        Card[] cartes = {Card.cinq, Card.cinq, Card.cinq};
        Card res = Validator.strict(cartes);
        if(Card.cinq!= res) Debug.Log("Validator.strict devrait retourner la carte commune");
        else Debug.Log("test passé bien 1");

        Card[] cartes = { Card.cinq, Card.deux, Card.cinq};
        Card res = Validator.strict(cartes);
        if(res!=null) Debug.Log("Validator.strict devrait retourner null s'il y a une carte différente des autres");
        else Debug.Log("test passé bien 2");

        Card[] cartes = {Card.trois, Card.cinq, Card.treize, Card.trois, Card.joker};
        Card res = Validator.moyenne(cartes);
        if(Card.cinq!= res) Debug.Log("Validator.moyenne ne calcule pas correctement la moyenne");
        else Debug.Log("test passé bien 3");

        Card[] cartes = {Card.trois, Card.cinq, Card.treize, Card.trois, Card.joker};
        Card res = Validator.mediane(cartes);
        if(Card.cinq.toInt()!= res.toInt()) Debug.Log("Validator.mediane ne calcule pas correctement la mediane");
        else Debug.Log("test passé bien 4");

        Card[] cartes = {Card.trois, Card.cinq, Card.trois, Card.treize, Card.trois, Card.trois, Card.joker};
        Card res = Validator.majorite_absolue(cartes);
        if(Card.trois!= res) Debug.Log("Validator.majorite_absolue ne calcule pas correctement la majorité absolue");
        else Debug.Log("test passé bien 5");

        Card[] cartes = {Card.huit, Card.cinq, Card.huit, Card.trois, Card.cafe};
        Card res = Validator.majorite_relative(cartes);
        if(Card.huit!= res) Debug.Log("Validator.majorite_relative ne calcule pas correctement la majorité relative");
        else Debug.Log("test passé bien 6");

        Card[] cartes = {Card.huit, Card.cinq, Card.huit, Card.cinq, Card.cafe};
        Card res = Validator.majorite_relative(cartes);
        if(res!=null) Debug.Log("Validator.majorite_relative devrait renvoyer null s'il n'y a pas de majorité");
        else Debug.Log("test passé bien 7");
        /*
        if(!File.Exists(testFilePath)) Debug.Log("Le fichier de test n'a pas été créé correctement.");
        string content = File.ReadAllText(testFilePath);
        Debug.Log(content);
        if(string.IsNullOrEmpty(content)) Debug.Log("Le fichier JSON généré est vide.");
        Debug.Log("Fichier JSON de test généré avec succès : " + testFilePath);
        Debug.Log("Contenu du fichier JSON : " + content);
        Debug.Log("Contenu du fichier JSON : " + content);
        var appManager = new AppManager(testFilePath, Validator.moyenne);
        Card[] cartes = { Card.huit };
        appManager.joue_tour(cartes);
        appManager.save();
        if(!File.Exists(testFilePath)) Debug.Log("Le fichier n'a pas été créé après la sauvegarde.");
        var reloadedManager = new AppManager(testFilePath, Validator.moyenne);
        if("Fonctionnalité 2" != reloadedManager.getCurrent().getNom()) Debug.Log("Le gestionnaire ne devrait pas perdre sa progression après un sauvegarde/chargement.");

        appManager = new AppManager(testFilePath, Validator.moyenne);
        Card[] cartes2 = { Card.cinq, Card.cinq, Card.cinq };
        int res = appManager.joue_tour(cartes2);
        appManager.save();
        if(1!= res) Debug.Log("AppManager.joue_tour n'a pas mis à jour correctement la fonctionnalité");
        else Debug.Log("c'est ok jusquici");
        Debug.Log(appManager.getCurrent().getNote());
        if("5"!= appManager.getCurrent().getNote()) Debug.Log("La note de la fonctionnalité n'a pas été correctement mise à jour");
        else Debug.Log("c'est ok jusquici 2222");
        */

        /* Debug.Log("TEST test_joueTour_maj_fonctionnalites()");
        var appManager = new AppManager(testFilePath, Validator.moyenne);
        Card[] cartes = { Card.cinq, Card.cinq, Card.cinq };
        int res = appManager.joue_tour(cartes);
        appManager.save();
        if(1!= res) Debug.Log("AppManager.joue_tour n'a pas mis à jour correctement la fonctionnalité");
        else Debug.Log("test passé");
        */

        /*
        Debug.Log("TEST test_NumberToCard()");
        Card res = Card.numberToCard(8);
        if(Card.huit!=res) Debug.Log("Card.numberToCard() ne retourne pas la bonne carte");
        
        Debug.Log("TEST test_NumberToCard_valeur_approximative()");
        int value = 15;
        res = Card.numberToCard(value);
        if(Card.treize!=res) Debug.Log("Card.numberToCard() ne retourne pas la carte à la valeur la plus proche");
        */

        var appManager = new AppManager(testFilePath, Validator.moyenne);
        Card[] cartes = { Card.huit };
        Card c; 
        appManager.joue_tour(cartes, out c);
        appManager.save();
        Assert.IsTrue(File.Exists(testFilePath), "Le fichier n'a pas été créé après la sauvegarde.");
        var reloadedManager = new AppManager(testFilePath, Validator.moyenne);
        Assert.AreEqual("Fonctionnalité 2", reloadedManager.getCurrent().getNom(), "Le gestionnaire ne devrait pas perdre sa progression après un sauvegarde/chargement.");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
