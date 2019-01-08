using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace URIS2018UserMicroServiceDemo.Models
{
    /// <summary>
    /// A model of the user type
    /// </summary>
    public class UserType
    {
        /// <summary>
        /// User type Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of user type
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Indicator if the type is active and can be used
        /// </summary>
        [Required]
        public bool Active { get; set; }

        /// <summary>
        /// Indicator if the user of this type can be registered
        /// </summary>
        [Required]
        public bool CanBeRegistered { get; set; }
    }
}