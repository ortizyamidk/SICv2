﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_LoginForm.Models
{
    public interface IDepartamentoRepository
    {
        IEnumerable<DepartamentoModel> GetDepartamentos();
        int GetAreasTerminadas();
        int GetTotalAreas();
    }
}
