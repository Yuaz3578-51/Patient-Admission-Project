using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using WebApplication35.Models;


namespace WebApplication35.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string? errorMessage = null)
        {
            if (!string.IsNullOrEmpty(errorMessage))
            {
                ViewData["LoginError"] = errorMessage;
            }
            return View();
        }

        public IActionResult Logout()
        {
            // Perform any cleanup or session management required for logout
            // For example, you can clear any user-related session data or authentication cookies.

            // Redirect the user to the login page after logout
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult AddPatient()
        {
            // Create a new instance of the Patient model and pass it to the view
            Patient newPatient = new Patient();
            return View(newPatient);
        }

        [HttpPost]
        public IActionResult AddPatient(Patient newPatient)
        {
            try
            {
                // Create the OracleConnection using your connection string
                string connectionString = "Data Source=ORCL;User Id=YUNUS;Password=12345; Connection Timeout=60; Max Pool Size=150; data source= (DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = YUNUS-SAHBAZ.probel.local)(PORT = 1521)) (CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = ORCL)))";
                //string connectionString = "Data Source=ORCL;User Id=YUNUS;Password=12345; Connection Timeout=60; Max Pool Size=150;";
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    // Create the OracleCommand with the INSERT query
                    string insertQuery = "INSERT INTO Patient (PatientID, PatientFName, PatientLName, PatientAddress, PatientDateOfBirth, PatientNumber) " +
                                         "VALUES (:PatientID, :PatientFName, :PatientLName, :PatientAddress, :PatientDateOfBirth, :PatientNumber)";

                    using (OracleCommand cmd = new OracleCommand(insertQuery, connection))
                    {
                        // Add parameters to the command
                        cmd.Parameters.Add(new OracleParameter("PatientID", OracleDbType.Int32)).Value = newPatient.PatientID;
                        cmd.Parameters.Add(new OracleParameter("PatientFName", OracleDbType.Varchar2)).Value = newPatient.PatientFName;
                        cmd.Parameters.Add(new OracleParameter("PatientLName", OracleDbType.Varchar2)).Value = newPatient.PatientLName;
                        cmd.Parameters.Add(new OracleParameter("PatientAddress", OracleDbType.Varchar2)).Value = newPatient.PatientAddress;
                        cmd.Parameters.Add(new OracleParameter("PatientDateOfBirth", OracleDbType.Date)).Value = newPatient.PatientDateOfBirth;
                        cmd.Parameters.Add(new OracleParameter("PatientNumber", OracleDbType.Varchar2)).Value = newPatient.PatientNumber;

                        // Execute the query
                        cmd.ExecuteNonQuery();
                    }
                }

                // After successfully adding the patient, fetch the updated patient data and pass it to the Index2 view
                List<Patient> patients = new List<Patient>();
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    string selectQuery = "SELECT * FROM Patient";
                    using (OracleCommand cmd = new OracleCommand(selectQuery, connection))
                    {
                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Patient patient = new Patient
                                {
                                    PatientID = reader.GetInt32(reader.GetOrdinal("PatientID")),
                                    PatientFName = reader.GetString(reader.GetOrdinal("PatientFName")),
                                    PatientLName = reader.GetString(reader.GetOrdinal("PatientLName")),
                                    PatientAddress = reader.GetString(reader.GetOrdinal("PatientAddress")),
                                    PatientDateOfBirth = reader.GetDateTime(reader.GetOrdinal("PatientDateOfBirth")),
                                    PatientNumber = reader.GetInt32(reader.GetOrdinal("PatientNumber"))
                                };

                                patients.Add(patient);
                            }
                        }
                    }
                }

                // Pass the patient data to the view
                ViewBag.data = patients;

                _logger.LogInformation("New patient added: {PatientID}, {PatientFName}, {PatientLName}, {PatientAddress}, {PatientDateOfBirth}, {PatientNumber}", newPatient.PatientID, newPatient.PatientFName, newPatient.PatientLName, newPatient.PatientAddress, newPatient.PatientDateOfBirth, newPatient.PatientNumber);

                // Redirect to the Index2 action after successfully adding the patient
                return RedirectToAction("Index2");
            }
            catch (OracleException ex)
            {
                // Handle any exceptions related to the database connection or query
                ViewData["ErrorMessage"] = "An error occurred while adding the patient to the database.";
                _logger.LogError(ex, "Error occurred while adding the patient to the database.");
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                ViewData["ErrorMessage"] = "An unexpected error occurred while adding the patient.";
                _logger.LogError(ex, "Unexpected error occurred while adding the patient.");
            }

            // If there was an error, return to the Index2 view without adding the patient
            return RedirectToAction("Index2");
        }









        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            // Replace this with your logic to validate the username and password against your database
            // For demonstration purposes, let's assume a valid username is "admin" and password is "password"
            if (username == "YUNUS" && password == "12345")
            {
                // If the login is successful, redirect to the desired page (e.g., Index2)
                return RedirectToAction("Index2");
            }
            else
            {
                // If the login fails, display a warning message and stay on the login page
                return Index("Invalid username or password. Please try again.");
            }
        }

        public IActionResult ViewTable(string tableName)
        {
            ViewData["TableName"] = tableName;
            ViewData["TableData"] = GetTableData(tableName);

            return View();
        }

        // Modify the action methods to ensure the correct model type is passed to the views
