using UnityEngine;

public class CafeController : MonoBehaviour
{
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            GameObject.Find("GameManager").GetComponent<GameController>().backFromCafe();
        }
    }
}