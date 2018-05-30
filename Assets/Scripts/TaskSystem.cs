using UnityEngine;

namespace Ria
{
    /// <summary>
    /// タスクの終了状態を返す
    /// </summary>
    /// <typeparam name="T">種類</typeparam>
    /// <param name="obj">タスクオブジェクト</param>
    /// <param name="no">ID</param>
    /// <returns></returns>
    public delegate bool OrderHandler<T>(T obj, int no);

    // タスク管理
    public sealed class TaskSystem<T>
    {
        #region Member
        /// <summary>
        /// 先端
        /// </summary>
        private Task<T> top = null;
        /// <summary>
        /// 終端
        /// </summary>
        private Task<T> end = null;
        /// <summary>
        /// 最大タスク数
        /// </summary>
        private int capacity = 0;
        /// <summary>
        /// 空きタスクインデックス
        /// </summary>
        private int freeCount = -1;
        /// <summary>
        /// 稼動タスク数
        /// </summary>
        public int ActCount { get; private set; }
        /// <summary>
        /// 生成された全タスク
        /// </summary>
        private Task<T>[] taskPool = null;
        /// <summary>
        /// 待機中のプール
        /// </summary>
        private Task<T>[] activeTask = null;
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="capacity">最大タスク接続数</param>
        public TaskSystem(int capacity)
        {
            this.capacity = capacity;
            this.taskPool = new Task<T>[this.capacity];
            this.activeTask = new Task<T>[this.capacity];
            for (int i = 0; i < this.capacity; ++i)
            {
                this.taskPool[i] = new Task<T>();
                this.activeTask[i] = this.taskPool[i];
            }
            this.freeCount = this.capacity;
        }
        
        /// <summary>
        /// 初期化
        /// </summary>
        public void Clear()
        {
            this.freeCount = this.capacity;
            this.ActCount = 0;
            this.top = null;
            this.end = null;
            for (int i = 0; i < this.capacity; ++i)
            {
                this.taskPool[i].prev = null;
                this.taskPool[i].next = null;
                this.activeTask[i] = this.taskPool[i];
            }
        }
        
        /// <summary>
        /// 接続
        /// </summary>
        /// <param name="item">接続するアイテム</param>
        public void Attach(T item)
        {
            Debug.Assert(item != null, "アタッチエラー");
            Debug.Assert(this.freeCount > 0, "キャパシティオーバー");
            Task<T> task = this.activeTask[this.freeCount - 1];
            task.item = item;
            if (this.ActCount > 0)
            {
                task.Attach(this.end, null);
                this.end = task;
            }
            else
            {
                task.Attach(null, null);
                this.end = task; this.top = task;
            }
            --this.freeCount;
            ++this.ActCount;
        }

        /// <summary>
        /// 接続解除
        /// </summary>
        /// <param name="task">接続するタスク</param>
        internal void Detach(Task<T> task)
        {
            if (task == this.top)
                this.top = task.next;
            if (task == this.end)
                this.end = task.prev;
            task.Detach();
            --this.ActCount;
            ++this.freeCount;
            this.activeTask[this.freeCount - 1] = task;
        }

        /// <summary>
        /// 全タスクに命令
        /// </summary>
        /// <param name="order">タスクの終了状態を返す</param>
        public void Order(OrderHandler<T> order)
        {
            int no = 0;
            Task<T> now = null;
            for (Task<T> task = this.top; task != null && this.ActCount > 0;)
            {
                now = task;
                task = task.next;
                if (!order(now.item, no))
                    this.Detach(now);
                ++no;
            }
        }
    }
}