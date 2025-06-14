﻿using TixTimeModels.Modelos;

namespace FixTimeBack.Custom
{
    public interface ITokenService
    {
        string GenerateJWT(Usuario model);
        string GenerateJWtByNewPassword(Usuario model);
    }
}
