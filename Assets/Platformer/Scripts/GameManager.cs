using UnityEngine;
using TMPro;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI scoreText;

    private int coins = 0;
    private int score = 0;

    // Update is called once per frame
    void Update()
    {
        int timeLeft = 100 - (int)(Time.time);

        // Check if time has run out
        if (timeLeft <= 0)
        {
            timeLeft = 0;
            HandleTimeRunOut();
        }

        timerText.text = $"Time: {timeLeft}";

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;

                if (clickedObject.CompareTag("Brick"))
                {
                    Destroy(clickedObject);
                    score += 100;
                }
                else if (clickedObject.CompareTag("Question"))
                {
                    coins += 1;
                    score += 150;
                }

                UpdateUI();
            }
        }
    }
    public void AddCoin()
    {
        coins += 1;
        UpdateUI();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateUI();
    }
    private void UpdateUI()
    {
        coinText.text = $"{coins}";
        scoreText.text = $"{score}";
    }
    private bool timeRunOut = false;
    private void HandleTimeRunOut()
    {
        if (timeRunOut)
            return;

        timeRunOut = true;

        Debug.LogWarning("GAME OVER: Player didn't complete the level in time.");
    }
}