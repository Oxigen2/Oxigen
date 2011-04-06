using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data.SqlClient;


namespace OxigenIIAdvertising.SOAStructures
{
    /// <summary>
    /// Represents a category of sub-categories or sub-category of channels
    /// </summary>
    [DataContract]
    public class Category
    {
        private int _categoryID;
        private string _categoryName;
        private bool _bHasChildren;

        /// <summary>
        /// The category's unique identifier
        /// </summary>
        [DataMember]
        public int CategoryID {
            get { return _categoryID; }
            set { _categoryID = value; }
        }

        /// <summary>
        /// The category's name
        /// </summary>
        [DataMember]
        public string CategoryName {
            get { return _categoryName; }
            set { _categoryName = value; }
        }

        /// <summary>
        /// Category has sub-categories
        /// </summary>
        [DataMember]
        public bool HasChildren {
            get { return _bHasChildren; }
            set { _bHasChildren = value; }
        }

        public Category(int categoryID, string categoryName, bool bHasChildren) {
            _categoryID = categoryID;
            _categoryName = categoryName;
            _bHasChildren = bHasChildren;
        }

        public Category() { }
    }
}