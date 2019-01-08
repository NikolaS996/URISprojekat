using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using URIS2018UserMicroServiceDemo.DataAccess;
using URIS2018UserMicroServiceDemo.Models;

namespace URIS2018UserMicroServiceDemo.Controllers
{
    //Kontroler klasa - sadrzi sve pristupne tacke koje su vezane za odredjenu funkcionalnost (npr. operacije sa user-om)
    public class UserController : ApiController
    {
        /*
        Primer pristupne tacke - svi zahtevi upuceni od strane klijenta koji sadrze rutu koja
        je ista kao i ruta koja je definisana u Routes dekoratoru ce biti obradjeni pomocu GetUser metode.
        Konkretno ova metoda ce pozvati metodu klase UserDB i proslediti joj sve neophodne parametre,
        kako bi se zahtev obradio.
        */
        /// <summary>
        /// Get user by ID
        /// </summary>
        /// <param name="id">ID of the user to return</param>
        /// <returns></returns>
        [Route("api/User/{id}"), HttpGet] //{id} predstavlja route parametar
        public User GetUser(int id)
        {
            string email = Request.Content.Headers.GetValues("email").First();
            string password = Request.Content.Headers.GetValues("password").First();
            //iz headera citamo polja username i password(header iz requesta)

            User user = UserDB.GetUser(email, password);//funkcija u UserDB koja trazi usera po email & pass, email=user
            if (user == null)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.Unauthorized);
            }
            else
                return UserDB.GetUserById(id);
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="user"></param>
		[Route("api/User"), HttpPost]
		public void PostUser([FromBody]User user)
		{
			UserDB.PostUser(user);
		}

      /// <summary>
      /// 
      /// </summary>
      /// <param name="id"></param>
        [Route("api/User/{id}"), HttpDelete]
        public void DeleteUser(int id)
        {
            UserDB.DeleteUser(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        [Route("api/User"), HttpPut]
        public void PutUser([FromBody]User user)
        {
            UserDB.PutUser(user);
        }
    }
}