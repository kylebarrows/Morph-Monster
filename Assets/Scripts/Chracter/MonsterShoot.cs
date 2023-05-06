using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterShoot : MonsterFormAbility, IMonsterReceiver
{
    private MonsterCharacter Character;

    private MonsterController Controller;
    // Start is called before the first frame update

    private const float timeBetweenShots = 1.0f;
    private float shotTimer = 0.0f;

    private Vector3 mousePos;
    private Vector3 mouseWorldPos;
    private Vector3 firePointPos;

    private float aimIndicatorLength = 5.0f;
    private float damage = 1.0f;

    public bool isAiming = false;
    public AudioClip audioClip1;
    AudioSource arrowsound;

    [SerializeField] private GameObject OnShootEffect;
    [SerializeField] private GameObject ProjectilePrefab;

    Plane plane = new Plane(Vector3.forward, 0);

    void Start()
    {
        FormSwapHandler.OnFormChange += OnFormChange;
        arrowsound = AddAudio (0.6f);
    }

    private void StartAiming()
    {
        isAiming = true;
        Controller.animator.SetBool("AimHeld", true);
    }

    private void StopAiming()
    {
        isAiming = false;
        Controller.animator.SetBool("AimHeld", false);
    }

    private Vector3 GetAimDirection()
    {
        firePointPos = transform.position;
        mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        float distance;
        if (plane.Raycast(ray, out distance))
        {
            mouseWorldPos = ray.GetPoint(distance);
        }
        mouseWorldPos.z = 0;

        Vector3 aimDir = (mouseWorldPos - transform.position).normalized;

        return aimDir;
    }

    private void HandleAiming()
    {
        Vector3 aimDir = GetAimDirection();

        Vector3 aimIndicatorEndPoint = transform.position + (aimDir * aimIndicatorLength);

        Debug.DrawLine(firePointPos, aimIndicatorEndPoint, Color.red, 2.0f, true);
        float angle = Mathf.Atan2(aimDir.x, aimDir.y) * Mathf.Deg2Rad;

    }

    private void Fire()
    {
        shotTimer = timeBetweenShots;
        if (ProjectilePrefab)
        {
            Vector3 aimDir = GetAimDirection();
            float rot = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
            GameObject projectileGO = Instantiate(ProjectilePrefab, firePointPos, Quaternion.Euler(0.0f, 0.0f, rot + 180));
            Projectile projectile = projectileGO.GetComponent<Projectile>();

            if(projectile)
            {
                projectile.SpawnProjectile(Controller.collider, aimDir, damage);
                arrowsound.clip = audioClip1;
                arrowsound.Play ();
            }
        }
        StopAiming();
    }

    public override void UpdateAbility()
    {
        if(!Active)
        {
            return;
        }

        if(Controller.MInput.aimHeld)
        {
            StartAiming();
            if(isAiming)
            {
                HandleAiming();
                
            }
        }
        else
        {
            StopAiming();
        }

        if (Controller.MInput.consumeHeld)
        {
            if (shotTimer <= 0)
            {
                Fire();
            }
        }

        if (shotTimer > 0)
        {
            shotTimer -= Time.deltaTime;
        }
    }

    public void OnFormChange(FormInfo info)
    {
        MonsterFormAbility.Activate(this, info.hasAimAndFire);
    }

    public void SetMonsterReference(MonsterCharacter character)
    {
        Character = character;
        Controller = Character.Controller;
        Character.Abilities.Shoot = this;
    }
        public AudioSource AddAudio(float vol) 
    { 
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();
        newAudio.playOnAwake = false;
        newAudio.volume = vol; 
        return newAudio; 
     }
}
