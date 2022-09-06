using System.ComponentModel.DataAnnotations;

namespace WebApiClientes
{
    public class Cadastro
    {
        //internal DateTime dataNasc;
        //internal int idade;

        [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve conter 11 dígitos")]
        [Required]
        public string CPF { get; set; }

        [MinLength(0, ErrorMessage = "Nome inválido")]
        [Required]
        public string Nome { get; set; }

        [Required]
        public DateTime DataNascimento { get; set; }

        public int Idade
        {
            get
            {
                int idade = DateTime.Now.Year - DataNascimento.Year;
                if (DateTime.Now.DayOfYear < DataNascimento.DayOfYear)
                {
                    idade--;
                }

                return idade;
            }
            set { }
        }
    }
}
