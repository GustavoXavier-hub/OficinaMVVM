using System;
using System.Collections.Generic;
using System.Text;

namespace OficinaMVVM.Models
{
    public class AtendimentoItem
    {
        public long? AtendimentoItemID { get; set; }
        public long? AtendimentoID { get; set; }
        public long? ServicoID { get; set; }
        public int Quantidade { get; set; }
        public double Valor { get; set; }
        public double SubTotal
        {
            get
            {
                return Quantidade * Valor;
            }
        }

        public Atendimento Atendimento { get; set; }        
        public Servico Servico { get; set; }



    }

}
