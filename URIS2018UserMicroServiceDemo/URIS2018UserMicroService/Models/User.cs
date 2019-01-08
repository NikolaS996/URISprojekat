using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace URIS2018UserMicroServiceDemo.Models
{
    /// <summary>
    /// A model of the user
    /// </summary>
    public class User
    {
        /// <summary>
        /// User ID
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// User email
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// User type ID
        /// </summary>
        public int UserTypeId { get; set; }

        /// <summary>
        /// User's phone number
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// List of user addresses
        /// </summary>
        public List<UserAddress> Addresses { get; set; }

        /// <summary>
        /// Indicator if the user is active
        /// </summary>
        public bool Active { get; set; }
    }
}