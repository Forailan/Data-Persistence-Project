using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text textPrefab;
    public int scoreCount = 5;

    // Start is called before the first frame update
    void Start()
    {
        if (DataManager.Instance != null)
        {
            var data = DataManager.Instance.saveData;
            var i = 1;
            foreach (var score in data.ScoreData)
            {
                var text = Instantiate(textPrefab, transform);
                text.text = $"{i++}: {score.Name} - {score.Score}";     
                if(i > 5)
                {
                    break;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
