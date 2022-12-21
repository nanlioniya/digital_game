using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountDownTimer : MonoBehaviour
{
    [SerializeField]
    private Text uiText;

    public int Duration;

    private int remainDuration;

    //private PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        Being(Duration);
    }

    private void Being(int Second)
    {
        remainDuration = Second;
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (remainDuration >= 0)
        {
            uiText.text = $"{remainDuration / 60:00} : {remainDuration % 60:00}";
            remainDuration--;
            yield return new WaitForSeconds(1f);
        }
        OnEnd();
    }

    private void OnEnd()
    {
        print("Game End.");
        SceneManager.LoadScene(2);
    }
}
