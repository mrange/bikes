// ############################################################################
// #                                                                          #
// #        ---==>  T H I S  F I L E  I S   G E N E R A T E D  <==---         #
// #                                                                          #
// # This means that any edits to the .cs file will be lost when its          #
// # regenerated. Changes should instead be applied to the corresponding      #
// # template file (.tt)                                                      #
// ############################################################################





// ReSharper disable CompareOfFloatsByEqualityOperator
// ReSharper disable InconsistentNaming
// ReSharper disable InvocationIsSkipped
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantAssignment


namespace SASBikes.DataModel
{

    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Xml.Linq;


    sealed partial class State : DataModelBase 
    {
        public State (DataModelContext context) : base (context)
        {
            _State_ZoomLevel = default (decimal)   ;
            _State_Lo = default (decimal)   ;
            _State_La = default (decimal)   ;
            _State_StationName = ""   ;
            _State_SearchingFor = ""   ;
            _State_FavoriteStationNames = new DataModelCollection<string> (context)   ;
            _State_Stations = new DataModelCollection<Station> (context)   ;
        }

        // --------------------------------------------------------------------
        public decimal State_ZoomLevel
        {
            get
            {
                return _State_ZoomLevel;
            }
            set
            {
                if (_State_ZoomLevel != value)
                {
                    var oldValue = _State_ZoomLevel; 

                    _State_ZoomLevel = value;

                    Changed__State_ZoomLevel (oldValue, value);

                    Raise_PropertyChanged ();
                }
            }
        }
        // --------------------------------------------------------------------
        decimal _State_ZoomLevel;
        // --------------------------------------------------------------------
        partial void Changed__State_ZoomLevel (decimal oldValue, decimal newValue);
        // --------------------------------------------------------------------

        // --------------------------------------------------------------------
        public decimal State_Lo
        {
            get
            {
                return _State_Lo;
            }
            set
            {
                if (_State_Lo != value)
                {
                    var oldValue = _State_Lo; 

                    _State_Lo = value;

                    Changed__State_Lo (oldValue, value);

                    Raise_PropertyChanged ();
                }
            }
        }
        // --------------------------------------------------------------------
        decimal _State_Lo;
        // --------------------------------------------------------------------
        partial void Changed__State_Lo (decimal oldValue, decimal newValue);
        // --------------------------------------------------------------------

        // --------------------------------------------------------------------
        public decimal State_La
        {
            get
            {
                return _State_La;
            }
            set
            {
                if (_State_La != value)
                {
                    var oldValue = _State_La; 

                    _State_La = value;

                    Changed__State_La (oldValue, value);

                    Raise_PropertyChanged ();
                }
            }
        }
        // --------------------------------------------------------------------
        decimal _State_La;
        // --------------------------------------------------------------------
        partial void Changed__State_La (decimal oldValue, decimal newValue);
        // --------------------------------------------------------------------

        // --------------------------------------------------------------------
        public string State_StationName
        {
            get
            {
                return _State_StationName;
            }
            set
            {
                if (_State_StationName != value)
                {
                    var oldValue = _State_StationName; 

                    _State_StationName = value;

                    Changed__State_StationName (oldValue, value);

                    Raise_PropertyChanged ();
                }
            }
        }
        // --------------------------------------------------------------------
        string _State_StationName;
        // --------------------------------------------------------------------
        partial void Changed__State_StationName (string oldValue, string newValue);
        // --------------------------------------------------------------------

        // --------------------------------------------------------------------
        public string State_SearchingFor
        {
            get
            {
                return _State_SearchingFor;
            }
            set
            {
                if (_State_SearchingFor != value)
                {
                    var oldValue = _State_SearchingFor; 

                    _State_SearchingFor = value;

                    Changed__State_SearchingFor (oldValue, value);

                    Raise_PropertyChanged ();
                }
            }
        }
        // --------------------------------------------------------------------
        string _State_SearchingFor;
        // --------------------------------------------------------------------
        partial void Changed__State_SearchingFor (string oldValue, string newValue);
        // --------------------------------------------------------------------

        // --------------------------------------------------------------------
        public DataModelCollection<string> State_FavoriteStationNames
        {
            get
            {
                return _State_FavoriteStationNames;
            }
            set
            {
                if (_State_FavoriteStationNames != value)
                {
                    var oldValue = _State_FavoriteStationNames; 

                    if (oldValue != null)
                    {
                        oldValue.CollectionChanged -= CollectionChanged__State_FavoriteStationNames;
                    }
                    _State_FavoriteStationNames = value;
                    if (value != null)
                    {
                        value.CollectionChanged += CollectionChanged__State_FavoriteStationNames;
                    }

                    Changed__State_FavoriteStationNames (oldValue, value);

                    Raise_PropertyChanged ();
                }
            }
        }
        // --------------------------------------------------------------------
        DataModelCollection<string> _State_FavoriteStationNames;
        void CollectionChanged__State_FavoriteStationNames (object sender, NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged__State_FavoriteStationNames (_State_FavoriteStationNames, e);
        }
        // --------------------------------------------------------------------
        partial void CollectionChanged__State_FavoriteStationNames (DataModelCollection<string> value, NotifyCollectionChangedEventArgs e);
        partial void Changed__State_FavoriteStationNames (DataModelCollection<string> oldValue, DataModelCollection<string> newValue);
        // --------------------------------------------------------------------

