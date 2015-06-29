using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using ProCulturaBackEnd.L10N;
using ProCulturaBackEnd.Models;

namespace ProCulturaBackEnd.Controllers
{
    public class UserController : ApiController
    {
        private readonly ProCulturaBackEndContext _db = new ProCulturaBackEndContext();

        // PUT api/Register2/5
        public IHttpActionResult PutUser(int id, UserModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != user.Id) //check for clearance
            {
                return BadRequest();
            }
            _db.Entry(user).State = EntityState.Modified;
            var obtaineduser = _db.UserModels.FirstOrDefault(x => x.Email == user.Email);
            if (obtaineduser != null)
            {
                if (!PasswordEncryptionService.CheckPassword(obtaineduser, user.Password) || obtaineduser.Password != user.Password)
                {
                    PasswordEncryptionService.Encrypt(user);
                }
            }
            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                throw;
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(UserModel))]
        public IHttpActionResult GetUser(int id, string token)
        {
            var obtaineduser = _db.UserModels.FirstOrDefault(x => x.Email == GeneralEncriptionService.Decrypt(token));
            if (obtaineduser != null)
                return Ok(obtaineduser);
            return NotFound();
        }

        // POST api/Register2
        public ResponseModel PostUser(RegisterModel user)
        {
          if(_db.UserModels.FirstOrDefault(x => x.Email == user.Email) != null)
              return new ResponseModel()
              {
                  Mensaje = LocalizedResponseService.LocalizedResponseFactory.EmailInUseMessage(),
                  Status = 500
              };

            if(!user.Password.Equals(user.ConfirmPassword))
                return new ResponseModel()
                {
                    Mensaje = LocalizedResponseService.LocalizedResponseFactory.PasswordMismatchMessage(),
                    Status = 500
                };
            var newUser = new UserModel()
            {
                Email = user.Email,
                Name = user.FirstName + " " + user.LastName,
                Password = user.Password
            };
            PasswordEncryptionService.Encrypt(newUser);
            _db.UserModels.Add(newUser);
            _db.SaveChanges();

            return new ResponseModel()
            {
                Mensaje = LocalizedResponseService.LocalizedResponseFactory.RegistrationSuccessMessage(),
                Status = 200
            };
        }

        // DELETE api/Register2/5
        public IHttpActionResult DeleteUser(int id)
        {
            var user = _db.UserModels.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            _db.UserModels.Remove(user);
            _db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return _db.UserModels.Count(e => e.Id == id) > 0;
        }
    }
}