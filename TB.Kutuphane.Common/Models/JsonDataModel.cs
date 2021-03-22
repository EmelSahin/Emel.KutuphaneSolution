using System;
using TB.Kutuphane.Common.Enum;

namespace TB.Kutuphane.Common.Models
{
    [Serializable]
    public class JsonDataModel
    {
        public int draw { get; set; }
        public int recordsFiltered { get; set; }
        public int recordsTotal { get; set; }
        public object data { get; set; }
        public ResultType result { get; set; }
        public string message { get; set; }
    }
}
