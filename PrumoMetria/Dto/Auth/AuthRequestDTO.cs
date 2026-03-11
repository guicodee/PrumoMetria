namespace PrumoMetria.Dto.Auth; 

public record AuthResponseDTO(
    string AccessToken,
    string RefreshToken,
    int ExpiresIn
);