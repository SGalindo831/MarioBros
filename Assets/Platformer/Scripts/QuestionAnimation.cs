using UnityEngine;

public class QuestionBlockAnimator : MonoBehaviour
{
    public float animationSpeed = 0.2f;
    private Material blockMaterial;
    private int currentFrame = 0;
    private float timer = 0f;
    private readonly int totalFrames = 5;

    void Start()
    {
        //Get the material
        blockMaterial = GetComponent<Renderer>().material;
    }

    void Update()
    {
        timer += Time.deltaTime;

        //Change frame when timer exceeds animation speed
        if (timer >= animationSpeed)
        {
            timer = 0f;
            currentFrame = (currentFrame + 1) % totalFrames;

            //Uodate the texture offset 
            float yOffset = -0.2f * currentFrame;
            blockMaterial.mainTextureOffset = new Vector2(0, yOffset);
        }
    }
}