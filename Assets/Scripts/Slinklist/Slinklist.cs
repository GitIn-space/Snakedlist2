using System;
using System.Collections;
using System.Collections.Generic;

namespace FG
{
    class Slinklist<T> : IDynamicList<T>, IEnumerable<T>, IEnumerator<T>
    {
        private class Listnode
        {
            public T data;
            public Listnode next = null;

            public Listnode(T data)
            {
                this.data = data;
            }
        }

        private Listnode head;
        private Listnode tail;
        private Listnode iterator;
        private int count;

        public Slinklist()
        {
            head = null;
            tail = null;
            iterator = null;
            count = 0;
        }

        public T this[int index]
        {
            get
            {
                int i = 0;
                for (Listnode c = head; c != null; c = c.next, i++)
                    if (i == index)
                        return c.data;
                throw new IndexOutOfRangeException();
            }

            set
            {
                int i = 0;
                for (Listnode c = head; c != null; c = c.next, i++)
                    if (i == index)
                    {
                        c.data = value;
                        return;
                    }
                throw new IndexOutOfRangeException();
            }
        }

        public int Count => count;

        public void Add(T item)
        {
            Listnode node = new Listnode(item);

            if (count == 0)
                head = node;

            else
                tail.next = node;

            tail = node;

            count++;
        }

        public void Clear()
        {
            head = null;
            tail = null;
            iterator = null;
            count = 0;
        }

        public bool Contains(T item)
        {
            for (Listnode c = head; c != null; c = c.next)
                if (EqualityComparer<T>.Default.Equals(c.data, item))
                    return true;
            return false;
        }

        public void CopyTo(T[] target, int index)
        {
            if (index >= count || index < 0 || target == null)
                throw new IndexOutOfRangeException();

            int i = 0;
            int f = 0;
            for (Listnode c = head; c != null; c = c.next, i++)
                if (i >= index)
                {
                    target[f] = c.data;
                    f++;
                }
        }

        public int IndexOf(T item)
        {
            int i = 0;
            for (Listnode c = head; c != null; c = c.next, i++)
                if (EqualityComparer<T>.Default.Equals(c.data, item))
                    return i;
            return -1;
        }

        public void Insert(int index, T item)
        {
            if (index < 0 || index > count)
                throw new IndexOutOfRangeException();

            if (index == count)
            {
                Add(item);
                return;
            }

            int i = 0;
            for (Listnode c = head, trail = head; c != null; trail = c, c = c.next, i++)
                if (i == index)
                {
                    Listnode temp = new Listnode(item);
                    trail.next = temp;
                    temp.next = c;
                    count++;

                    if (index == count - 1)
                        tail = temp;
                    if (index == 0)
                        head = temp;
                }
        }

        public bool Remove(T item)
        {
            for (Listnode c = head, trail = head; c != null; trail = c, c = c.next)
                if (EqualityComparer<T>.Default.Equals(c.data, item))
                {
                    trail.next = c.next;
                    count--;
                    return true;
                }
            return false;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= count)
                throw new IndexOutOfRangeException();

            int i = 0;
            for (Listnode c = head, trail = head; c != null; trail = c, c = c.next, i++)
                if (i == index)
                {
                    trail.next = c.next;
                    count--;
                }
        }

        public T Current => iterator.data;

        object IEnumerator.Current => iterator.data;

        public IEnumerator<T> GetEnumerator()
        {
            return (IEnumerator<T>)this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)this;
        }

        public bool MoveNext()
        {
            if (iterator == null)
                iterator = head;
            else
                iterator = iterator.next;

            return iterator != null;
        }

        public void Reset()
        {
            iterator = null;
        }

        public void Dispose()
        {
            ;
        }
    }
}
