using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfiPro.Domain.Enums
{
    public enum ProjectStatus
    {
        Publicado = 1,
        Asignado = 2,
        EnProceso = 3,
        PendienteConfirmacion = 4,
        Finalizado = 5,
        Cancelado = 6,
        Expirado = 7
    }
}
