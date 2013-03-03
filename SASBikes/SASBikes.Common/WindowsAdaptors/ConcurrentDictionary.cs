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
    partial interface IConcurrentDictionary<TKey, TValue>
    {
        bool TryAdd(TKey key, TValue value);        
        bool TryRemove(TKey key, out TValue value);
        void Clear ();
    }

#if SILVERLIGHT || WINDOWS_PHONE
    sealed partial class ConcurrentDictionary<TKey, TValue> : IConcurrentDictionary<TKey, TValue>
    {
        readonly System.Collections.Generic.Dictionary<TKey, TValue> m_dictionary = new System.Collections.Generic.Dictionary<TKey, TValue> ();         
        public bool TryAdd(TKey key, TValue value)
        {
            lock (m_dictionary)
            {
                if (!m_dictionary.ContainsKey(key))
                {
                    return false;
                }

                m_dictionary[key] = value;                

                return true;
            }
        }

        public bool TryRemove(TKey key, out TValue value)
        {
            lock (m_dictionary)
            {
                if (!m_dictionary.TryGetValue(key, out value))
                {
                    return false;
                }

                m_dictionary.Remove(key);

                return true;
            }
        }

        public void Clear()
        {
            m_dictionary.Clear();
        }
    }
#else
    sealed partial class ConcurrentDictionary<TKey, TValue> : IConcurrentDictionary<TKey, TValue>
    {
        readonly System.Collections.Concurrent.ConcurrentDictionary<TKey, TValue> m_dictionary = new System.Collections.Concurrent.ConcurrentDictionary<TKey, TValue> ();         
        public bool TryAdd(TKey key, TValue value)
        {
            return m_dictionary.TryAdd(key, value);
        }

        public bool TryRemove(TKey key, out TValue value)
        {
            return m_dictionary.TryRemove(key, out value);
        }

        public void Clear()
        {
            m_dictionary.Clear();
        }
    }
#endif
}
