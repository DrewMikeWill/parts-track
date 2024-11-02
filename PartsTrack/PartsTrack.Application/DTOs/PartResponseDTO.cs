namespace PartsTrack.Application.DTOs
{
    public class PartResponseDTO
    {
        public bool Success { get; set; }
        public PartDTO Part { get; set; }
        public string Message { get; set; }

        public PartResponseDTO(bool success, PartDTO part, string message)
        {
            Success = success;
            Part = part;
            Message = message;
        }
    }
}
