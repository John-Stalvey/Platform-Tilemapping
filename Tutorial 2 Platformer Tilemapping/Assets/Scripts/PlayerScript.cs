using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D rd2d;
    public float speed;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    private int scoreValue;
    private int livesValue;

    public GameObject WinTextObject;
    public GameObject LoseTextObject;

    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        scoreValue = 0;

        rd2d = GetComponent<Rigidbody2D>();
        livesValue = 3;

        SetCountText();
        WinTextObject.SetActive(false);

        SetCountText();
        LoseTextObject.SetActive(false);

        musicSource.clip = musicClipTwo;
        musicSource.Play();
        musicSource.loop = true;

    }

    void SetCountText()
    {
        scoreText.text = "Score: " + scoreValue.ToString();
        if (scoreValue >= 8)
        {
            WinTextObject.SetActive(true);

            musicSource.clip = musicClipTwo;
            musicSource.Stop();

            musicSource.clip = musicClipOne;
            musicSource.Play();
            musicSource.loop = false;

            speed = 0;

        }

        scoreText.text = "Score: " + scoreValue.ToString();
        if (scoreValue == 4)
        {
            livesValue = 3;
            transform.position = new Vector2(50f, 0.5f);
        }

        livesText.text = "Lives: " + livesValue.ToString();
        if (livesValue == 0)
        {
            LoseTextObject.SetActive(true);
            Destroy(gameObject);
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            SetCountText();
            Destroy(collision.collider.gameObject);
        }

        if (collision.collider.tag == "Enemy")
        {
            Destroy(collision.collider.gameObject);
            livesValue = livesValue - 1;

            SetCountText();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3),ForceMode2D.Impulse);
            }
        }
    }


}
