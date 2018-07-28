using UnityEngine;

namespace Ria
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private PlayerManager playerManager;
        [SerializeField]
        private EnemyManager enemyManager;
        [SerializeField]
        private SoundManager soundManager;

        private void Start()
        {
            playerManager.Init();
            enemyManager.Init();
            soundManager.Init();
        }

        private void Update()
        {
            playerManager.Run();
            enemyManager.Run();
            soundManager.Run();
        }
    }
}