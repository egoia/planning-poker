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

        
        Debug.Log("TEST test_NumberToCard()");
        Card res = Card.numberToCard(8);
        Assert.AreEqual(Card.huit, res, "Card.numberToCard() ne retourne pas la bonne carte");
        
        Debug.Log("TEST test_NumberToCard_valeur_approximative()");
        int value = 15;
        Card res = Card.numberToCard(value);
        Assert.AreEqual(Card.treize, res, "Card.numberToCard() ne retourne pas la carte à la valeur la plus proche");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
