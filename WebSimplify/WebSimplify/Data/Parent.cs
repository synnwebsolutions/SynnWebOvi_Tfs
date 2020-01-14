using SynnWebOvi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebSimplify
{
    public class Parent : GenericData
    {
        [GenericDataField("ParentName", "ParentName")]
        public string ParentName { get; set; }

        public Parent(IDataReader data)
        {
            Load(data);
        }

        public Parent()
        {

        }
    }
}