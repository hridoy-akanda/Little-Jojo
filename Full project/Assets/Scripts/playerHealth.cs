using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour {

    public float fullHealth;
    public GameObject deathFX;
    float currentHealth;
    playerController controlMovement;
    public AudioClip playerHurt;
    AudioSource playerAS;

    public restartGame theGameManager;

    //HUD variables
    public Slider healthSlider;
    public Image damageScreen;
    public Text gameOverScreen;
    public Text winGameScreen;


    bool damaged = false;
    Color damagedColour = new Color (0f, 0f, 0f, 0.5f);
    float smoothColour = 5f;


    // Use this for initialization
	void Start () {
        currentHealth = fullHealth;
        controlMovement = GetComponent<playerController>();
        playerAS = GetComponent<AudioSource>();

        //HUD initialization
        healthSlider.maxValue = fullHealth;
        healthSlider.value = fullHealth;

        damaged = false;

	}
	
	// Update is called once per frame
	void Update () {
		if (damaged)
        {
            NewMethod();
        }
        else
        {
            //damageScreen.color = Color.Lerp(a: damageScreen.color, b: Color.clear, t: smoothColour * Time.deltaTime);

        }
        damaged = false;
	}

    private void NewMethod()
    {
        //damageScreen.color = damagedColour;
    }

    public void AddDamage (float damage)
    {
        if (damage <= 0)
        {
            return;
        }
        currentHealth -= damage;
        //playerAS.clip = playerHurt;
        //playerAS.Play();
        playerAS.PlayOneShot(playerHurt);

        healthSlider.value = currentHealth;

        damaged = true;

        if (currentHealth <= 0)
        {
            makeDead();
        }
    }
    public void makeDead()
    {
        Instantiate(deathFX, transform.position, transform.rotation);
        Destroy(gameObject);
        Animator gameOverAnimator = gameOverScreen.GetComponent<Animator>();
        gameOverAnimator.SetTrigger("gameOver");
        theGameManager.restartTheGame();
    }

    public  void winGame()
    {
        Destroy(gameObject);
        theGameManager.restartTheGame();
        Animator winGameAnimator = winGameScreen.GetComponent<Animator>();
        winGameAnimator.SetTrigger("gameOver");
    }
}
