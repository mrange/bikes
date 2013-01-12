using System.Collections.ObjectModel;

namespace SASBikes.DataModel
{
    sealed partial class DataModelCollection<T> : ObservableCollection<T>, IDataModelEntity
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