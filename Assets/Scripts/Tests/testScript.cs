using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System;
using System.IO;
using PlanningPoker;

[TestFixture]
public class testScript
{

    // Tests pour Validator

    [Test]
    public void test_mode_strict_cartes_identiques()
    {
        Card[] cartes = {Card.cinq, Card.cinq, Card.cinq};
        Card res = Validator.strict(cartes);
        Assert.AreEqual(Card.cinq, res, "Validator.strict devrait retourner la carte commune");
    }

    [Test]
    public void test_mode_strict_cartes_differentes()
    {
        Card[] cartes = { Card.cinq, Card.deux, Card.cinq};
        Card res = Validator.strict(cartes);
        Assert.IsNull(res, "Validator.strict devrait retourner null s'il y a une carte différente des autres");
    }

    [Test]
    public void test_mode_moyenne()
    {
        Card[] cartes = {Card.trois, Card.cinq, Card.treize, Card.trois, Card.joker};
        Card res = Validator.moyenne(cartes);
        Assert.AreEqual(Card.cinq, res, "Validator.moyenne ne calcule pas correctement la moyenne");
    }

    [Test]
    public void test_mode_mediane()
    {
        Card[] cartes = {Card.trois, Card.cinq, Card.treize, Card.trois, Card.joker};
        Card res = Validator.mediane(cartes);
        Assert.AreEqual(Card.cinq.toInt(), res.toInt(), "Validator.mediane ne calcule pas correctement la mediane");
    }

    [Test]
    public void test_mode_majorite_absolue()
    {
        Card[] cartes = {Card.trois, Card.cinq, Card.trois, Card.treize, Card.trois, Card.trois, Card.joker};
        Card res = Validator.majorite_absolue(cartes);
        Assert.AreEqual(Card.trois, res, "Validator.majorite_absolue ne calcule pas correctement la majorité absolue");
    }

    [Test]
    public void test_mode_majorite_relative()
    {
        Card[] cartes = {Card.huit, Card.cinq, Card.huit, Card.trois, Card.cafe};
        Card res = Validator.majorite_relative(cartes);
        Assert.AreEqual(Card.huit, res, "Validator.majorite_relative ne calcule pas correctement la majorité relative");
    }

    [Test]
    public void test_mode_majorite_relative_nulle()
    {
        Card[] cartes = {Card.huit, Card.cinq, Card.huit, Card.cinq, Card.cafe};
        Card res = Validator.majorite_relative(cartes);
        Assert.IsNull(res, "Validator.majorite_relative devrait renvoyer null s'il n'y a pas de majorité");
    }    

    // Tests pour AppManager

    private string testFilePath;

    [SetUp]
    public void SetUp()
    {
        
        // Créer un fichier JSON temporaire pour les tests
        testFilePath = Path.Combine(Application.dataPath, "test_fonctionnalites.json");
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

        Assert.IsTrue(File.Exists(testFilePath), "Le fichier de test n'a pas été créé correctement.");
        string content = File.ReadAllText(testFilePath);
        Assert.IsFalse(string.IsNullOrEmpty(content), "Le fichier JSON généré est vide.");
        Debug.Log("Fichier JSON de test généré avec succès : " + testFilePath);
        Debug.Log("Contenu du fichier JSON : " + content);
    }

    [TearDown]
    public void TearDown()
    {
        // Supprimer le fichier de test après chaque test
        if (File.Exists(testFilePath))
        {
            File.Delete(testFilePath);
            Debug.Log("Fichier JSON de test supprimé : " + testFilePath);
        }
    }

    [Test]
    public void test_joueTour_maj_fonctionnalites()
    { 
        var appManager = new AppManager(testFilePath, Validator.moyenne);
        Card[] cartes = { Card.cinq, Card.cinq, Card.cinq };
        int res = appManager.joue_tour(cartes);
        Assert.AreEqual(1, res, "AppManager.joue_tour n'a pas mis à jour correctement la fonctionnalité");
        Assert.AreEqual("5", appManager.getCurrent().getNote(), "La note de la fonctionnalité n'a pas été correctement mise à jour");
    }

    [Test]
    public void test_joueTour_TermineToutesLesFonctionnalites()
    {
        var appManager = new AppManager(testFilePath, Validator.moyenne);
        Card[] cartes = { Card.cinq, Card.huit };
        while (appManager.getCurrent() != null)
        {
            appManager.joue_tour(cartes);
        }
        Assert.IsNull(appManager.getCurrent(), "AppManager.joue_tour n'a pas correctement progressé à travers toutes les fonctionnalités");
    }

    [Test]
    public void Test_Save()
    {
        var appManager = new AppManager(testFilePath, Validator.moyenne);
        Card[] cartes = { Card.huit };
        appManager.joue_tour(cartes);
        appManager.save();
        Assert.IsTrue(File.Exists(testFilePath), "Le fichier n'a pas été créé après la sauvegarde.");
        var reloadedManager = new AppManager(testFilePath, Validator.moyenne);
        Assert.AreEqual("Fonctionnalité 2", reloadedManager.getCurrent().getNom(), "Le gestionnaire ne devrait pas perdre sa progression après un sauvegarde/chargement.");
    }

    // Tests pour Card

    [Test]
    public void test_NumberToCard()
    {
        Card res = Card.numberToCard(8);
        Assert.AreEqual(Card.huit, res, "Card.numberToCard() ne retourne pas la bonne carte");
    }

    [Test]
    public void test_NumberToCard_valeur_approximative()
    {
        int value = 15;
        Card res = Card.numberToCard(value);
        Assert.AreEqual(Card.treize, res, "Card.numberToCard() ne retourne pas la carte à la valeur la plus proche");
    }

}
