using System.Collections.Generic;



namespace TankGame
{
    public class GenericPool<T> : MonoSingletonGeneric<GenericPool<T>>where T:class
    {
        private List<PooledItem<T>> pooledItems = new List<PooledItem<T>>();
        
        public virtual T GetItem()
        {
            PooledItem<T> pooledItem;
            if (pooledItems.Count>0)
            {
                pooledItem = pooledItems.Find(i => !i.isUsed);
                if (pooledItem != null)
                {
                    pooledItem.isUsed = true;
                    return pooledItem.item;
                }
            }
            pooledItem = CreateNewPooledItem();
            return pooledItem.item;
        }
        private PooledItem<T> CreateNewPooledItem()
        {
            PooledItem<T> newItem = new PooledItem<T>
            {
                item = CreateItem(),
                isUsed = true
            };
            pooledItems.Add(newItem);
            return newItem;
        }
        public virtual T CreateItem()
        {
            return (T)null;
        }
        public virtual void ReturnItem(T item)
        {
           PooledItem<T> pooledItem = pooledItems.Find(i => i.item.Equals(item));
           if (pooledItem != null)
           {
            pooledItem.isUsed = false;
           }
        }
    }
    public class PooledItem<T>
    {
        public T item;
        public bool isUsed;
    }
}
