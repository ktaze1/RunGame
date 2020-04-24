using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float movSpeed = 2f;
    int score, hScore;
    public Text hScoreText, scoreText;
    bool lookingRight = true;

    public ParticleSystem effectPrefab;

    GameManager gameManager;
    Animator anim;
    public Transform rayOrigin;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        anim = GetComponent<Animator>();
        LoadHScore(); 
    }

    private void LoadHScore()
    {
        hScore = PlayerPrefs.GetInt("hScore");
        hScoreText.text = hScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.GameStarted) return;
        anim.SetTrigger("GameStarted");
        transform.position += transform.forward * Time.deltaTime * movSpeed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Turn();
        }

        CheckFalling();
    }

    private void CheckFalling()
    {
        if(Physics.Raycast(rayOrigin.position, new Vector3(0, -1, 0)))
        {
            anim.SetTrigger("Falling");
            gameManager.RestartGame();
        }
    }

    private void Turn()
    {
        if (lookingRight)
        {
            transform.Rotate(new Vector3(0, -90, 0));
        }
        else
            transform.Rotate(new Vector3(0, 90, 9));

        lookingRight = !lookingRight;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Crystal")
        {
            MakeScore();
            var effect = Instantiate(effectPrefab);
            effect.transform.position = other.transform.position;
            Destroy(effect, 1f);
            Destroy(other.gameObject);
        }
    }

    private void MakeScore()
    {
        score++;
        scoreText.text = score.ToString();
        if(score > hScore)
        {
            hScore = score;
            hScoreText.text = hScore.ToString();
            PlayerPrefs.SetInt("hScore", hScore);
        }
    }
}
