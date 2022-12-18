using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] Text scoreTextFinal;
    [SerializeField] GameObject finMenu;
    float numScore = 0;
    Rigidbody2D rb2D;
    [SerializeField] float forse = 1f;
    bool zoneBallOn = false;
    //bool zoneBallOff = false;
    private void Start()
    {
        finMenu.SetActive(false);
        rb2D = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "ActivZone") 
        {
            Debug.Log("Zone On");
            zoneBallOn = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "ActivZone")
        {
            Debug.Log("Zone Off");
            zoneBallOn = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "DeadObj")
        {
            finMenu.SetActive(true);
            Destroy(gameObject);
        }
    }

    public void OnMouseDown()
    {
        if (zoneBallOn)
        {
            rb2D.AddForce(transform.up * forse, ForceMode2D.Impulse);
            float rnd = Random.Range(0.1f, 1f);
            rnd = Mathf.Clamp(rnd, -10, 10);
            transform.rotation = Quaternion.Euler(0f, 0f, rnd);
            numScore += 1;
            scoreText.text = numScore.ToString();
        }
    }
    private void Update()
    {
        scoreTextFinal.text = scoreText.text;
    }
}
