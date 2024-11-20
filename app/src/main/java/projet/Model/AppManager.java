package projet.Model;

import com.fasterxml.jackson.core.exc.StreamReadException;
import com.fasterxml.jackson.core.exc.StreamWriteException;
import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.DatabindException;
import com.fasterxml.jackson.databind.ObjectMapper;

import java.io.File;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Collections;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class AppManager {
    public enum Card{
        zero("0"), un("1"), deux("2"), trois("3"), cinq("5"), huit("8"), treize("13"), vingt("20"), quarante("40"), cent("100"), joker("?"), cafe("cafe");

        private String string_value;

        private Card(String s){
            this.string_value=s;
        }
        public String toString(){
            return this.string_value;
        }
        public int toInt(){
            if(this == cafe) return -2;
            else if(this == joker) return -1;
            else return Integer.valueOf(this.string_value);
        }
        // à finir !!
        public static Card intToCard(int n){
            if(n==0) return zero;
            if(n==1) return un;
            if(n==2) return deux;
            if(n==3) return trois;
            if(n<6) return cinq;
            return cafe;
        }
    }
    
    private ArrayList<Joueur> joueurs;
    private ArrayList<Fonctionnalite> fonctionnalites;
    private String file;
    private int current_fonc = 0;
    private int tour = 1;

    private final static Validator strict = (Card[] cartes) -> {
        for(int i = 1; i<cartes.length; i++) {
            if(cartes[i] != cartes[i-1]) return null;
        }
        return cartes[0];
    };
    private final static Validator moyenne = (Card[] cartes) -> {
        int somme = 0;
        int nb_cartes = cartes.length;
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

        int moyenne = somme/nb_cartes;
        return Card.intToCard(moyenne);
    };
    private final static Validator mediane = (Card[] cartes) -> {
        int nb_cafes = 0;
        int nb_jokers = 0;
        ArrayList cartes_tmp = new ArrayList<Integer>();
        for (Card c : cartes) {
            switch(c) {
                case cafe : nb_cafes++; break;
                case joker : nb_jokers++; break;
                default : cartes_tmp.add(c.toInt());
            }
        }
        int nb_cartes = cartes_tmp.size();
        Collections.sort(cartes_tmp);
        int mediane;
        if(nb_cartes%2==1) mediane = (int)(cartes_tmp.get((nb_cartes/2)+1));
        else{
            int v1 = (int)(cartes_tmp.get((nb_cartes/2)+1));
            int v2 = (int)(cartes_tmp.get(nb_cartes/2));
            mediane = (v1+v2)/2;
        }
        return Card.intToCard(mediane);
    };
    private final static Validator majorite_absolue = (Card[] cartes) -> {
        Map<Card, Integer> map = new HashMap<>();
        Card max_card= cartes[0];
        for (Card c : cartes) {
            map.compute(c, (key, value) -> value == null ? 1 : value + 1);
            if(max_card!=c){
                max_card = map.get(max_card) < map.get(c) ? c : max_card; 
            }
        }
        int nb_cartes = cartes.length;
        if(map.get(max_card) > nb_cartes/2) {
            return max_card;
        }
        return null;
    };
    private final static Validator majorite_relative = (Card[] cartes) -> {
        Map<Card, Integer> map = new HashMap<>();
        Card max_card= cartes[0];
        for (Card c : cartes) {
            if(c!=Card.joker){
                map.compute(c, (key, value) -> value == null ? 1 : value + 1);
                if(max_card!=c){
                    max_card = map.get(max_card) < map.get(c) ? c : max_card; 
                }
            } 
        }
        int max_rec = map.get(max_card);
        map.remove(max_card);
        if(map.containsValue(max_rec)) return null;
        return max_card;
    };

    public AppManager(ArrayList<Joueur> joueurs, String file) throws StreamReadException, DatabindException, IOException{
        this.file=file;
        this.joueurs = joueurs;
        ObjectMapper objectMapper = new ObjectMapper();
        fonctionnalites = objectMapper.readValue(new File(this.file), new TypeReference<ArrayList<Fonctionnalite>>() {});
    }

    public void save() {
        File outputFile = new File(this.file);
        ObjectMapper om = new ObjectMapper();
        try {
            om.writeValue(outputFile, this.fonctionnalites);
        } catch (StreamWriteException e) {
            e.printStackTrace();
        } catch (DatabindException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public Fonctionnalite getCurrent() {
        return fonctionnalites.get(current_fonc);
    }

    public boolean joue_tour(ArrayList<Card> cartes) {

        tour++;
    }

}
