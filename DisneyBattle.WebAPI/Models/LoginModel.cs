using System.ComponentModel.DataAnnotations;

namespace DisneyBattle.WebAPI.Models
{
    public class LoginModel
    {
        private string _pseudo;
        private string _password;

        [Required]
        public string Pseudo
        {
            get
            {
                return _pseudo;
            }

            set
            {
                _pseudo = value;
            }
        }

        [Required]
        public string Password
        {
            get
            {
                return _password;
            }

            set
            {
                _password = value;
            }
        }
    }
}
