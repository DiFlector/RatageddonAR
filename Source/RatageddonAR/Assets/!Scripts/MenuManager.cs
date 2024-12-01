using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _scoreMenu;
    
    public void StartGame()
    {
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
    
    public void ShowScoreMenu()
    {
        _scoreMenu.SetActive(value: true);
    }
    
    public void HideScoreMenu()
    {
        _scoreMenu.SetActive(value: false);
    }
}
