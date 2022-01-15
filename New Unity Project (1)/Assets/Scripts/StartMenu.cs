using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] GameObject titleCatch;
    [SerializeField] GameObject titleCow;
    [SerializeField] GameObject titleAnd;
    [SerializeField] GameObject titleHorse;
    [SerializeField] GameObject Buttons;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        InvokeRepeating("StartTitle", 1f, 1f);
    }
    public void ClickedStartButton()
    {
        SceneManager.LoadScene(1);
    }
    public void ClickedQuitButton()
    {
        // save any game data here
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
    void StartTitle()
    {
        if (titleHorse.activeSelf)
        {
            Buttons.SetActive(true);
        }
        if (titleCow.activeSelf)
        {
            titleHorse.SetActive(true);
        }

        if (titleCatch.activeSelf)
        {
            titleCow.SetActive(true);
            titleAnd.SetActive(true);
        }
        titleCatch.SetActive(true);
    }
}
