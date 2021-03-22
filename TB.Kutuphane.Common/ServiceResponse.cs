using System;
using TB.Kutuphane.Common.Enum;

namespace TB.Kutuphane.Common
{
    public class ServiceResponse
    {
        /// <summary>
        /// İşlem sonucu
        /// </summary>
        public string ResponseText { get; set; }

        /// <summary>
        /// Varsa işlemin döndürdüğü değer
        /// </summary>
        public object ReturnObject { get; set; }

        /// <summary>
        /// Yanıt(Enum)
        /// </summary>
        public ResultType ResultType { get; set; }

        /// <summary>
        /// Geçerli zaman
        /// </summary>
        public DateTime ResponseDateTime { get; set; }

        public ServiceResponse()
        {
            ResponseDateTime = DateTime.UtcNow;
            ResultType = ResultType.Unspecified;
        }
    }
}
