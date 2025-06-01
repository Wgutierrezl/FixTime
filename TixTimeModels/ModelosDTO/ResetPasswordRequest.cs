using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TixTimeModels.ModelosDTO
{
    public class ResetPasswordRequest
    {
        //Esta es una propiedad que aun no estoy seguro de implementar, ya que no veo la necesidad de mandarle un token
        public string Token { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
    }
}
