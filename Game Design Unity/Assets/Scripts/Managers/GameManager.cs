using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Plataformer2d
{
    public class GameManager : MonoBehaviour
    {
        //condicion de victoria nivel 1: llegar al final del nivel
        //condicion de derrota: La vida/HP del player/jugador llego a 0 o menos de 0
        [SerializeField] private PlayerController player=null;
        [SerializeField] private Transform playerCheckpoint;
        [SerializeField] private List<GameObject> vidas = new List<GameObject>();
        public static GameManager instance;
        private void Start()
        {
            player.onDie.AddListener(OnPlayerDieHandler);
            player.onExtraLifeGain.AddListener(OnGainExtraLifeHandler);
            RespawnPlayer();
            DontDestroyOnLoad(this);
            if (GameManager.instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                RespawnPlayer();
            }
        }
        IEnumerator GameOver()
        {
            //game over Scene
            player.GameOver();
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene("Game Over");
            
        }
        IEnumerator Victory()
        {
            //victory scene
            yield return new WaitForSeconds(4f);
            SceneManager.LoadScene("The End");
        }
        public void StartVictorySequence()
        {
            StartCoroutine(Victory());
        }
        private void OnPlayerDieHandler()
        {
            LoseLife();
            if (player.GetLives() <= 0)
            {
                StartCoroutine(GameOver());
            }
            else
            {
                RestartPlayer();
            }
        }
        private void OnGainExtraLifeHandler()
        {
            GainLife();
        }
        public void RespawnPlayer()
        {
            player.transform.position = playerCheckpoint.position;
        }
        public void RestartPlayer()
        {
            RespawnPlayer();
            player.ResetValues();
        }
        public void ChangeCheckpoint(Transform newCheckPoint)
        {
            playerCheckpoint = newCheckPoint;
        }
        //UI DE lives Stack
        private void LoseLife()
        {
            if (player.GetLives() > 0)
            {
                for (int i = 0; i < vidas.Count; i++)
                {
                    if (vidas[i].activeSelf == true)
                    {
                        vidas[i].SetActive(false);
                        break;
                    }
                }
            }
        }
        public void GainLife()
        {
            for (int i = 0; i < vidas.Count; i++)
            {
                if (vidas[i].activeSelf == false)
                {
                    vidas[i].SetActive(true);
                    break;
                }
            }
        }
    }
}