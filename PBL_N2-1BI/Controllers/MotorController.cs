﻿using Microsoft.AspNetCore.Mvc;
using PBL_N2_1BI.DAO;
using PBL_N2_1BI.Models;
using System.Collections.Generic;
using System;
using PBL_N2_1BI.Filters;
using Microsoft.AspNetCore.Http;

namespace PBL_N2_1BI.Controllers
{
    public class MotorController : Controller
    {
        [SessionAuthorize]
        public IActionResult Consulta(MotorViewModel motorConsulta)
        {
            ViewBag.MensagemErro = HttpContext.Session.GetString("MensagemErro");
            HttpContext.Session.Remove("MensagemErro");

            List<MotorViewModel> listaMotores = new List<MotorViewModel>();
            listaMotores = new MotorDAO().ListarMotores(motorConsulta);

            ViewBag.Filtros = motorConsulta;

            return View(listaMotores);
        }

        [SessionAuthorize]
        public IActionResult Adicionar()
        {
            MotorViewModel motorNovo = new MotorViewModel();
            return View("Cadastro", motorNovo);
        }

        [SessionAuthorize]
        public IActionResult Editar(int idMotor)
        {
            MotorViewModel motorNovo = new MotorViewModel();
            motorNovo = new MotorDAO().PesquisarPorId(idMotor);

            return View("Cadastro", motorNovo);
        }

        public IActionResult Salvar(MotorViewModel motor)
        {
            MotorDAO dao = new MotorDAO();
            try
            {
                if (!motor.Id.HasValue)
                {
                    dao.Inserir(motor);
                    TempData["Mensagem"] = "Motor salvo com sucesso!";
                }
                else
                {
                    dao.Alterar(motor);
                    TempData["Mensagem"] = "Motor alterado com sucesso!";
                }
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel(ex.ToString()));
            }
            return RedirectToAction("Consulta");
        }

        [SessionAuthorize]
        public IActionResult Excluir(int Id)
        {
            try
            {
                new MotorDAO().Excluir(Id);
                TempData["Mensagem"] = "Motor excluído com sucesso!";
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel(ex.ToString()));
            }
            return RedirectToAction("Consulta");
        }
    }
}
