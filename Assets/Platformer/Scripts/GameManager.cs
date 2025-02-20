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
        int timeLeft = 300 - (int)(Time.time);
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

                coinText.text = $"{coins}";
                scoreText.text = $"{score}";
            }
        }

    }
}
