package projet.Model;
import projet.Model.AppManager.Card;

// interface fonctionnelle pour les modes de jeu

public interface Validator {
    public Card validate(Card[] cartes);
}
