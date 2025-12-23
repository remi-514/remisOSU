using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public AudioSource theMusic;
    public bool startPlaying;
    public BeatScroller theBS;

    public static GameManager instance;

    public int currentScore;
    public int scorePerNote = 100;
    public int scorePerGoodNote = 200;
    public int scorePerPerfectNote = 300;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI HitLabel;

    private Coroutine hitLabelRoutine;

    void Start()
    {
        instance = this;

        Color c = HitLabel.color;
        c.a = 0f;
        HitLabel.color = c;
    }

    void Update()
    {
        if (!startPlaying)
        {
            if (Keyboard.current.anyKey.isPressed)
            {
                startPlaying = true;
                theBS.hasStarted = true;
                theMusic.Play();
            }
        }
    }

    public void UpdateScore()
    {
        scoreText.text = currentScore.ToString();
    }


    public void longNoteHit(float accuracy)
    {
        if (accuracy >= 0.8f)
            PerfectHit();
        else if (accuracy >= 0.7f)
            GoodHit();
        else if (accuracy >= 0.5f)
            NormalHit();
        else
            NoteMissed();
    }

    public void NormalHit()
    {
        currentScore += scorePerNote;
        UpdateScore();

        HitLabel.text = "Meh!";
        HitLabel.color = Color.orange;

        PlayHitLabel();
    }

    public void GoodHit()
    {
        currentScore += scorePerGoodNote;
        UpdateScore();

        HitLabel.text = "Good!";
        HitLabel.color = Color.yellow;

        PlayHitLabel();
    }

    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote;
        UpdateScore();

        HitLabel.text = "Perfect!";
        HitLabel.color = Color.green;

        PlayHitLabel();
    }

    public void NoteMissed()
    {
        HitLabel.text = "Missed!";
        HitLabel.color = Color.red;

        PlayHitLabel();
    }


    void PlayHitLabel()
    {
        if (hitLabelRoutine != null)
            StopCoroutine(hitLabelRoutine);

        hitLabelRoutine = StartCoroutine(HitLabelAnim());
    }

    IEnumerator HitLabelAnim()
    {
        RectTransform rt = HitLabel.rectTransform;
        
        Color c = HitLabel.color;
        c.a = 1f;
        HitLabel.color = c;
        rt.localScale = new Vector3(3f, 3f, 3f);

        float popDuration = 0.1f;
        float t = 0f;
        while (t < popDuration)
        {
            t += Time.deltaTime;
            float scale = Mathf.Lerp(0.7f, 2f, t / popDuration);
            rt.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }
        rt.localScale = Vector3.one * 2f;

        yield return new WaitForSeconds(0.2f);

        t = 0f;
        float fadeDuration = 0.25f;
        Vector3 startScale = rt.localScale;
        Vector3 endScale = Vector3.one * 1.2f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float progress = t / fadeDuration;
            
            c.a = Mathf.Lerp(1f, 0f, progress);
            HitLabel.color = c;
            
            rt.localScale = Vector3.Lerp(startScale, endScale, progress);
            yield return null;
        }
        
        c.a = 0f;
        HitLabel.color = c;
    }
}
