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

using System.Collections.ObjectModel;

namespace SASBikes.Common.DataModel
{
    public partial class DataModelCollection<T> : ObservableCollection<T>, IDataModelEntity
    {
        readonly DataModelContext m_context;

        public DataModelCollection(DataModelContext context)
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
    }
}