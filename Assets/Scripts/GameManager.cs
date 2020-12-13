using Player;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace {
    public class GameManager : MonoBehaviour {
        public static GameManager Instance;
        [SerializeField] private int maxLives = 5;
        [SerializeField] private Transform player;
        private Transform currentCheckPoint;
        private int currentLives;

        [SerializeField] private Text LifeCounter;


        private void Awake() {
            if (Instance != null)
                if (Instance != this) {
                    Destroy(gameObject);
                    return;
                }

            Instance = this;
            currentCheckPoint = transform;
            currentLives = maxLives;
            LifeCounter.text = "x" + currentLives.ToString();
        }

        public void SetCheckpoint(Transform checkPoint) {
            currentCheckPoint = checkPoint;
        }

        public void Respawn() {
            currentLives--;
            if (currentLives > 0) {
                player.position = currentCheckPoint.position;
                player.GetComponent<PlayerMovement>().Stop();
                LifeCounter.text = "x" + currentLives.ToString();
                return;
            }

            //TODO you fucking lost scrub
            Application.Quit();
        }

        public void Win() {
            Debug.Log("conglaterations, you won. gg ez");
            Application.Quit();
        }
    }
}