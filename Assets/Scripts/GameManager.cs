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
            playerManager.Init(this.gameObject);
            enemyManager.Init(this.gameObject);
            soundManager.Init(this.gameObject);

            SoundManager.GI.PlayBGM(BACK_GROUND_MUSIC.TITLE);
        }

        private void Update()
        {
            playerManager.Run();
            enemyManager.Run();
            soundManager.Run();

            if (Input.GetKeyDown(KeyCode.A))
            {
                SoundManager.GI.PlaySE(SOUND_EFFECT.VERONICA_NORMAL_SHOT);
            }
        }
    }
}