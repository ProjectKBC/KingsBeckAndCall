using UnityEngine;

namespace Ria
{
    [System.Serializable]
    public class EnemyManager : ChildManager
    {
        public enum ENEMY
        {
            UFA_FIRST,
            UFA_SECOND,
            UFA3_THIRD,

            MAX,
        }

        [SerializeField, Tooltip("")]
        private Vector3 startPosition;
        [SerializeField, Tooltip("タイプ")]
        private ENEMY enemyType = ENEMY.UFA_FIRST;



        [SerializeField, Tooltip("ScriptableObject")]
        private EnemyScriptableObject[] scriptables = new EnemyScriptableObject[(int)ENEMY.MAX];
        [SerializeField, Tooltip("生成数")]
        private int[] caps = new int[(int)ENEMY.MAX];
    
        private ActorPool<EnemyCachedActor> pool = new ActorPool<EnemyCachedActor>();
        private float calcTime = 0;

        protected override void OnInit()
        {
            this.pool.Initialize(0, this.scriptables, this.caps);
            this.pool.Generate();
        }

        protected override void OnRun()
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

            // とりあえず1 秒毎に発射
            float span = 1f;
            if (this.calcTime >= span)
            {
                EnemyCachedActor enemy = new EnemyCachedActor();
                this.pool.AwakeObject((int)this.enemyType, startPosition, out enemy);
                this.calcTime -= span;
            }

            // アクティブなオブジェクトの更新
            this.pool.Proc(elapsedTime);
        }

        public void OnDestroy()
        {
            this.pool.Final();
        }
    }
}