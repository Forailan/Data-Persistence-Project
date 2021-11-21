using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField]
    private TMP_Text bestScoreText;
    [SerializeField]
    private TMP_InputField nameInput;

    // Start is called before the first frame update
    void Start()
    {
        if (DataManager.Instance != null)
        {
            var data = DataManager.Instance.saveData;
            if (data != null && data.ScoreData.Count > 0)
            {
                var bestPlayer = data.ScoreData[0];
                bestScoreText.gameObject.SetActive(true);
                bestScoreText.text = $"Best score: {bestPlayer.Name} - {bestPlayer.Score}";
            }
        }
    }

    public void StartNewGame()
    {
        if (!string.IsNullOrWhiteSpace(nameInput.text))
        {
            DataManager.Instance.userName = nameInput.text;
            SceneManager.LoadScene(1);
        }
        else
        {
            nameInput.placeholder.color = Color.red;
        }
    }

    public void HighScores()
    {
        SceneManager.LoadScene(2);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}