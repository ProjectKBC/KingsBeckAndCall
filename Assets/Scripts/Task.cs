namespace Ria
{
    /// <summary>
    /// タスク
    /// </summary>
    /// <typeparam name="T">タスクの種類</typeparam>
    internal sealed class Task<T>
    {
        #region member
        public T item = default(T);
        public Task<T> prev = null;
        public Task<T> next = null;
        #endregion

        #region Main Function
        /// <summary>
        /// 接続処理
        /// </summary>
        /// <param name="prev">前のノード</param>
        /// <param name="next">後のノード</param>
        public void Attach(Task<T> prev, Task<T> next)
        {
            this.prev = prev;
            this.next = next;
            if (prev != null) { prev.next = this; }
            if (next != null) { next.prev = this; }
        }

        /// <summary>
        /// 切断処理
        /// </summary>
        public void Detach()
        {
            if (this.prev != null) { this.prev.next = this.next; }
            if (this.next != null) { this.next.prev = this.prev; }
            this.prev = null;
            this.next = null;
        }
        #endregion
    }
}