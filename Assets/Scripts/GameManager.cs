using Player;
using UnityEngine;

namespace DefaultNamespace {
    public class GameManager : MonoBehaviour {
        public static GameManager Instance;
        [SerializeField] private int maxLives = 3;
        [SerializeField] private Transform player;
        private Transform currentCheckPoint;
        private int currentLives;

        private void Awake() {
            if (Instance != null)
                if (Instance != this) {
                    Destroy(gameObject);
                    return;
                }

            Instance = this;
            currentCheckPoint = transform;
            currentLives = maxLives;
        }

        public void SetCheckpoint(Transform checkPoint) {
            currentCheckPoint = checkPoint;
        }

        public void Respawn() {
            currentLives--;
            if (currentLives > 0) {
                player.position = currentCheckPoint.position;
                player.GetComponent<PlayerMovement>().Stop();
                return;
            }

            //TODO you fucking lost scrub
            Application.Quit();
        }

        public void Win() {
            Debug.Log("conglaterations, you won. gg ez");
        }
    }
}