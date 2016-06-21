using UnityEngine;
using System.Collections;

public class EnergyController : MonoBehaviour
{
    public HPBar energyBar;
    public float maxEnergy = 50.0f;
    public float energyRefillSpeed = 0.25f;

    private float _energy;

    public float energy
    {
        get { return _energy; }
        set
        {
            _energy = value < 0 ? 0 : value;

            if (energyBar)
                energyBar.value = _energy;
        }
    }

    void Update()
    {
        // Automatic energy refil
        if (energy < maxEnergy)
            energy += energyRefillSpeed * Time.deltaTime;
    }

    public void DrainEnergy()
    {
        energy -= maxEnergy;
    }

    public bool energyFull
    {
        get { return energy >= maxEnergy; }
    }

    public float unitEnergy
    {
        get { return energy / maxEnergy; }
    }
}
