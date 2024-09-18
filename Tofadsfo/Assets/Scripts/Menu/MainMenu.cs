using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [SerializeField] Button _newGameButton;
    [SerializeField] Button _continueButton;
    [SerializeField] Button _quitButton;
    // Start is called before the first frame update
    void Start()
    {
        if( !GameSaver.LoadGameData())
        {
            GameSaver.CreateGameData();
        }
        if(GameSaver.GameData.level==GameSaver._tutoriallevelindex)
        {
            _continueButton.interactable = false;
        }
    }
    public void StartNewGame()
    {
        SceneManager.LoadScene(GameSaver._tutoriallevelindex);
    }

    public void Continue()
    {
        SceneManager.LoadScene(GameSaver.GameData.level);
    }
    public void ExitApplication()
    {
        Application.Quit();
    }
}
