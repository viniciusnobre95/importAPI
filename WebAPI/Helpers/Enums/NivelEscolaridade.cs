using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ImportApi.Models
{
    public enum NivelEscolaridade
    {
        Infantil = 1,
        Fundamental = 2,
        Médio = 3,
        Superior = 4,
    }
}
