﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AspNetCore.Models
{
    public class NovoUsuariomodel
    {
        [Required(ErrorMessage = "O nome não pode estar em branco")]
        [MaxLength(80,ErrorMessage = "Apenas 80 caracteres permitidos!")]
        [Display(Name = "Primeiro Nome")]
        // Aqui posso usar o Remote para verificar se algum email já está existente no banco de dados para não haver repetição com isso chamo o método ValidateNome no UsuarioController para validar. 
        //[Remote("ValidateNome", "Usuario", HttpMethod = "POST", ErrorMessage = "Nome não pode estar em branco.")]
        public string Nome { get; set; }
    }
}
