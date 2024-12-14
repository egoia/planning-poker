using UnityEngine;

/// @class Card
/// @brief Représente les cartes du jeu
///
/// Les cartes permettent d'exprimer des estimations pour la planification de tâches

public class Card{
    
    /// @brief Carte avec la valeur "0"
    public static readonly Card zero = new Card("0");
    /// @brief Carte avec la valeur "1"
    public static readonly Card un = new Card("1");
    /// @brief Carte avec la valeur "2"
    public static readonly Card deux = new Card("2");
    /// @brief Carte avec la valeur "3"
    public static readonly Card trois = new Card("3");
    /// @brief Carte avec la valeur "5"
    public static readonly Card cinq = new Card("5");
    /// @brief Carte avec la valeur "8"
    public static readonly Card huit = new Card("8");
    /// @brief Carte avec la valeur "13"
    public static readonly Card treize = new Card("13");
    /// @brief Carte avec la valeur "20"
    public static readonly Card vingt = new Card("20");
    /// @brief Carte avec la valeur "40"
    public static readonly Card quarante = new Card("40");
    /// @brief Carte avec la valeur "100"
    public static readonly Card cent = new Card("100");
    /// @brief Carte joker, estimation indéterminée
    public static readonly Card joker = new Card("?");
    /// @brief Carte "café", utilisée pour indiquer vouloir une pause
    public static readonly Card cafe = new Card("cafe");

    /// @brief Représentation textuelle de la carte
    private string string_value;

    /// @brief Chemin de l'image associée à la carte
    private string image;

    /// @brief Pool de cartes disponibles avec des valeurs numériques
    /// il sert à mapper un int avec une carte (cf. fonction numberToCard())
    private static Card[] cardPool = {zero, un , deux, trois, cinq, huit, treize, vingt, quarante, cent};

    /// @brief Contructeur de Carte
    /// @param s: Valeur textuelle de la carte
    private Card(string s)
    {
        this.string_value=s;
    }

    /// @brief Second constructeur de Carte
    /// @param v: Valeur textuelle de la carte
    /// @param image: Chemin de l'image associée
    private Card(string v, string image) : this(v)
    {
        this.image = image;
    }

    /// @brief Retourne la représentation textuelle de la carte
    /// @return: La représentation textuelle de la carte
    public string toString()
    {
        return this.string_value;
    }

    /// @brief Convertit la carte en une valeur int
    /// @return: La valeur entière correspondante, ou une valeur spéciale :
    /// - -2 pour la carte "café"
    /// - -1 pour le joker
    public int toInt()
    {
        if(this == cafe) return -2;
        else if(this == joker) return -1;
        else return int.Parse(this.string_value);
    }
    
    /// @brief Associe une valeur numérique à la carte correspondante dans le pool
    /// @param v: Valeur numérique à associer
    /// @return: La carte dont la valeur est la plus proche de v
    /// Retourne 100 si la valeur dépasse le maximum du pool
    public static Card numberToCard(float v)
    {
        if(v==-1) return joker;
        if(v==-2) return cafe;
        for(int i =1; i<cardPool.Length; i++)
        {
            int n1 = cardPool[i-1].toInt();
            int n2 = cardPool[i].toInt();
            if(n1<=v && v<=n2)
            {
                float diff1 = v-n1;
                float diff2 = n2-v;
                Card res = diff1 < diff2 ? cardPool[i-1] : cardPool[i];
                return res;
            }
        }
        return cent; 
    }
}