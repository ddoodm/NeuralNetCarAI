using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;
using System.Collections.Generic;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public HPBar healthBar;
    public Text finish;
    public Text money;
    public Button restartButton;
	public Image gameOverScreen;

    private bool analyticsSent = false;
    private bool flipCoroutineRunning = false;
    private bool gameIsOver = false;

    public string controllerA = "SocketFront";

    private Vector3 initialPosition;

    private Coroutine flipTimerCoroutine;

    public float
        maxHealth = 50.0f,
        mass = 1.0f,
        damageFactor = 1.0f,
        maxInstantDamageFraction = 0.01f;

    public int moneyGained;

    // Never set this directly! Anywhere! Yes, this means you!
    private float _health;
    public float health
    {
        get { return _health; }
        set
        {
            _health = value<0? 0 : value;
            healthBar.value = _health;
        }
    }

    void Start()
    {
        if (transform.GetComponent<VehicleController>().player == 2)
        {
            controllerA += "P2";
        }

        initialPosition = this.transform.position;

        healthBar.maxValue = maxHealth;
        health = maxHealth;
        calculateMass();
    }
	
	// Update is called once per frame
	void Update ()
    {
        calculateMass();
        if (health <= 0)
            gameIsOver = true;

        if (gameIsOver)
            gameOver();

        if (isFlipped() && !flipCoroutineRunning)
        {
            flipCoroutineRunning = true;
            flipTimerCoroutine = StartCoroutine(FlipCountdown());
        }

        // Check if player is out of the world!
        // (TODO: Fix this bad hack)
        if (this.transform.position.y < -15.0f)
        {
            this.transform.root.GetComponent<Rigidbody>().velocity = Vector3.zero;
            this.transform.position = initialPosition;
        }
	}

    public void issueDamage(Collision collision)
    {
        //Determine damage multiplier
        float damageMultiplier = 1.0f;

        PlayerHealth enemy = collision.gameObject.GetComponent<PlayerHealth>();
        float damage = Mathf.Clamp(collision.impulse.magnitude, 0.0f, maxHealth * maxInstantDamageFraction); ;
        float thisDamage = 0;

        //check if hit by weapon
        foreach (ContactPoint contact in collision.contacts)
        {
            weaponStats weapon = contact.otherCollider.gameObject.GetComponent<weaponStats>();
            weaponStats armor = contact.thisCollider.gameObject.GetComponent<weaponStats>();
            if (weapon != null)
            {           
                if(weapon.damageMultiplier > 1)
                    damageMultiplier = weapon.damageMultiplier;
            }
            if (armor != null)
            {
                if (armor.damageMultiplier < 1)
                    damageMultiplier *= armor.damageMultiplier;
            }

            thisDamage = damage * damageMultiplier * (enemy? enemy.mass : 1.0f);

            if (armor != null)
                armor.issueDamageAttachment(thisDamage);

        }
        if (collision.gameObject.tag != "Untagged")
        {
            Debug.Log("Damage Multiplier: " + damageMultiplier);
            Debug.Log(collision.gameObject.tag + " Mass: " + (enemy ? enemy.mass : 1.0f));
        }
        this.issueDamage(thisDamage);

        if (collision.gameObject.tag != "Untagged")
        {
            Debug.Log(gameObject.tag + " hit for " + thisDamage);
            Debug.Log(gameObject.name + " has " + health + " remaining");
        }

        /* TODO: Re-enable this; Deinyon disabled analytics for now, because of compiler errors.*/
        if (enemy != null)
        {
            //transform.GetComponent<AudioSource>().Play();
            /*
            Analytics.CustomEvent("Hit", new Dictionary<string, object> 
        {
            {"Was Hit", gameObject.name},
            {"Hit by", enemy.gameObject.name},
            {"Hit for", thisDamage},
            {"Remaining HP", health}
        });*/
        }
    }

    public void issueDamage(float damage)
    {
        
        health -= damage;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(Time.timeSinceLevelLoad > 1)
            issueDamage(collision);
        

        if (collision.gameObject.CompareTag("Weapon"))
        {
            hazardDamage(collision);
        }
        
    }

    private void hazardDamage(Collision collision)
    {
        float damageMultiplier = 1.0f;
        float damage = collision.relativeVelocity.magnitude;
        float thisDamage = 0;

        //check if hit by weapon
        foreach (ContactPoint contact in collision.contacts)
        {
            weaponStats weapon = contact.otherCollider.gameObject.GetComponent<weaponStats>();
            weaponStats armor = contact.thisCollider.gameObject.GetComponent<weaponStats>();
            if (weapon != null)
            {
                if (weapon.damageMultiplier > 1)
                    damageMultiplier = weapon.damageMultiplier;
            }
            if (armor != null)
            {
                if (armor.damageMultiplier < 1)
                    damageMultiplier *= armor.damageMultiplier;
            }

            thisDamage = damage * damageMultiplier * weapon.mass;
            weapon.issueDamageAttachment(thisDamage);


            if (armor != null)
                armor.issueDamageAttachment(thisDamage);

        }

        this.issueDamage(thisDamage);

        Debug.Log(gameObject.name + " has " + health + " remaining");
    }

    private void calculateMass()
    {
        mass = 1;
        weaponStats[] allChildren = GetComponentsInChildren<weaponStats>();
        foreach (weaponStats child in allChildren)
        {
            mass += child.mass;
        }
        gameObject.GetComponent<Rigidbody>().mass = 50 * mass;
    }

    /// <summary>
    /// Why is no it GameController? :(
    /// </summary>
    void gameOver()
    {
        string winner = "Bot";
        if (gameObject.CompareTag("Player"))
        {
            moneyGained += 50;
            winner = "Bot";
            finish.text = "You Lose!";
        }

        else if (gameObject.CompareTag("Enemy"))
        {
            moneyGained += 100;
            finish.text = "You win!";
            winner = "Player";
        }
		Time.timeScale = 0.0f;
		//Show Game Over Screen 
		//restartButton.gameObject.SetActive(true);
		finish.gameObject.SetActive (true);
		Color gameOverScrColor = new Color(0.3f,0.5f,1,1);
		gameOverScreen.color = gameOverScrColor;

        if (!analyticsSent)
        {
            /* TODO: Re-enable this; Deinyon disabled analytics for now, because of compiler errors.
            */
            /*
            Analytics.CustomEvent("gameOver", new Dictionary<string, object> 
            {
                {"Winner", winner},
                {"Remaining HP", health},
            });*/

            calculateMoneyEarned();
            analyticsSent = true;
        }

        if (Application.loadedLevelName != "BattleScene03Multi")
        {
            if (Input.GetButtonDown(controllerA) || Input.anyKeyDown)
            {
                Application.LoadLevel("ItemStore");
            }
        }
        else
        {
            if (Input.GetButtonDown(controllerA) || Input.anyKeyDown)
            {
                Application.LoadLevel("MultiStore");
            }
        }
    }

    private void calculateMoneyEarned()
    {
        persistentStats stats = GameObject.FindGameObjectWithTag("Persistent Stats").GetComponent<persistentStats>();

        if (Time.timeSinceLevelLoad < 60)
        {
            moneyGained += 100;
        }
        else
        {
            moneyGained += (int)Time.timeSinceLevelLoad * (10 / 6);
        }

        weaponStats[] weapons = gameObject.GetComponentsInChildren<weaponStats>();
        foreach (weaponStats weapon in weapons)
        {
            moneyGained += 30;
        }

        switch (Application.loadedLevel)
        {
            case 6:
                moneyGained += 200;
                break;
            case 7:
                moneyGained += 300;
                break;

            default:
                moneyGained += 100;
                break;


        }
        if (gameObject.CompareTag("Enemy"))
        {
            moneyGained *= 2;
        }

        money.text = "Money Earned: " + moneyGained;
        money.gameObject.SetActive(true);
        stats.playerMoney += moneyGained;
    }

	IEnumerator showGameOver() {
		yield return new WaitForSeconds(2);
	}

    IEnumerator FlipCountdown()
    {
        flipCounterController flipCtrl =
            GameObject.FindWithTag("GameController").GetComponent<flipCounterController>();

        for (int i=5; i > 0; i--)
        {
            flipCtrl.UpdateCountdown(this.transform.root.gameObject, i);

            yield return new WaitForSeconds(1);

            if (!isFlipped())
            {
                flipCoroutineRunning = false;
                yield break;
            }
        }

        // Request the GameController to reset the players
        flipCtrl.TriggerFlipTimeout(this.transform.root.gameObject, UnflipCompleteCallback);
    }

    public void UnflipCompleteCallback()
    {
        flipCoroutineRunning = false;
    }

    public bool isFlipped()
    {
        return Vector3.Dot(transform.up, Vector3.up) < 0.1;
    }
}
