using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Manager : MonoBehaviour
{


    public const int columns = 7;
    public const int rows = 2;

    [SerializeField] private GameObject UPRight;
    [SerializeField] private GameObject DOWNLeft;
    [SerializeField] private GameObject[] cards;

    private int[] Randomiser(int[] location)
    {
        int[] array = location.Clone() as int[];
        for (int i = 0; i < array.Length; i++)
        {
            int newArray = array[i];
            int j = Random.Range(i, array.Length);
            array[i] = array[j];
            array[j] = newArray;
        }

        return array;
    }

    private Unknown firstOpen;
    private Unknown lastOpen;

    private int score = 0;
    private int attempts = 0;

    [SerializeField] private Text scoreText;
    [SerializeField] private Text attemptsText;
    [SerializeField] private Button restart;

    private void Start()
    {
        restart.onClick.AddListener(Restart);
        scoreText.text = "Score: " + score;
        attemptsText.text = "Attempts: " + attempts;

        Vector2 upRight = UPRight.transform.position;
        Vector2 downLeft = DOWNLeft.transform.position;

        int[] location = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6 };
        location = Randomiser(location);

        float xDelta = (upRight.x - downLeft.x) / columns;
        float yDelta = (upRight.y - downLeft.y) / rows;


        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                int index = j * columns + i;
                int id = location[index];
                Instantiate(cards[location[index]], new Vector2(downLeft.x + xDelta * i, downLeft.y + yDelta * j), Quaternion.identity);
            }
        }
    }



    public bool canOpen
    {
        get { return lastOpen == null; }
    }

    public void imageOpened(Unknown startObject)
    {
        if (firstOpen == null)
            firstOpen = startObject;
        else
        {
            lastOpen = startObject;
            StartCoroutine(CheckGuessed());
        }
    }

    private IEnumerator CheckGuessed()
    {
        if (firstOpen.tag == lastOpen.tag)
        {
            score++;
            scoreText.text = "Score: " + score;
        }
        else
        {
            yield return new WaitForSeconds(0.5f);

            firstOpen.Close();
            lastOpen.Close();
        }
        attempts++;
        attemptsText.text = "Attempts: " + attempts;

        firstOpen = null;
        lastOpen = null;
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }


}
