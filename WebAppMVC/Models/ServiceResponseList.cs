using Newtonsoft.Json;

namespace WebAppMVC.Models
{
    public class ServiceResponseList<T> where T : class
    {
        public int? Count { get; set; }

        public int? PageIndex { get; set; }

        public int? PageSize { get; set; }


        public T? Data { get; set; }

        public int? PageCount { get; set; }


        public int? draw { get; set; }

        public int? recordsTotal { get; set; }

        public int? recordsFiltered { get; set; }
    }
}
