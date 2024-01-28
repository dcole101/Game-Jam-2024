using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public GameObject[] gameObjects; // Array to hold your game objects

    void Update()
    {
        // Check for key presses
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleGameObject(0);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            ToggleGameObject(1);
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            ToggleGameObject(2);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            ToggleGameObject(3);
        }
    }

    void ToggleGameObject(int index)
    {
        // Ensure the index is within the array bounds
        if (index >= 0 && index < gameObjects.Length)
        {
            // Toggle the visibility of the selected game object
            gameObjects[index].SetActive(!gameObjects[index].activeSelf);
        }
        else
        {
            Debug.LogWarning("Invalid index: " + index);
        }
    }
}
