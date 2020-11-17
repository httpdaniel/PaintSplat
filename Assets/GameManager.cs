using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text winnerName;
    [SerializeField]
    public GameObject endScreen;

    void Start() {
        // Set name to blank
        winnerName.text = "";

        // Hide end screen
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

    public void Restart() {
        // restart the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit() {
        // quit the game
        Application.Quit();
    }
}
