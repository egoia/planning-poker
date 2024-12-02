using UnityEngine;

public class Card{
    
    public static readonly Card zero = new Card("0");
    public static readonly Card un = new Card("1");
    public static readonly Card deux = new Card("2");
    public static readonly Card trois = new Card("3");
    public static readonly Card cinq = new Card("5");
    public static readonly Card huit = new Card("8");
    public static readonly Card treize = new Card("13");
    public static readonly Card vingt = new Card("20");
    public static readonly Card quarante = new Card("40");
    public static readonly Card cent = new Card("100");
    public static readonly Card joker = new Card("?");
    public static readonly Card cafe = new Card("cafe");


    private string string_value;
    private string image;

    private static Card[] cardPool = {zero, un , deux, trois, cinq, huit, treize, vingt, quarante, cent};

    private Card(string s){
        this.string_value=s;
    }

    private Card(string v, string image) : this(v){
        this.image = image;
    }
    public string toString(){
        return this.string_value;
    }
    public int toInt(){
        if(this == cafe) return -2;
        else if(this == joker) return -1;
        else return int.Parse(this.string_value);
    }
    // à finir !!
    public static Card numberToCard(float v){
        for(int i =1; i<cardPool.Length; i++){
            int n1 = cardPool[i-1].toInt();
            int n2 = cardPool[i].toInt();
            if(n1<=v && v<=n2){
                float diff1 = v-n1;
                float diff2 = n2-v;
                Card res = diff1 < diff2 ? cardPool[i-1] : cardPool[i];
                return res;
            }
        }
        return cent; // si on a parcouru tout le tableau (sauf cent) sans trouver de carte compatible c'est que c'est plus grand que cent
    }
}