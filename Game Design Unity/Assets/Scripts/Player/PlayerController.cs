using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Plataformer2d
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private HPbarController _myHealthBar;
        public float currentlife;
        public float damage = 10;
        public float knockback = 2;
        public float maxlife = 100;
        public int arrows = 10;
        public int potionsAmount = 0;
        public int maxPotions = 5;
        public float potionRestoreAmount = 25f;
        public GameObject proyectileprefab;
        public bool hasKey = false;

        //---EVENTOS-------------------
        public UnityEvent onDie = new UnityEvent();
        public Action<float> onLifeChange;//HP y barra de vida
        public UnityEvent onExtraLifeGain = new UnityEvent();//vidas
        public Action<int> onPotionCounterChange;
        public Action<int> onArrowsCounterChange;
        [SerializeField] private float speed = 0;
        [SerializeField] private float jumpForceSpeed = 0;
        [SerializeField] private float groundDetectionDistance = 1f;
        [SerializeField] private float rotationspeed;
        [SerializeField] private float TimeToPlayAudio = 1f; //Timer del Audio
        [SerializeField] private LayerMask groundDetectionLayers;
        [SerializeField] private int lives = 1;
        [SerializeField] private AnimatorStateListener animatorStateListener;
        [SerializeField] private float stepCooldown=1;
        [SerializeField] private AudioSource Footsteps;
        [SerializeField] private List<AudioClip> clipFootsteps;
        private AudioClip lastStep;
        private int attack1Hash = Animator.StringToHash("Attack1");//me guardo la traduccion a Hash del nombre de un estado del animator 
        private int attack2Hash = Animator.StringToHash("Attack2");//con el hash puedo comparar onStateEnter y si es igual se que se entro a ese estado
        private int attack3Hash = Animator.StringToHash("Attack3");
        private AnimatorStateInfo thisStateInfo;
        private int maxExtraLives;
        private float AudioCooldown;
        private float originalStepCooldown;
        private AudioSource audiosource;
        private Rigidbody2D myRigidBody;
        private bool isDead = false;
        private bool isGrounded = true;
        private bool isDash = false;
        private float dashCooldown = 1f;
        private float TimeToDash;
        private Animator animatorcontroller;
        private void Awake()
        {
            ResetValues();
            AudioCooldown = 0f;
            TimeToDash = dashCooldown;
            myRigidBody = GetComponent<Rigidbody2D>();
            animatorcontroller = GetComponent<Animator>();
            animatorcontroller.SetBool("IsRunning", false);
            animatorcontroller.SetBool("IsDead", false);
            animatorcontroller.SetBool("IsGrounded", false);
            audiosource = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
            maxExtraLives = lives;
            animatorStateListener.OnStateEntered += OnStateEntered;
            onArrowsCounterChange?.Invoke(arrows);
        }
        private void Start()
        {
            originalStepCooldown = stepCooldown;
            _myHealthBar.SetMaxHealth(maxlife);
        }
        private void Update()
        {
            stepCooldown -= Time.deltaTime;
            float horizontal = Input.GetAxis("Horizontal");
            //-----INPUT DEL PLAYER-----
            if (horizontal != 0)
            {
                //GetAxis is smoothed based on the “sensitivity” setting so that value gradually changes from 0 to 1, or 0 to -1. 
                //Whereas GetAxisRaw will only ever return 0, -1, or 1 exactly (assuming a digital input such as a keyboard or joystick button)
                //FUNCIONA
                
                if (isDead == false)//chequeo si esta muerto el jugador asi no se mueve mientras esta muerto
                {
                    if (horizontal < 0)
                    {
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                        animatorcontroller.SetBool("IsRunning", true);
                    }

                    if (horizontal > 0)
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                        animatorcontroller.SetBool("IsRunning", true);
                    }

                    if (isGrounded == true)
                    {
                        PlayStep();
                    }
                }

            }

            if (horizontal == 0)
            {
                animatorcontroller.SetBool("IsRunning", false);
            }

            //float ymovement = Input.GetAxis("Vertical") * speed * Time.deltaTime;

            float xmovement = horizontal * speed * Time.deltaTime;

            if (isDead == false)//chequeo si esta muerto el jugador asi no se mueve mientras esta muerto
            {
                transform.position += new Vector3(xmovement, 0f, 0f);
            }

            //---------*SALTO DEL PLAYER*---------------
            if (Input.GetButtonDown("Jump") && isGrounded == true && !isDead)
            {
                animatorcontroller.SetTrigger("Jump");
                animatorcontroller.SetBool("IsGrounded", false);
                isGrounded = false;
                myRigidBody.velocity = Vector2.up * jumpForceSpeed; //el salto 
                //myRigidBody.AddForce(Vector2.up * jumpForceSpeed, ForceMode2D.Impulse);
            }
            if (Input.GetButtonDown("Potion"))
            {
                UsePotion();
            }
            //--------------Input del Dash del player----------------
            if (Input.GetButtonDown("Dash") && TimeToDash <= 0 && isGrounded)
            {
                isDash = true;
                TimeToDash = dashCooldown;
            }
            TimeToDash -= Time.deltaTime;

            //---------------Attack del player---------------------
            //TODO preguntar como hacer que el player se quede quieto al atacar
            if (Input.GetButtonDown("Fire1") && !isDead)
            {
                if (AudioCooldown <= 0)
                {
                    animatorcontroller.SetTrigger("Attack");
                    audiosource.Play();
                    AudioCooldown = TimeToPlayAudio;

                    //speed=0;//TODO reducir velocidad de movimiento al atacar
                }
            }
            AudioCooldown -= Time.deltaTime;
            //ataque de rango del player
            if (Input.GetButtonDown("Range Attack") && arrows > 0)
            {
                animatorcontroller.SetTrigger(isGrounded ? "IsShooting" : "IsAirShooting");
                //Shoot();

            }

            RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, Vector2.down, groundDetectionDistance, groundDetectionLayers);//raycast para detectar que toca el piso y cambiar IsGrounded
            Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundDetectionDistance, 0), Color.red);
            //Debug.Log(hit.collider);

            if (hit.collider != null)
            {
                isGrounded = true;
                animatorcontroller.SetBool("IsGrounded", true);
            }
            else
            {
                isGrounded = false;
                animatorcontroller.SetBool("IsGrounded", false);
            }
        }
        private void FixedUpdate()//meter Fisicas ACA
        {   //DASH DEL PLAYER
            if (isDash == true)
            {
                animatorcontroller.SetTrigger("IsDash");
                isDash = false;
                myRigidBody.AddForce((Vector2)transform.right * speed, ForceMode2D.Impulse);
            }
        }

        private void PlayStep()
        {
            if (stepCooldown <= 0)
            {
                audiosource.PlayOneShot(clipFootsteps[Random.Range(0, clipFootsteps.Count)]);
                stepCooldown = originalStepCooldown;
            }
        }
        public void GetDamage(float damage)
        {
            if (currentlife > 0 && !isDash)
            {
                animatorcontroller.SetTrigger("GetHurt");//reproduce la animacion de sufrir daño
                CameraShake.Instance.ShakeCamera(1f, 0.5f);
                currentlife -= damage;
            }
            onLifeChange?.Invoke(currentlife);
            _myHealthBar.SetHealth(currentlife);

            if (currentlife <= 0)
            {
                Die();
            }
        }
        public void GetHealing(float healnum)
        {

            if (currentlife == maxlife)
            {
                return;
            }
            currentlife += healnum;
            if (currentlife > maxlife)
            {
                currentlife = maxlife;

            }
            onLifeChange?.Invoke(currentlife);
            _myHealthBar.SetHealth(currentlife);
        }
        private void UsePotion()
        {
            if (potionsAmount > 0)
            {
                GetHealing(potionRestoreAmount);
                potionsAmount--;
                onPotionCounterChange?.Invoke(potionsAmount);
            }
        }
        private void Die()
        {
            lives--;
            onDie.Invoke();
            //Time.timeScale = 0f;
            //new WaitForSecondsRealtime(1f); //TODO slowmo effect cuando el player muere
            //Time.timeScale = 1f;
        }
        public void GameOver()
        {
            isDead = true;
            animatorcontroller.SetBool("IsDead", true);
            Destroy(gameObject, 10f);//destruimos al player
        }
        public void Shoot()//metodo que se llama cuando se dispara una bala
        {
            GameObject bullet = Instantiate(proyectileprefab);//instanciamos en una variable de metodo el proyectileprefab
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
            Arrow bulletarrow = bullet.GetComponent<Arrow>();
            bullet.transform.position = transform.position;//le assignamos la posicion del jugador que es el game object de este componente
            bullet.transform.rotation = transform.rotation;
            bulletRB.AddForce((Vector2)transform.right * bulletarrow.speed, ForceMode2D.Impulse);
            arrows--;
            onArrowsCounterChange?.Invoke(arrows);
        }
        public void ResetValues()//funcion que para cuando respawnee tenga toda la vida y otros stats
        {
            currentlife = maxlife;
            onLifeChange?.Invoke(currentlife);
            _myHealthBar.SetHealth(currentlife);
        }
        public bool GetIsDead()//funcion que devuelve si el player esta vivo o no
        {
            return isDead;
        }
        public float GetMaxLife()
        {
            return maxlife;
        }
        public float GetCurrentLife()
        {
            return currentlife;
        }
        public float GetLifePercentage()
        {
            return (float)currentlife / maxlife;
        }
        public int GetLives()
        {
            return lives;
        }
        public int GetMaxLives()
        {
            return maxExtraLives;
        }
        //con el hash puedo comparar onStateEnter y si es igual se que se entro a ese estado
        //comparo si estoy en alguno de los 3 estados de ataque
        //si estoy pongo el attacking en True//el primer attaque solo se incia si esta en False el parametro de animacion "Attacking"
        //OPCIONAL: puedo hacer lo mismo en onStateExited y onStateUpdate para saber Cuando Sali de Un Estado(exited) o cuando estoy adentro de un estado-
        // -todos los frame se llama a onstateUpdate
        private void OnStateEntered(AnimatorStateInfo stateInfo)
        {
            thisStateInfo = stateInfo;
            if (stateInfo.shortNameHash == attack1Hash || stateInfo.shortNameHash == attack2Hash || stateInfo.shortNameHash == attack3Hash)
            {
                animatorcontroller.SetBool("Attacking", true);
            }
            else
            {
                animatorcontroller.SetBool("Attacking", false);
            }
        }
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.CompareTag("Revive"))
            {
                if (lives + 1 > maxExtraLives)
                {
                    return;
                }
                lives++;
                onExtraLifeGain.Invoke();
            }
            if (collider.CompareTag("HealthPotion"))
            {
                if (potionsAmount < maxPotions)
                {
                    potionsAmount++;
                    onPotionCounterChange?.Invoke(potionsAmount);
                }
            }
            if (collider.CompareTag("PickupArrow"))
            {
                if (arrows < 99)
                {
                    arrows++;
                    onArrowsCounterChange?.Invoke(arrows);
                }
            }
            if (collider.CompareTag("Key"))
            {
                hasKey = true;
                //Debug.Log("haskey");
            }
        }
    }
}

