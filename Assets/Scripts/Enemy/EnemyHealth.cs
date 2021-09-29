using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;


    Animator anim;
    AudioSource enemyAudio;
    public ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;


    void Awake()
    {
        //Mendapatkan reference komponen
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        //Set current health
        currentHealth = startingHealth;
    }


    void Update()
    {
        //Cek jika sinking
        if (isSinking)
        {
            //memindahkan object kebawah
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage (int amount, Vector3 hitPoint)
    {
        // cek jika TEWAS
        if (isDead)
            return;

        // putar audio
        enemyAudio.Play ();

        // kurangi health
        currentHealth -= amount;

        //Ganti posisi particle
        hitParticles.transform.position = hitPoint;

        //Menjalankan Particle System
        hitParticles.Play();

        // Tewas jika health kurang dari atau sama dengan 0!
        if (currentHealth <= 0)
        {
            Death ();
        }
    }


    void Death ()
    {
        // set isDead
        isDead = true;

        //Set Capcollider ke trigger
        capsuleCollider.isTrigger = true;

        //tirgger play animation Dead.
        anim.SetTrigger ("Dead");
        

        //Putar suara animasi tewas
        enemyAudio.clip = deathClip;
        enemyAudio.Play ();
    }


    public void StartSinking ()
    {
        //disable Navmesh Component
        GetComponent<UnityEngine.AI.NavMeshAgent> ().enabled = false;

        //Set RB ke Kinematic
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;
        Destroy (gameObject, 2f);
    }
}