/*
        public IActionResult Applications()
        {
            var applications = GetTableData("Application").Cast<Application>().ToList();
            ViewData["TableName"] = "Applications";
            ViewData["TableData"] = applications;

            return View("Tables");
        }

        public IActionResult DrugDefinitions()
        {
            var drugDefinitions = GetTableData("Drug_Definition").Cast<Drug_Definition>().ToList();
            ViewData["TableName"] = "DrugDefinitions";
            ViewData["TableData"] = drugDefinitions;

            return View("Tables");
        }
*/

        // Add similar methods for other tables...

        public IActionResult Index2()
        {
            string oradb = "Data Source=ORCL;User Id=YUNUS;Password=12345; Connection Timeout=60; Max Pool Size=150; data source= (DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = YUNUS-SAHBAZ.probel.local)(PORT = 1521)) (CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = ORCL)))";
            List<Application> applications = new List<Application>();
            List<Patient> patients = new List<Patient>();

            try
            {
                using (OracleConnection conn = new OracleConnection(oradb))
                {
                    conn.Open();
                    // Fetch data from the Application table
                    using (OracleCommand cmd = new OracleCommand("SELECT * FROM APPLICATION", conn))
                    {
                        using (OracleDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                // Create an Application object and populate its properties with the retrieved data
                                Application application = new Application
                                {
                                    AppID = dr.GetInt32(dr.GetOrdinal("APPID")),
                                    PatientID = dr.GetInt32(dr.GetOrdinal("PATIENTID")),
                                    UnitID = dr.GetInt32(dr.GetOrdinal("UNITID")),
                                    StaffID = dr.GetInt32(dr.GetOrdinal("STAFFID")),
                                    Record_Date = dr.GetDateTime(dr.GetOrdinal("RECORD_DATE")),
                                    Diagnosis_ID = dr.GetInt32(dr.GetOrdinal("DIAGNOSIS_ID")),
                                    Diagnosis_Date = dr.GetDateTime(dr.GetOrdinal("DIAGNOSIS_DATE")),
                                };

                                applications.Add(application);
                            }
                        }
                    }

                    // Fetch data from the Patient table
                    using (OracleCommand cmd = new OracleCommand("SELECT * FROM Patient", conn))
                    {
                        using (OracleDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                // Create a Patient object and populate its properties with the retrieved data
                                Patient patient = new Patient
                                {
                                    PatientID = dr.GetInt32(dr.GetOrdinal("PatientID")),
                                    PatientFName = dr.GetString(dr.GetOrdinal("PatientFName")),
                                    PatientLName = dr.GetString(dr.GetOrdinal("PatientLName")),
                                    PatientAddress = dr.GetString(dr.GetOrdinal("PatientAddress")),
                                    PatientDateOfBirth = dr.GetDateTime(dr.GetOrdinal("PatientDateOfBirth")),
                                    PatientNumber = dr.GetInt32(dr.GetOrdinal("PatientNumber")),
                                    PatientPhone = dr.GetString(dr.GetOrdinal("PatientPhone"))
                                };

                                patients.Add(patient);
                            }
                        }
                    }
                }

                // Pass the data to the view using ViewBag
                ViewBag.ApplicationsData = applications;
                ViewBag.PatientsData = patients;
            }
            catch (OracleException ex)
            {
                // Handle any exceptions related to the database connection or query
                ViewData["ErrorMessage"] = "An error occurred while fetching data from the database.";
                _logger.LogError(ex, "Error occurred while fetching data from the database.");
            }

            return View();
        }



        // Define the GetSafeDateTime method to handle DBNull.Value
        private DateTime GetSafeDateTime(IDataReader reader, string columnName)
        {
            int columnIndex = reader.GetOrdinal(columnName);
            return reader.IsDBNull(columnIndex) ? DateTime.MinValue : reader.GetDateTime(columnIndex);
        }


        [HttpGet]
        public IActionResult EditPatient(int patientID)
        {
            // Fetch the patient data based on the provided patientID
            string connectionString = "Data Source=ORCL;User Id=YUNUS;Password=12345; Connection Timeout=60; Max Pool Size=150; data source= (DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = YUNUS-SAHBAZ.probel.local)(PORT = 1521)) (CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = ORCL)))"; // Replace with your actual connection string
            Patient? patient = null;
            Application? application = null;

            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    string selectPatientQuery = "SELECT * FROM Patient WHERE PatientID = :PatientID";
                    using (OracleCommand cmd = new OracleCommand(selectPatientQuery, connection))
                    {
                        cmd.Parameters.Add(new OracleParameter("PatientID", OracleDbType.Int32)).Value = patientID;

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Populate the patient object with the retrieved data
                                patient = new Patient
                                {
                                    PatientID = reader.GetInt32(reader.GetOrdinal("PatientID")),
                                    PatientFName = reader.GetString(reader.GetOrdinal("PatientFName")),
                                    PatientLName = reader.GetString(reader.GetOrdinal("PatientLName")),
                                    PatientAddress = reader.GetString(reader.GetOrdinal("PatientAddress")),
                                    PatientDateOfBirth = GetSafeDateTime(reader, "PatientDateOfBirth"),
                                    PatientNumber = reader.GetInt32(reader.GetOrdinal("PatientNumber")),
                                    PatientPhone = reader.GetString(reader.GetOrdinal("PatientPhone"))
                                };
                            }
                        }
                    }

                    // Fetch the application data based on the provided patientID
                    string selectApplicationQuery = "SELECT * FROM Application WHERE PatientID = :PatientID";
                    using (OracleCommand cmd = new OracleCommand(selectApplicationQuery, connection))
                    {
                        cmd.Parameters.Add(new OracleParameter("PatientID", OracleDbType.Int32)).Value = patientID;

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Populate the application object with the retrieved data
                                application = new Application
                                {
                                    AppID = reader.GetInt32(reader.GetOrdinal("AppID")),
                                    UnitID = reader.GetInt32(reader.GetOrdinal("UnitID")),
                                    StaffID = reader.GetInt32(reader.GetOrdinal("StaffID")),
                                    Record_Date = GetSafeDateTime(reader, "Record_Date"),
                                    Diagnosis_ID = reader.GetInt32(reader.GetOrdinal("Diagnosis_ID")),
                                    Diagnosis_Date = GetSafeDateTime(reader, "Diagnosis_Date")
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                _logger.LogError(ex, "Error occurred while fetching patient data for editing.");
                ViewData["ErrorMessage"] = "An error occurred while fetching patient data for editing.";
            }

            // If patient is null, it means the patient with the provided ID does not exist in the database
            if (patient == null)
            {
                ViewData["ErrorMessage"] = "Patient not found.";
                return RedirectToAction("Index2");
            }

            // Create a tuple of the Patient and Application objects
            var model = (patient, application);

            return View(model);
        }


        [HttpPost]
        public IActionResult SavePatientChanges(Patient editedPatient, Application editedApplication)
        {
            try
            {
                // Check for model validation errors
                if (!ModelState.IsValid)
                {
                    // If there are validation errors, return to the EditPatient view
                    return View("EditPatient", (editedPatient, editedApplication));
                }

                // Update the patient data and application data in the database
                string connectionString = "Data Source=ORCL;User Id=YUNUS;Password=12345; Connection Timeout=60; Max Pool Size=150; data source= (DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = YUNUS-SAHBAZ.probel.local)(PORT = 1521)) (CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = ORCL)))"; // Replace with your actual connection string

                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    // Update the patient data in the database based on the editedPatient object
                    string updatePatientQuery = "UPDATE Patient SET PatientFName = :PatientFName, PatientLName = :PatientLName, " +
                                                "PatientAddress = :PatientAddress, PatientDateOfBirth = :PatientDateOfBirth, " +
                                                "PatientNumber = :PatientNumber, PatientPhone = :PatientPhone " +
                                                "WHERE PatientID = :PatientID";

                    using (OracleCommand cmd = new OracleCommand(updatePatientQuery, connection))
                    {
                        cmd.Parameters.Add(new OracleParameter("PatientID", OracleDbType.Int32)).Value = editedPatient.PatientID;
                        cmd.Parameters.Add(new OracleParameter("PatientFName", OracleDbType.Varchar2)).Value = editedPatient.PatientFName;
                        cmd.Parameters.Add(new OracleParameter("PatientLName", OracleDbType.Varchar2)).Value = editedPatient.PatientLName;
                        cmd.Parameters.Add(new OracleParameter("PatientAddress", OracleDbType.Varchar2)).Value = editedPatient.PatientAddress;
                        cmd.Parameters.Add(new OracleParameter("PatientDateOfBirth", OracleDbType.Date)).Value = editedPatient.PatientDateOfBirth;
                        cmd.Parameters.Add(new OracleParameter("PatientNumber", OracleDbType.Varchar2)).Value = editedPatient.PatientNumber;
                        cmd.Parameters.Add(new OracleParameter("PatientPhone", OracleDbType.Varchar2)).Value = editedPatient.PatientPhone;

                        cmd.ExecuteNonQuery();
                    }

                    // Update the application data in the database based on the editedApplication object
                    string updateApplicationQuery = "UPDATE Application SET " +
                                                    "UnitID = :UnitID, StaffID = :StaffID, " +
                                                    "Record_Date = :Record_Date, Diagnosis_ID = :Diagnosis_ID, " +
                                                    "Diagnosis_Date = :Diagnosis_Date " +
                                                    "WHERE AppID = :AppID";

                    using (OracleCommand cmd = new OracleCommand(updateApplicationQuery, connection))
                    {
                        cmd.Parameters.Add(new OracleParameter("AppID", OracleDbType.Int32)).Value = editedApplication.AppID;
                        cmd.Parameters.Add(new OracleParameter("UnitID", OracleDbType.Int32)).Value = editedApplication.UnitID;
                        cmd.Parameters.Add(new OracleParameter("StaffID", OracleDbType.Int32)).Value = editedApplication.StaffID;
                        cmd.Parameters.Add(new OracleParameter("Record_Date", OracleDbType.Date)).Value = editedApplication.Record_Date;
                        cmd.Parameters.Add(new OracleParameter("Diagnosis_ID", OracleDbType.Int32)).Value = editedApplication.Diagnosis_ID;
                        cmd.Parameters.Add(new OracleParameter("Diagnosis_Date", OracleDbType.Date)).Value = editedApplication.Diagnosis_Date;

                        cmd.ExecuteNonQuery();
                    }
                }

                _logger.LogInformation("Patient data updated: {PatientID}, {PatientFName}, {PatientLName}, {PatientAddress}, {PatientDateOfBirth}, {PatientNumber}",
                    editedPatient.PatientID, editedPatient.PatientFName, editedPatient.PatientLName, editedPatient.PatientAddress, editedPatient.PatientDateOfBirth, editedPatient.PatientNumber);

                _logger.LogInformation("Application data updated: {AppID}, {UnitID}, {StaffID}, {Record_Date}, {Diagnosis_ID}, {Diagnosis_Date}",
                    editedApplication.AppID, editedApplication.UnitID, editedApplication.StaffID, editedApplication.Record_Date, editedApplication.Diagnosis_ID, editedApplication.Diagnosis_Date);

                // Redirect to the Index2 action after successfully updating the patient and application information
                return RedirectToAction("Index2");
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                _logger.LogError(ex, "Error occurred while saving patient changes.");
                ViewData["ErrorMessage"] = "An error occurred while saving patient changes.";
            }

            // If there was an error, return to the EditPatient view to allow the user to try again
            return View("EditPatient", (editedPatient, editedApplication));
        }















        // Helper method to get table data (you can use this in other actions)
        private List<object> GetTableData(string tableName)
        {
            // Replace this with your logic to fetch data from the database for the specified table
            // For demonstration purposes, return an empty list
            return new List<object>();
        }

        // Add other action methods...

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}