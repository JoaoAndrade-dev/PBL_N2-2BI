﻿using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;

namespace PBL_N2_1BI.Models
{
    public class SimulacaoViewModel : PadraoViewModel
    {
        [StringLength(50, ErrorMessage = "O Nome deve ter no máximo 100 caracteres.")]
        [Required(ErrorMessage = "O Nome é um campo obrigatório!")]
        public string Nome { get; set; }
        public DateTime? DataCriacaoAlteracao { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public MotorViewModel Motor { get; set; }
        public int? IdMotor { get; set; }
        public UsuarioViewModel Usuario { get; set; }
        public int? IdUsuario { get; set; }
        public decimal? Media { get; set; }
        public decimal? Min { get; set; }
        public decimal? Max { get; set; }
    }
}
