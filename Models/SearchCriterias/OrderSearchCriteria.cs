using System;
using System.Collections.Generic;
using System.Text;

namespace Models.SearchCriterias
{
    public abstract class SearchCriteria : Base
    {

    }

    public class OrderSearchCriteria : SearchCriteria
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string OrderNumber { get; set; }
        public OrderStatus? Status { get; set; }
    }

    public class CustomerSearchCriteria : SearchCriteria
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
