using System.Collections.Generic;
using UnityEngine;

public class Validator 
{
     public delegate Card Validate(Card[] cartes);

    public static readonly Validate strict = (Card[] cartes) => {
        for(int i = 1; i<cartes.Length; i++) {
            if(cartes[i] != cartes[i-1]) return null;
        }
        return cartes[0];
    };

    public static readonly Validate moyenne = (Card[] cartes) => {
        int somme = 0;
        int nb_cartes = cartes.Length;
        int nb_cafes = 0;
        int nb_jokers = 0;
        for(int i = 0; i<nb_cartes; i++){
            int val = cartes[i].toInt();
            switch(val) {
                case -2 : nb_cafes++; break;
                case -1 : nb_jokers++; break;
                default : somme+=val; break;
            }
        }
        if(nb_cafes >= nb_cartes/2) return Card.cafe;
        if(nb_jokers == nb_cartes) return Card.joker;
        nb_cartes-=(nb_jokers+nb_cafes);

        float moyenne = somme/nb_cartes;
        return Card.numberToCard(moyenne);
    };
    public static readonly Validate mediane = (Card[] cartes) => {
        int nb_cafes = 0;
        int nb_jokers = 0;
        List<int> cartes_tmp = new List<int>();
        foreach (Card c in cartes) {
            if(c==Card.cafe)nb_cafes++;
            else if (c==Card.joker)nb_jokers++;
            else cartes_tmp.Add(c.toInt());
        }
        int nb_cartes = cartes_tmp.Count;
        cartes_tmp.Sort();
        int mediane;
        if(nb_cafes>nb_cartes)return Card.cafe;
        if(nb_cartes==0) return Card.joker;
        if(nb_cartes%2==1) mediane = (int)(cartes_tmp[((nb_cartes/2)+1)]);
        else{
            int v1 = (int)(cartes_tmp[(nb_cartes/2)+1]);
            int v2 = (int)(cartes_tmp[(nb_cartes/2)]);
            mediane = (v1+v2)/2;
        }
        return Card.numberToCard(mediane);
    };
    public static readonly Validate majorite_absolue = (Card[] cartes) => {
        Dictionary<Card, int> map = new Dictionary<Card, int>();
        Card max_card= cartes[0];
        foreach (Card c in cartes) {
            if(map.ContainsKey(c)){
                map[c] += 1;
            }
            else map[c]=1;
            
            if(max_card!=c){
                max_card = map[max_card] < map[c] ? c : max_card; 
            }
        }
        int nb_cartes = cartes.Length;
        if(map[max_card] > nb_cartes/2) {
            return max_card;
        }
        return null;
    };
    public static readonly Validate majorite_relative = (Card[] cartes) => {
        Dictionary<Card, int> map = new Dictionary<Card, int>();
        Card max_card= cartes[0];
        foreach (Card c in cartes) {
            if(c!=Card.joker){
                if(map.ContainsKey(c)){
                    map[c] += 1;
                }
                else map[c]=1;
                if(max_card!=c){
                   max_card = map[max_card] < map[c] ? c : max_card; 
                }
            } 
        }
        int max_rec = map[max_card];
        map.Remove(max_card);
        if(map.ContainsValue(max_rec)) return null;
        return max_card;
    };
}