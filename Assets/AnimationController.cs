using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public GameObject[] objectsToAnimate;
    private int currentIndex = 0;
    public float delayBetweenObjects = 20f;

    private void Start()
    {
        // Start the animation loop
        InvokeRepeating("AnimateObjects", 0f, delayBetweenObjects);
    }

    private void AnimateObjects()
    {
        // Turn off the previous object
        if (currentIndex > 0)
        {
            objectsToAnimate[currentIndex - 1].SetActive(false);
        }

        // Turn on the current object
        if (currentIndex < objectsToAnimate.Length)
        {
            objectsToAnimate[currentIndex].SetActive(true);
            Debug.Log("Next Jester Animation Played");
            currentIndex++;
        }
        else
        {
            // If we've reached the end, restart the animation
            currentIndex = 0;
            // Optionally, you can stop the animation by calling CancelInvoke("AnimateObjects");
        }
    }
}
