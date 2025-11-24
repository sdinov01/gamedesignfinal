using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI stageScore;
    [SerializeField] private TextMeshProUGUI targetText;

    public int tscore = 0;
    public int sscore = 0;
    public int target;
    public int stage = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        target = 4;
        UpdateScoreDisplay();
        NewStage();
    }

    public void AddPoint()
    {
        tscore++;
        sscore++;
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Total Score: " + tscore;
        }
        if (stageScore != null)
        {
            stageScore.text = "Stage Score: " + sscore;
        }
    }

    public void NewStage()
    {
        if (targetText != null)
        {
            targetText.text = "Stage target: " + target;
        }
    }
}