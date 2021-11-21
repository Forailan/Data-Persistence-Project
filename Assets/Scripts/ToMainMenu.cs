using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMainMenu : MonoBehaviour
{
    public void Back()
    {
        SceneManager.LoadScene(0);
    }
}
