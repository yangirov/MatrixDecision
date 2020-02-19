namespace MatrixDecision
{
    public class Patient
    {
        public string Guid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int SnilsMissingReason { get; set; }
        public int Age { get; set; }
        public int SocialStatus { get; set; }

        public int PatientIdentity { get; set; }
        public int InformationNewborn { get; set; }
        public int ShiftWorker { get; set; }
        public int Nationality { get; set; }
        public int MaritalStatus { get; set; }
        public int CitizenShip { get; set; }
        public int InvGroup { get; set; }
        public int Workplace { get; set; }
    }
}
