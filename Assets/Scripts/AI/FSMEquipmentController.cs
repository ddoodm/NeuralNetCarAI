using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(SocketEquipment))]
[RequireComponent(typeof(FSMBotController))]
public class FSMEquipmentController : MonoBehaviour
{
    private SocketEquipment socketEquipment;
    private FSMBotController botControl;
    private EnergyController energyControl;
    private Equipment[] equips;
    private Transform player;
    private Rigidbody thisBody;

    public float
        flipperActivationRadius = 8.0f,
        flipperMinCooldownSeconds = 1.0f, flipperMaxCooldownSeconds = 2.8f,
        plasmaShieldActivationRadius = 10.0f, plasmaCooldownMin = 5.0f, plasmaCooldownMax = 10.0f;

	// Use this for initialization
	void Start ()
    {
        socketEquipment = GetComponent<SocketEquipment>();
        botControl = GetComponent<FSMBotController>();
        //player = GameObject.FindWithTag("Player");
        player = botControl.playerTransform;
        thisBody = GetComponent<Rigidbody>();
        energyControl = GetComponent<EnergyController>();

        flipperCooldownTimers = new float[Enum.GetNames(typeof(SocketLocation)).Length];
        hammerCooldownTimers = new float[Enum.GetNames(typeof(SocketLocation)).Length];
        SawHoldTimers = new float[Enum.GetNames(typeof(SocketLocation)).Length];
	}
	
	// Update is called once per frame
	void Update ()
    {
        equips = socketEquipment.equipmentTypes;

        // Process logic for each socket
        for (int socket = 0; socket < equips.Length; socket++)
            DoLogicFor((SocketLocation)socket, equips[socket]);
	}

    private void DoLogicFor(SocketLocation socket, Equipment item)
    {
        switch(item)
        {
            case Equipment.Item_Flipper: WeaponLogic_Flipper(socket); break;
            case Equipment.Item_CircularSaw: WeaponLogic_Saw(socket); break;
            case Equipment.Item_Hammer: WeaponLogic_Hammer(socket); break;
            case Equipment.Item_PlasmaShield: WeaponLogic_PlasmaShield(socket); break;
            case Equipment.Item_Booster:
                if(socket == SocketLocation.BACK)
                    WeaponLogic_RearBooster(socket);
                else
                    WeaponLogic_LateralBooster(socket);
                break;
        }
    }

    private float[] flipperCooldownTimers;
    private void WeaponLogic_Flipper(SocketLocation socket)
    {
        flipperCooldownTimers[(int)socket] -= Time.deltaTime;

        Weapon flipper = socketEquipment.GetWeaponInSocket(socket);
        if (flipper != null)
        {
            Transform flipperTransform = flipper.GetGameObject().transform;

            float distanceToPlayer = (this.transform.position - player.transform.position).magnitude;

            bool playerInRange = distanceToPlayer <= flipperActivationRadius;
            bool imUpsideDown = Vector3.Dot(this.transform.root.up, Vector3.up) < 0;
            bool correctState = botControl.state == FSMBotController.FSMState.ARRIVE;
            bool cooldownOkay = flipperCooldownTimers[(int)socket] <= 0.0f;

            if ((playerInRange || imUpsideDown) && correctState && cooldownOkay)
            {
                flipper.Use();

                // The cooldown time should reflect the distance to the player:
                flipperCooldownTimers[(int)socket] =
                    UnityEngine.Random.Range(flipperMinCooldownSeconds, flipperMaxCooldownSeconds);
            }
        }
    }

    private float[] hammerCooldownTimers;
    private void WeaponLogic_Hammer(SocketLocation socket)
    {
        hammerCooldownTimers[(int)socket] -= Time.deltaTime;
        Weapon hammer = socketEquipment.GetWeaponInSocket(socket);
        if (hammer != null)
        {
            Transform hammerTransform = hammer.GetGameObject().transform;
            float distanceToPlayer = (this.transform.position - player.transform.position).magnitude;

            bool playerInRange = distanceToPlayer <= flipperActivationRadius;
            bool correctState = botControl.state == FSMBotController.FSMState.ARRIVE;
            bool cooldownOkay = hammerCooldownTimers[(int)socket] <= 0.0f;

            if (playerInRange && correctState && cooldownOkay)
            {
                hammer.Use();
                hammerCooldownTimers[(int)socket] = UnityEngine.Random.Range(flipperMinCooldownSeconds, flipperMaxCooldownSeconds);
            }
        }
    }

