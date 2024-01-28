using UnityEngine;
using System.Collections;


public class KingAnimationController : MonoBehaviour
{
    public GameObject happy;
    public GameObject bored;


    void Start()
    {
   
        StartCoroutine(KingHappy());
    }

    IEnumerator KingHappy()
    {
        // Set the initial state
        happy.SetActive(true);
        bored.SetActive(false);

        Debug.Log("King Happy!");

        // Wait for 6 seconds
        yield return new WaitForSeconds(6f);

        // Reverse the state after 6 seconds
        happy.SetActive(false);
        bored.SetActive(true);
    }
}

// if (!isCoroutineRunning)
//{
    // Start the coroutine
    //StartCoroutine(KingHappyCoroutine());
//}