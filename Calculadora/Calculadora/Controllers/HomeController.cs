using Calculadora.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Calculadora.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {

            ViewBag.Visor = "0";
            ViewBag.PrimeiroOperador = "Sim";
            ViewBag.Operador = "";
            ViewBag.PrimeiroOperando = "";
            ViewBag.LimpaVisor = "Sim";

            return View();
        }
        [HttpPost]
            
        public IActionResult Index(string botao, 
            string visor, 
            string primeiroOperador,
            string primeiroOperando,
            string operador,
            string limpaVisor)
        {
            //avaliar o valor associado à variável 'botão'
            switch (botao)
            {
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                case "0":
                    //atribuir ao 'visor'o valor do botao
                    if (visor == "0" || limpaVisor == "Sim") visor = botao;
                    else visor = visor + botao;//visor +=botao;
                    break;

                case "+/-":
                    //faz a inversão do valor do visor
                    if (visor.StartsWith('-')) visor = visor.Substring(1);
                    else visor = "-" + visor;
                    break;

                case ",":
                    if (!visor.Contains(',')) visor += ",";
                    break;
                case "+":
                case "-":
                case "/":
                case "x":
                case "=":
                    limpaVisor = "Sim";
                    if (primeiroOperador != "Sim")
                    {
                        //esta é a 2º vez (ou mais) que se selecionou um 'operador'
                        //efetuar a operação com o operador antigo e os valores dos operandos
                        double operando1 = Convert.ToDouble(primeiroOperando);
                        double operando2 = Convert.ToDouble(visor);
                        //efetuar a operação aritmética
                        switch (operador)
                        {
                            case "+":
                                visor = operando1 + operando2 + "";
                                break;
                            case "-":
                                visor = operando1 - operando2 + "";
                                break;
                            case "/":
                                visor = operando1 / operando2 + "";
                                break;
                            case "x":
                                visor = operando1 * operando2 + "";
                                break;
                        }
                    }//fim do if(primeiroOperador != "Sim")
                        //armazebar os valores atuais para calculos futuros
                        //Visor atual, que passa a '1º operando'
                        primeiroOperando = visor;
                        //guardar o valor do operador para efetuar a operação
                        operador = botao;
                    if (botao != "=")
                    {
                        //assinalar o que se vai fazer com os operadores
                        primeiroOperador = "Nao";
                    }
                    else
                    {
                        primeiroOperador = "Sim";
                    }

            break;
                case "C":
                    //reset da calculadora

                    visor = "0";
                    primeiroOperador = "Sim";
                    operador = "";
                    primeiroOperando = "";
                    limpaVisor = "Sim";

                    break;
            }//fim do switch
            //enviar o valor do 'visor' para a view
            ViewBag.Visor = visor;
            //preciso de manter o 'estado das variaveis auxiliares
            ViewBag.PrimeiroOperador = primeiroOperador;
            ViewBag.Operador = operador;
            ViewBag.PrimeiroOperando = primeiroOperando;
            ViewBag.LimpaVisor = limpaVisor;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
