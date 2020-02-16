using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProAgil.WebAPI.Dtos
{
    public class EventoDto
    {
        public int Id { get; set; }
        public string Local { get; set; }
        public string DataEvento { get; set; }
        [Required(ErrorMessage = "O Tema é Obrigatório")]
        public string Tema { get; set; }
        [Range(2, 120000,ErrorMessage = "Quantidade de pessoas deve ser entre 2 e 12000")]
        public int QtdPessoas { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Telefone { get; set; }
        public ICollection<LoteDto> Lotes { get; set; }   
        public ICollection<RedeSocialDto> RedesSociais { get; set; }
        public string ImagemUrl { get; set; }
        public ICollection<PalestranteDto> Palestrantes { get; set; }
    }
}