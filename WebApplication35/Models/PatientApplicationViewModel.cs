namespace WebApplication35.Models
{
    public class PatientApplicationViewModel
    {
        public Patient NewPatient { get; set; }
        public Application NewApplication { get; set; }
        public Unit newUnit { get; set; }
        public Staff newStaff { get; set; }

        // Additional properties for random IDs
        public int RandomPatientID { get; set; }
        public int RandomStaffID { get; set; }
        public int RandomUnitID { get; set; }
        public int RandomApplicationID { get; set; }
    }
}
