using BCrypt.Net;

namespace TaskApp.Models
{
    public class Usuario 
    {
        public int Id {set; get;}
        public string Nome {set; get;} = string.Empty;   
        public int Idade {set; get;}
        public string Email {set; get;} = string.Empty;
        public string SenhaHash {set; get;} = string.Empty;
        public void SetSenhaHash(string senha)
        {
            SenhaHash = BCrypt.Net.BCrypt.HashPassword(senha); 
        }

        public bool VerificaSenha(string senha)
        {
            return BCrypt.Net.BCrypt.Verify(SenhaHash, senha);
        }
    }
}