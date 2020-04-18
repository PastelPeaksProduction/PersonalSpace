using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem Damage;
    public ParticleSystem Heal;
    public ParticleSystem Space;
    public ParticleSystem Sprint;
    public ParticleSystem Calm;
    public ParticleSystem Objective;

    public void DamagePlay()
    {
        Damage.gameObject.SetActive(true);
        Damage.Play();
    }

    public void DamageStop()
    {
        
        Damage.Pause();
        Damage.gameObject.SetActive(false);
    }

    public void HealPlay()
    {
        Heal.gameObject.SetActive(true);
        Heal.Play();
    }

    public void HealStop()
    {
        Heal.Pause();
        Heal.gameObject.SetActive(false);
    }

    public void SpacePlay()
    {
        Space.Play();
    }

    public void SpaceStop()
    {
        Space.Stop();
    }

    public void SprintPlay()
    {
        Sprint.Play();
    }

    public void SprintStop()
    {
        Sprint.Stop();
    }

    public void CalmPlay()
    {
        Calm.Play();
    }

    public void CalmStop()
    {
        Calm.Stop();
    }

    public void ObjectivePlay()
    {
        Objective.Play();
    }

    public void ObjectiveStop()
    {
        
        Objective.Stop();
    }

    public void StopAll()
    {
        Objective.Stop();
        Calm.Stop();
        Sprint.Stop();
        Space.Stop();
        Heal.Stop();
        Damage.Stop();
    }

    public bool SpecialInEffect()
    {
        return Calm.isPlaying || Sprint.isPlaying || Space.isPlaying;
    }
}
