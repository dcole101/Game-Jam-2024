using System.Collections;
using UnityEngine;

public class TextureChanger : MonoBehaviour
{
    public Material targetMaterial;  // Reference to the material you want to change
    public Texture[] textures;       // Array of textures to cycle through
    public float changeInterval = 5f;  // Time interval for texture change in seconds

    private int currentTextureIndex = 0;  // Index of the currently displayed texture

    private void Start()
    {
        // Start the coroutine to change textures
        StartCoroutine(ChangeTextureRoutine());
    }

    IEnumerator ChangeTextureRoutine()
    {
        while (true)
        {
            // Change the texture of the material
            ChangeTexture();

            // Wait for the specified interval before changing the texture again
            yield return new WaitForSeconds(changeInterval);
        }
    }

    void ChangeTexture()
    {
        // Log the texture change
        Debug.Log("Changing texture to: " + textures[currentTextureIndex].name);

        // Set the current texture to the material's main texture
        targetMaterial.mainTexture = textures[currentTextureIndex];

        // Move to the next texture in the array
        currentTextureIndex = (currentTextureIndex + 1) % textures.Length;
    }
}
