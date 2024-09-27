namespace TestProject.Dtos.ResponseDtos {
    public class GenericResponseDto {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string>? Errors { get; set; }
    }
}