    private float[] SawHoldTimers;
    private void WeaponLogic_Saw(SocketLocation socket)
    {
        SawHoldTimers[(int)socket] += Time.deltaTime;
        Weapon saw = socketEquipment.GetWeaponInSocket(socket);
        if (saw != null)
        {
            Transform sawTransform = saw.GetGameObject().transform;
            float distanceToPlayer = (this.transform.position - player.transform.position).magnitude;

            bool playerInRange = distanceToPlayer <= flipperActivationRadius;
            bool correctState = botControl.state == FSMBotController.FSMState.ARRIVE;
            bool holdTimeOkay = SawHoldTimers[(int)socket] <= 3.0f;

            if (playerInRange && correctState)
            {
                saw.Use();
                SawHoldTimers[(int)socket] = 0f;
            }
            if (!playerInRange || !correctState || !holdTimeOkay)
            {
                saw.EndUse();
            }
        }
    }

    // Unified booster-wide timer
    private float boosterCooldown = 0.0f;
    private bool boosting = false;
    private void WeaponLogic_RearBooster(SocketLocation socket)
    {
        boosterCooldown -= Time.deltaTime;
        Weapon booster = socketEquipment.GetWeaponInSocket(socket);
        if (booster != null)
        {
            Vector3 vectorToPlayer = player.transform.position - this.transform.position;
            float distanceToPlayer = (vectorToPlayer).magnitude;

            bool inRange = distanceToPlayer > 5.0f;
            //bool notStuck = thisBody.velocity.magnitude > 0.8f;
            bool correctState = (botControl.state == FSMBotController.FSMState.ARRIVE || botControl.state == FSMBotController.FSMState.ARRIVE_SIDE);
            bool goodAngle = Vector3.Dot(vectorToPlayer.normalized, thisBody.transform.forward) >= 0.8f;
            bool hasEnergy = energyControl.energy >= energyControl.maxEnergy * 0.4f;

            if (inRange && correctState && goodAngle && hasEnergy)
                booster.Use();

            if (!inRange)
                booster.EndUse();
        }
    }

    private void WeaponLogic_LateralBooster(SocketLocation socket)
    {
        boosterCooldown -= Time.deltaTime;
        Weapon booster = socketEquipment.GetWeaponInSocket(socket);

        Vector3 toPlayer = (player.transform.position - this.transform.position);
        float distanceToPlayer = toPlayer.magnitude;
        float lateralDistance =
            this.transform.InverseTransformPoint(player.transform.position).x;

        // Do we want to boost to attack the player?
        bool boostTowardPlayer =
            (socket == SocketLocation.LEFT && lateralDistance > 4.0f)
            || (socket == SocketLocation.RIGHT && lateralDistance < -4.0f);
        boostTowardPlayer &= botControl.state == FSMBotController.FSMState.ARRIVE;

        // Do we want to boost to get un-stuck?
        bool isStuck = thisBody.velocity.magnitude < 0.1f;

        // Boost toward the player whenever possible,
        // or try to get un-stuck
        if ((boostTowardPlayer || isStuck)
            && boosterCooldown < 0.0f)
        {
            booster.Use();
            boosterCooldown = UnityEngine.Random.Range(0.8f, 2.0f);
        }
        else
            booster.EndUse();
    }

    private float plasmaCooldownTimer = 0.0f;
    private void WeaponLogic_PlasmaShield(SocketLocation socket)
    {
        plasmaCooldownTimer -= Time.deltaTime;
        Weapon shield = socketEquipment.GetWeaponInSocket(socket);

        if (shield != null)
        {
            Transform shieldTransform = shield.GetGameObject().transform;
            float distanceToPlayer = (this.transform.position - player.transform.position).magnitude;

            bool playerInRange = distanceToPlayer <= plasmaShieldActivationRadius;
            bool cooldownOkay = plasmaCooldownTimer <= 0.0f;
            bool hasEnergy = energyControl? (energyControl.energy > 8.0f) : false;

            if (playerInRange && cooldownOkay && hasEnergy)
            {
                shield.Use();
                plasmaCooldownTimer = UnityEngine.Random.Range(plasmaCooldownMin, plasmaCooldownMax);
            }
        }
    }
}