        // --------------------------------------------------------------------
        public DataModelCollection<Station> State_Stations
        {
            get
            {
                return _State_Stations;
            }
            set
            {
                if (_State_Stations != value)
                {
                    var oldValue = _State_Stations; 

                    if (oldValue != null)
                    {
                        oldValue.CollectionChanged -= CollectionChanged__State_Stations;
                    }
                    _State_Stations = value;
                    if (value != null)
                    {
                        value.CollectionChanged += CollectionChanged__State_Stations;
                    }

                    Changed__State_Stations (oldValue, value);

                    Raise_PropertyChanged ();
                }
            }
        }
        // --------------------------------------------------------------------
        DataModelCollection<Station> _State_Stations;
        void CollectionChanged__State_Stations (object sender, NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged__State_Stations (_State_Stations, e);
        }
        // --------------------------------------------------------------------
        partial void CollectionChanged__State_Stations (DataModelCollection<Station> value, NotifyCollectionChangedEventArgs e);
        partial void Changed__State_Stations (DataModelCollection<Station> oldValue, DataModelCollection<Station> newValue);
        // --------------------------------------------------------------------


    }
    sealed partial class Station : DataModelBase 
    {
        public Station (DataModelContext context) : base (context)
        {
            _Station_Name = ""   ;
            _Station_Number = default (int)   ;
            _Station_Address = ""   ;
            _Station_FullAddress = ""   ;
            _Station_Lo = default (decimal)   ;
            _Station_La = default (decimal)   ;
            _Station_IsOpen = default (bool)   ;
            _Station_IsBonus = default (bool)   ;
        }

        // --------------------------------------------------------------------
        public string Station_Name
        {
            get
            {
                return _Station_Name;
            }
            set
            {
                if (_Station_Name != value)
                {
                    var oldValue = _Station_Name; 

                    _Station_Name = value;

                    Changed__Station_Name (oldValue, value);

                    Raise_PropertyChanged ();
                }
            }
        }
        // --------------------------------------------------------------------
        string _Station_Name;
        // --------------------------------------------------------------------
        partial void Changed__Station_Name (string oldValue, string newValue);
        // --------------------------------------------------------------------

        // --------------------------------------------------------------------
        public int Station_Number
        {
            get
            {
                return _Station_Number;
            }
            set
            {
                if (_Station_Number != value)
                {
                    var oldValue = _Station_Number; 

                    _Station_Number = value;

                    Changed__Station_Number (oldValue, value);

                    Raise_PropertyChanged ();
                }
            }
        }
        // --------------------------------------------------------------------
        int _Station_Number;
        // --------------------------------------------------------------------
        partial void Changed__Station_Number (int oldValue, int newValue);
        // --------------------------------------------------------------------

        // --------------------------------------------------------------------
        public string Station_Address
        {
            get
            {
                return _Station_Address;
            }
            set
            {
                if (_Station_Address != value)
                {
                    var oldValue = _Station_Address; 

                    _Station_Address = value;

                    Changed__Station_Address (oldValue, value);

                    Raise_PropertyChanged ();
                }
            }
        }
        // --------------------------------------------------------------------
        string _Station_Address;
        // --------------------------------------------------------------------
        partial void Changed__Station_Address (string oldValue, string newValue);
        // --------------------------------------------------------------------

        // --------------------------------------------------------------------
        public string Station_FullAddress
        {
            get
            {
                return _Station_FullAddress;
            }
            set
            {
                if (_Station_FullAddress != value)
                {
                    var oldValue = _Station_FullAddress; 

                    _Station_FullAddress = value;

                    Changed__Station_FullAddress (oldValue, value);

                    Raise_PropertyChanged ();
                }
            }
        }
        // --------------------------------------------------------------------
        string _Station_FullAddress;
        // --------------------------------------------------------------------
        partial void Changed__Station_FullAddress (string oldValue, string newValue);
        // --------------------------------------------------------------------

