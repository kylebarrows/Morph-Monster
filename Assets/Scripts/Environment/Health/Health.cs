using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public float startingHealth;
    [SerializeField] private float viewCurrentHealth;
    [SerializeField] public float currentHealth { get; private set; }
    
    public AudioClip audioClip1;
    AudioSource splatsound;
    private void Awake()
    {
        currentHealth = startingHealth;
        viewCurrentHealth = currentHealth;
        splatsound = AddAudio (0.6f);

    }

    // Update is called once per frame
    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        viewCurrentHealth = currentHealth;
        splatsound.clip = audioClip1;
        splatsound.Play ();
    }

    public void Respawn()
    {
        currentHealth = startingHealth;
        viewCurrentHealth = currentHealth;
    }

    public void HandleConsume()
    {
        currentHealth = 0;
        viewCurrentHealth = currentHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            TakeDamage(1);

    }
        public AudioSource AddAudio(float vol) 
    { 
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();
        newAudio.playOnAwake = false;
        newAudio.volume = vol; 
        return newAudio; 
     }
}
