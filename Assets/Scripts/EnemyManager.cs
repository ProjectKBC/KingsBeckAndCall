using UnityEngine;

namespace Ria
{
    public class EnemyManager : MonoBehaviour
    {
        public enum ENEMY
        {
            UFA1,
            UFA2,
            UFA3,

            MAX,
        }

        [SerializeField, Tooltip("ScriptableObject")]
        private EnemyScriptableObject[] scriptables = new EnemyScriptableObject[(int)ENEMY.MAX];
        [SerializeField, Tooltip("生成数")]
        private int[] caps = new int[(int)ENEMY.MAX];
        [SerializeField, Tooltip("タイプ")]
        private ENEMY enemy_type = ENEMY.UFA1;

        private Transform trans_;
        private ObjectPool<EnemyCachedActor> pool = new ObjectPool<EnemyCachedActor>();
        private float calcTime = 0;

        public void Start()
        {
            trans_ = this.transform;
            this.pool.Initialize(0, this.scriptables, this.caps);
            this.pool.Generate();
        }
        public void OnDestroy()
        {
            this.pool.Final();
        }
        public void Update()
        {
            // スペースキーで一括回収
            if (Input.GetKeyDown(KeyCode.Space))
            {
                this.pool.Clear();
            }

            // アクティブなオブジェクト数の更新
            // 呼び出されたフレームで経過時間0 秒で処理されていたものを通常稼動扱いにする
            this.pool.FrameTop();
            float elapsedTime = Time.deltaTime;
            this.calcTime += elapsedTime;

            // とりあえず0.1 秒毎に発射
            float span = 0.1f;
            if (this.calcTime >= span)
            {
                EnemyCachedActor enemy = new EnemyCachedActor();
                if (this.pool.AwakeObject((int)this.enemy_type, trans_.position, out enemy))
                {

                }
            }

            // アクティブなオブジェクトの更新
            this.pool.Proc(elapsedTime);
        }
    }
}