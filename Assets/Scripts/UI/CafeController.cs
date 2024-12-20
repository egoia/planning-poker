using UnityEngine;

/// @class CafeController
/// @brief Permet d'attendre le retour du joueur après une pause café
/// 
/// Cette classe est un composant utilisé lorsque le canva Café est activé
/// Il attend qu'on appuie sur la barre d'espace pour reprendre la partie

public class CafeController : MonoBehaviour
{
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            GameObject.Find("GameManager").GetComponent<GameController>().backFromCafe();
        }
    }
}