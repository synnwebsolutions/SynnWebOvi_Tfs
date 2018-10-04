using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynnCore.DataAccess.DynamicDb
{
    public class DynamicDbDataField
    {
        public DynamicDbDataField(DynamicDbFieldName dbFieldName, string propertName)
        {
            DbFieldName = dbFieldName;
            PropertName = propertName;
        }

        public DynamicDbFieldName DbFieldName { get; set; }
        public string PropertName { get; set; }
    }

    public enum DynamicDbFieldName
    {
        Field0,
        Field1,
        Field2,
        Field3,
        Field4,
        Field5,
        Field6,
        Field7,
        Field8,
        Field9,
        Field10,
        Field11,
        Field12,
        Field13,
        Field14,
        Field15,
        Field16,
        Field17,
        Field18,
        Field19,
        Field20,
        Field21,
        Field22,
        Field23,
        Field24,
        Field25,
        Field26,
        Field27,
        Field28,
        Field29,
        Field30,
        Field31,
        Field32,
        Field33,
        Field34,
        Field35,
        Field36,
        Field37,
        Field38,
        Field39,
        FieldXm0,
        FieldXml,
        FieldXm2,
        FieldXm3,
        FieldXm4,
        FieldXm5,
        FieldXm6,
        FieldXm7,
        FieldXm8,
        FieldXm9
    }

}
