namespace parkingMaster.DTOs
{
    
    public class SecurityQuestionDTO
    {
        public string Question { get; set; }
        public string Answer { get; set; }

        // Constructors
        public SecurityQuestionDTO() { }

       public SecurityQuestionDto(string question)
        {
            Question = question;
        }
    }
}
