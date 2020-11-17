using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text winnerName;
    [SerializeField]
    public GameObject endScreen;

    void Start() {
        winnerName.text = "";
        endScreen.SetActive(false);
    }
    //public Text playerName;
    void Update() {
        //check time
        // if time is up - end game
    }

    void setName() {
        //winnerName.text = name;
    }

    void endGame()
    {
        Debug.Log("Game Over");
        // change text to winner
        endScreen.SetActive(true);
    }
}
