using UnityEngine;

namespace Ria
{
    public enum GAME_PHASE
    {
        Loading,
        Run,
        Pose,
    }

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
            playerManager.Init(this.gameObject);
            enemyManager.Init(this.gameObject);
            soundManager.Init(this.gameObject);

            SoundManager.GI.PlayBGM(BACK_GROUND_MUSIC.GAME_STAGE_DEBUG);
        }

        private void Update()
        {

            playerManager.Run();
            enemyManager.Run();
            soundManager.Run();
        }
    }
}