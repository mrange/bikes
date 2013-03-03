// ----------------------------------------------------------------------------------------------
// Copyright (c) Mårten Rånge.
// ----------------------------------------------------------------------------------------------
// This source code is subject to terms and conditions of the Microsoft Public License. A 
// copy of the license can be found in the License.html file at the root of this distribution. 
// If you cannot locate the  Microsoft Public License, please send an email to 
// dlr@microsoft.com. By using this source code in any fashion, you are agreeing to be bound 
//  by the terms of the Microsoft Public License.
// ----------------------------------------------------------------------------------------------
// You must not remove this notice, or any other, from this software.
// ----------------------------------------------------------------------------------------------

namespace SASBikes.Common.WindowsAdaptors
{
    partial interface IConcurrentQueue<TValue>
    {
        int Count { get; }
        void Enqueue(TValue value);
        bool TryDequeue(out TValue value);
    }

#if SILVERLIGHT || WINDOWS_PHONE
    sealed partial class ConcurrentQueue<TValue> : IConcurrentQueue<TValue>
    {
        readonly System.Collections.Generic.Queue<TValue> m_queue = new System.Collections.Generic.Queue<TValue>();


        public int Count
        {
            get
            {
                lock (m_queue)
                {
                    return m_queue.Count;
                }
            }
        }

        public void Enqueue(TValue value)
        {
            lock (m_queue)
            {
                m_queue.Enqueue(value);
            }
        }

        public bool TryDequeue(out TValue value)
        {
            lock (m_queue)
            {
                if (m_queue.Count > 0)
                {
                    value = m_queue.Dequeue();
                    return true;
                }
                else
                {
                    value = default(TValue);
                    return false;

                }
            }
        }
    }
#else
    sealed partial class ConcurrentQueue<TValue> : IConcurrentQueue<TValue>
    {
        readonly System.Collections.Concurrent.ConcurrentQueue<TValue> m_queue = new System.Collections.Concurrent.ConcurrentQueue<TValue> ();


        public int Count { get { return m_queue.Count; }}

        public void Enqueue(TValue value)
        {
            m_queue.Enqueue(value);
        }

        public bool TryDequeue(out TValue value)
        {
            return m_queue.TryDequeue(out value);
        }
    }
#endif
}