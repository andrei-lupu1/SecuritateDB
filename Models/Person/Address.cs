﻿using Models.Catalogs;
using Models.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Person
{
    [Table("ADDRESSES")]
    public class Address: DomainModelBase, IDomainModel
    {
        [ForeignKey("Person")]
        public int PERSON_ID { get; set; }

        public Person Person { get; set; }

        public string ADDRESS { get; set; }

        public int ZIP_CODE { get; set; }

        [ForeignKey("City")]
        public int CITY_ID { get; set; }

        public City City { get; set; }
    }
}