        // --------------------------------------------------------------------
        public decimal Station_Lo
        {
            get
            {
                return _Station_Lo;
            }
            set
            {
                if (_Station_Lo != value)
                {
                    var oldValue = _Station_Lo; 

                    _Station_Lo = value;

                    Changed__Station_Lo (oldValue, value);

                    Raise_PropertyChanged ();
                }
            }
        }
        // --------------------------------------------------------------------
        decimal _Station_Lo;
        // --------------------------------------------------------------------
        partial void Changed__Station_Lo (decimal oldValue, decimal newValue);
        // --------------------------------------------------------------------

        // --------------------------------------------------------------------
        public decimal Station_La
        {
            get
            {
                return _Station_La;
            }
            set
            {
                if (_Station_La != value)
                {
                    var oldValue = _Station_La; 

                    _Station_La = value;

                    Changed__Station_La (oldValue, value);

                    Raise_PropertyChanged ();
                }
            }
        }
        // --------------------------------------------------------------------
        decimal _Station_La;
        // --------------------------------------------------------------------
        partial void Changed__Station_La (decimal oldValue, decimal newValue);
        // --------------------------------------------------------------------

        // --------------------------------------------------------------------
        public bool Station_IsOpen
        {
            get
            {
                return _Station_IsOpen;
            }
            set
            {
                if (_Station_IsOpen != value)
                {
                    var oldValue = _Station_IsOpen; 

                    _Station_IsOpen = value;

                    Changed__Station_IsOpen (oldValue, value);

                    Raise_PropertyChanged ();
                }
            }
        }
        // --------------------------------------------------------------------
        bool _Station_IsOpen;
        // --------------------------------------------------------------------
        partial void Changed__Station_IsOpen (bool oldValue, bool newValue);
        // --------------------------------------------------------------------

        // --------------------------------------------------------------------
        public bool Station_IsBonus
        {
            get
            {
                return _Station_IsBonus;
            }
            set
            {
                if (_Station_IsBonus != value)
                {
                    var oldValue = _Station_IsBonus; 

                    _Station_IsBonus = value;

                    Changed__Station_IsBonus (oldValue, value);

                    Raise_PropertyChanged ();
                }
            }
        }
        // --------------------------------------------------------------------
        bool _Station_IsBonus;
        // --------------------------------------------------------------------
        partial void Changed__Station_IsBonus (bool oldValue, bool newValue);
        // --------------------------------------------------------------------


    }

    static partial class DataModelSerializer
    {
        public static XElement Serialize (this DataModelCollection<State> instance, string name)
        {
            if (instance == null)
            {
                return null;
            }

            return CreateElement (
                    name
                ,   instance.Select ((v,i) => v.Serialize (i.ToString()))
                );

        }

        public static XElement Serialize (this State instance, string name)
        {
            if (instance == null)
            {
                return null;
            }
            return CreateElement (
                    name
                ,   instance.State_ZoomLevel.Serialize ("ZoomLevel")
                ,   instance.State_Lo.Serialize ("Lo")
                ,   instance.State_La.Serialize ("La")
                ,   instance.State_StationName.Serialize ("StationName")
                ,   instance.State_SearchingFor.Serialize ("SearchingFor")
                ,   instance.State_FavoriteStationNames.Serialize ("FavoriteStationNames")
                ,   instance.State_Stations.Serialize ("Stations")
                );
        }

        public static void Unserialize (
                this XElement element
            ,   DataModelContext context
            ,   IUnserializeErrorReporter reporter
            ,   ref DataModelCollection<State> instance
            )
        {
            instance = new DataModelCollection<State> (context);

            if (element == null)
            {
                return;
            }

            foreach (var subElement in element.Elements (NodeName))
            {
                State subInstance = null;
                
                subElement.Unserialize (
                    context,
                    reporter,
                    ref subInstance
                    );

                instance.Add (subInstance);                                
            }
        }

        public static void Unserialize (
                this XElement element
            ,   DataModelContext context
            ,   IUnserializeErrorReporter reporter
            ,   ref State instance
            )
        {
            instance = new State (context);

            if (element == null)
            {
                return;
            }

            foreach (var subElement in element.Elements(NodeName))
            {
                var nameAttribute = subElement.Attribute(NameAttributeName);
                if (nameAttribute == null)
                {
                    continue;
                }

                var name = nameAttribute.Value;

                switch (name)
                {
                    case "ZoomLevel":
                        {
                            var value = default (decimal);

                            subElement.Unserialize (
                                context,
                                reporter,
                                ref value
                                );       
                            
                            instance.State_ZoomLevel = value;                                
                        }
                        break;
                    case "Lo":
                        {
                            var value = default (decimal);

                            subElement.Unserialize (
                                context,
                                reporter,
                                ref value
                                );       
                            
                            instance.State_Lo = value;                                
                        }
                        break;
                    case "La":
                        {
                            var value = default (decimal);

                            subElement.Unserialize (
                                context,
                                reporter,
                                ref value
                                );       
                            
                            instance.State_La = value;                                
                        }
                        break;
                    case "StationName":
                        {
                            var value = "";

                            subElement.Unserialize (
                                context,
                                reporter,
                                ref value
                                );       
                            
                            instance.State_StationName = value;                                
                        }
                        break;
                    case "SearchingFor":
                        {
                            var value = "";

                            subElement.Unserialize (
                                context,
                                reporter,
                                ref value
                                );       
                            
                            instance.State_SearchingFor = value;                                
                        }
                        break;
                    case "FavoriteStationNames":
                        {
                            var value = new DataModelCollection<string> (context);

                            subElement.Unserialize (
                                context,
                                reporter,
                                ref value
                                );       
                            
                            instance.State_FavoriteStationNames = value;                                
                        }
                        break;
                    case "Stations":
                        {
                            var value = new DataModelCollection<Station> (context);

                            subElement.Unserialize (
                                context,
                                reporter,
                                ref value
                                );       
                            
                            instance.State_Stations = value;                                
                        }
                        break;
                    default:
                        break;
                }
            }
        }


