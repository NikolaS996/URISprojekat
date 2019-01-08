using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace URIS2018UserMicroServiceDemo.Models
{
    /// <summary>
    /// A model of the user address
    /// </summary>
    public class UserAddress
    {
        /// <summary>
        /// Address ID
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// User that is associated with the address
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// The address that consists of the street name and house number
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Zip code of the address
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// City name
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// Country name
        /// </summary>
        public string CountryName { get; set; }

        /// <summary>
        /// Latitude of the address
        /// </summary>
        public string Latitude { get; set; }

        /// <summary>
        /// Longitude of the address
        /// </summary>
        public string Longitude { get; set; }

        /// <summary>
        /// Indicator if the address is a default one
        /// </summary>
        public bool Default { get; set; }

        /// <summary>
        /// Indicator if the address is active and can be used
        /// </summary>
        public bool Active { get; set; }
    }
}