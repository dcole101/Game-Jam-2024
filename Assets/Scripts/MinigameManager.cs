using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MinigameManager : MonoBehaviour
{
    public GameObject leftCurtain;
    public GameObject rightCurtain;
    public GameObject levelUpText;

    public List<GameObject> hearts;
    public Sprite emptyHeart;

    public Canvas minigameCanvas;
    public MiniGameBase MiniGame;

    public GameObject sfxController;

    bool curtainOpenSoundPlayed;
    bool levelUpSoundPlayed;

    public Image jesterTimer;
    public float jesterSpeed = 5000f;
    public float lerpedX = 240;

    bool lerpStarted = false; 

    bool gameRunning;
    bool gameWon;
    bool isGameEndDelay;
    bool isClosingCurtains;
    bool isSwitchingGame;
    bool playedCurtainsOpenSound;

    float timeElapsed = 5;
    float gameEndDelay = 2;

    float timerTime = 0f;

    int minigameCount = 6;

    int health = 5;

    float speedModifier;

    int currentID = -1;
    List<int> availableIDs;

    float curtainSpeed = 600;


    public CameraAnimator camAnim;

    private IEnumerator coroutine;

    //king animations
    public GameObject happy;
    public GameObject bored;

    private void Start()
    {

        curtainOpenSoundPlayed = false;
        levelUpSoundPlayed = false;

        availableIDs = new List<int>();
        coroutine = KingHappy();

        speedModifier = 1.0f;
        for (int i = 0; i < minigameCount; i++)
        {
            availableIDs.Add(i);
        }

        gameWon = false;
        SwitchMiniGame();
        timerTime = MiniGame.timeLimit;

        camAnim.JesterCamAnim();
    }

    void Update()
    {
        //Play Minigame
        if (gameRunning)
        {
            int gameState = MiniGame.UpdateGame(sfxController, Time.deltaTime);

            //Debug.Log(MiniGame.timeLimit/timerTime);

            // Calculate the lerp position based on the elapsed time

            lerpedX = Mathf.Lerp(-240, 240, MiniGame.timeLimit / timerTime);


            // Update the position of the UI Image
            jesterTimer.transform.position = new Vector2(lerpedX, 35);

            if (gameState != 0)
            {
                if (gameState == 1)
                {
                    gameWon = true;
                    sfxController.GetComponent<AudioSource>().PlayOneShot(sfxController.GetComponent<AudioController>().sfx[Random.Range(15, 18)], 0.85f);
                }
                else
                {
                    gameWon = false;
                    sfxController.GetComponent<AudioSource>().PlayOneShot(sfxController.GetComponent<AudioController>().sfx[Random.Range(13, 15)], 0.85f);
                    health--;
                    hearts[health].GetComponent<Image>().sprite = emptyHeart;
                }

                Debug.Log(health);

                timeElapsed = 0;
                gameRunning = false;
                isGameEndDelay = true;

                camAnim.KingCamAnim();
            }
        }
        //Delay after game
        else if (timeElapsed < gameEndDelay && isGameEndDelay)
        {
            timeElapsed += Time.deltaTime;


            if (gameWon)
            {
                //victory effect
                //StartCoroutine(coroutine);
                

                happy.SetActive(true);
                bored.SetActive(false);
            }
            else
            {
               
                //Lost Effect
            }

            if (timeElapsed >= gameEndDelay)
            {
                sfxController.GetComponent<AudioSource>().PlayOneShot(sfxController.GetComponent<AudioController>().sfx[6], 0.7f);
                isGameEndDelay = false;
                isClosingCurtains = true;
            }
        }
        //Close curtains
        else if (isClosingCurtains)
        {
            leftCurtain.GetComponent<Transform>().position += new Vector3(curtainSpeed * Time.deltaTime, 0, 0);
            rightCurtain.GetComponent<Transform>().position += new Vector3(curtainSpeed * Time.deltaTime * -1, 0, 0);

            //Debug.Log(leftCurtain.GetComponent<Transform>().position.x + " ; " + rightCurtain.GetComponent<Transform>().position.x);

            if (leftCurtain.GetComponent<Transform>().position.x >= 550 - (leftCurtain.GetComponent<RectTransform>().rect.width * leftCurtain.GetComponent<RectTransform>().localScale.x / 2) && rightCurtain.GetComponent<Transform>().position.x <= 530 + (rightCurtain.GetComponent<RectTransform>().rect.width * rightCurtain.GetComponent<RectTransform>().localScale.x / 2))
            {
                isClosingCurtains = false;
                isSwitchingGame = true;
                timeElapsed = 0;
            }
        }
        //Check if game over
        else if (health <= 0)
        {
            SceneManager.LoadScene("GameOverScreen");
        }
        else if (availableIDs.Count <= 0 && isSwitchingGame)
        {




            timeElapsed += Time.deltaTime;
            jesterTimer.gameObject.SetActive(false);

            if (timeElapsed > 0.5)
            {
                if (levelUpSoundPlayed == false)
                {
                    sfxController.GetComponent<AudioSource>().PlayOneShot(sfxController.GetComponent<AudioController>().sfx[10]);
                    levelUpSoundPlayed = true;
                }

                levelUpText.SetActive(true);
            }
            if (timeElapsed > 1.5)
            {
                levelUpText.SetActive(false);
            }

            //Level UP effects
            if (timeElapsed > 2)
            {
                LevelUp();
                levelUpSoundPlayed = false;
                jesterTimer.gameObject.SetActive(true);
            }
        }
        //Switch MiniGame
        else if (isSwitchingGame)
        {
            camAnim.JesterCamAnim();

            isSwitchingGame = false;
            SwitchMiniGame();
            jesterTimer.transform.position = new Vector2(240, 45);

            timerTime = MiniGame.timeLimit;
        }
        //Open Curtains
        else
        {
            leftCurtain.GetComponent<Transform>().position -= new Vector3(curtainSpeed * Time.deltaTime, 0, 0);
            rightCurtain.GetComponent<Transform>().position -= new Vector3(curtainSpeed * Time.deltaTime * -1, 0, 0);

            if (curtainOpenSoundPlayed == false)
            {
                sfxController.GetComponent<AudioSource>().PlayOneShot(sfxController.GetComponent<AudioController>().sfx[6], 0.7f);
                curtainOpenSoundPlayed = true;
                Debug.Log("SOUND");
            }

            if (leftCurtain.GetComponent<Transform>().position.x <= -20 - (leftCurtain.GetComponent<RectTransform>().rect.width * leftCurtain.GetComponent<RectTransform>().localScale.x / 2) && rightCurtain.GetComponent<Transform>().position.x >= 1100 + (rightCurtain.GetComponent<RectTransform>().rect.width * rightCurtain.GetComponent<RectTransform>().localScale.x / 2))
            {
                curtainOpenSoundPlayed = false;
                gameRunning = true;

                happy.SetActive(false);
                bored.SetActive(true);
            }
        }
    }

    void SwitchMiniGame()
    {
        Debug.Log("Switching MiniGame");
        int gameID = GetRandomGameID();

        if (MiniGame != null)
        {
            MiniGame.ResetGame();
        }

        switch (gameID)
        {
            case 0:
                MiniGame = new WhackMole();
                break;
            case 1:
                MiniGame = new RockPaperScissors();
                break;
            case 2:
                MiniGame = new HoopJump();
                break;
            case 3:
                MiniGame = new DontDrinkPoison();
                break;
            case 4:
                MiniGame = new CutTheSteak();
                break;
            case 5:
                MiniGame = new CatchTheTips();
                break;
        }

        MiniGame.SetupGame(minigameCanvas, speedModifier);

    }

    int GetRandomGameID()
    {

        int idPos = Random.Range(0, availableIDs.Count);
        int newGameID = availableIDs[idPos];

        if (newGameID == currentID)
        {
            return GetRandomGameID();
        }
        currentID = newGameID;
        availableIDs.RemoveAt(idPos);
        return newGameID;
    }

    void LevelUp()
    {
        for (int i = 0; i < minigameCount; i++)
        {
            availableIDs.Add(i);
        }

        Debug.Log("LEVEL UP");
        speedModifier += 0.2f;



    }

    IEnumerator KingHappy()
    {
        // Set the initial state
        happy.SetActive(true);
        bored.SetActive(false);

        Debug.Log("King Happy!");

        // Wait for 6 seconds
        yield return new WaitForSeconds(2);

        // Reverse the state after 6 seconds
        happy.SetActive(false);
        bored.SetActive(true);
    }
}


