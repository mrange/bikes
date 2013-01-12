// ReSharper disable InconsistentNaming

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SASBikes.DataModel
{
    abstract partial class DataModelBase : INotifyPropertyChanged, IDataModelEntity
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
