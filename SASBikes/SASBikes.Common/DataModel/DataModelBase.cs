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

// ReSharper disable InconsistentNaming

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SASBikes.Common.DataModel
{
    public abstract partial class DataModelBase : INotifyPropertyChanged, IDataModelEntity
    {
        readonly DataModelContext m_context;

        protected DataModelBase(DataModelContext context)
        {
            m_context = context;
        }

        public DataModelContext Context 
        {
            get
            {
                return m_context;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void Raise_PropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