        public static XElement Serialize (this DataModelCollection<Station> instance, string name)
        {
            if (instance == null)
            {
                return null;
            }

            return CreateElement (
                    name
                ,   instance.Select ((v,i) => v.Serialize (i.ToString()))
                );

        }

        public static XElement Serialize (this Station instance, string name)
        {
            if (instance == null)
            {
                return null;
            }
            return CreateElement (
                    name
                ,   instance.Station_Name.Serialize ("Name")
                ,   instance.Station_Number.Serialize ("Number")
                ,   instance.Station_Address.Serialize ("Address")
                ,   instance.Station_FullAddress.Serialize ("FullAddress")
                ,   instance.Station_Lo.Serialize ("Lo")
                ,   instance.Station_La.Serialize ("La")
                ,   instance.Station_IsOpen.Serialize ("IsOpen")
                ,   instance.Station_IsBonus.Serialize ("IsBonus")
                );
        }

        public static void Unserialize (
                this XElement element
            ,   DataModelContext context
            ,   IUnserializeErrorReporter reporter
            ,   ref DataModelCollection<Station> instance
            )
        {
            instance = new DataModelCollection<Station> (context);

            if (element == null)
            {
                return;
            }

            foreach (var subElement in element.Elements (NodeName))
            {
                Station subInstance = null;
                
                subElement.Unserialize (
                    context,
                    reporter,
                    ref subInstance
                    );

                instance.Add (subInstance);                                
            }
        }

        public static void Unserialize (
                this XElement element
            ,   DataModelContext context
            ,   IUnserializeErrorReporter reporter
            ,   ref Station instance
            )
        {
            instance = new Station (context);

            if (element == null)
            {
                return;
            }

            foreach (var subElement in element.Elements(NodeName))
            {
                var nameAttribute = subElement.Attribute(NameAttributeName);
                if (nameAttribute == null)
                {
                    continue;
                }

                var name = nameAttribute.Value;

                switch (name)
                {
                    case "Name":
                        {
                            var value = "";

                            subElement.Unserialize (
                                context,
                                reporter,
                                ref value
                                );       
                            
                            instance.Station_Name = value;                                
                        }
                        break;
                    case "Number":
                        {
                            var value = default (int);

                            subElement.Unserialize (
                                context,
                                reporter,
                                ref value
                                );       
                            
                            instance.Station_Number = value;                                
                        }
                        break;
                    case "Address":
                        {
                            var value = "";

                            subElement.Unserialize (
                                context,
                                reporter,
                                ref value
                                );       
                            
                            instance.Station_Address = value;                                
                        }
                        break;
                    case "FullAddress":
                        {
                            var value = "";

                            subElement.Unserialize (
                                context,
                                reporter,
                                ref value
                                );       
                            
                            instance.Station_FullAddress = value;                                
                        }
                        break;
                    case "Lo":
                        {
                            var value = default (decimal);

                            subElement.Unserialize (
                                context,
                                reporter,
                                ref value
                                );       
                            
                            instance.Station_Lo = value;                                
                        }
                        break;
                    case "La":
                        {
                            var value = default (decimal);

                            subElement.Unserialize (
                                context,
                                reporter,
                                ref value
                                );       
                            
                            instance.Station_La = value;                                
                        }
                        break;
                    case "IsOpen":
                        {
                            var value = default (bool);

                            subElement.Unserialize (
                                context,
                                reporter,
                                ref value
                                );       
                            
                            instance.Station_IsOpen = value;                                
                        }
                        break;
                    case "IsBonus":
                        {
                            var value = default (bool);

                            subElement.Unserialize (
                                context,
                                reporter,
                                ref value
                                );       
                            
                            instance.Station_IsBonus = value;                                
                        }
                        break;
                    default:
                        break;
                }
            }
        }




    }

}

