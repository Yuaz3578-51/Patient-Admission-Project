using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using WebApplication35.Models;
using System.Security.Cryptography;
using Microsoft.VisualBasic;
using System.Globalization;
using System.Transactions;

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
            // Perform any cleanup or session management required for logout. For example, you can clear any user-related session data or authentication cookies.
            // Redirect the user to the login page after logout
            return RedirectToAction("Index");
        }



        public IActionResult AddPatient()
        {
            ViewBag.RandomPatientID = new Random().Next(2, 99);
            ViewBag.RandomStaffID = new Random().Next(2, 99);
            ViewBag.RandomUnitID = new Random().Next(2, 99);
            ViewBag.RandomApplicationID = new Random().Next(2, 99);

            var viewModel = new PatientApplicationViewModel
            {
                NewPatient = new Patient(),
                NewApplication = new Application(),
                newUnit = new Unit()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddPatient(Patient addedPatient)
        {
            try
            {
                string connectionString = "Data Source=ORCL;User Id=YUNUS;Password=12345; Connection Timeout=60; Max Pool Size=150; data source= (DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = YUNUS-SAHBAZ.probel.local)(PORT = 1521)) (CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = ORCL)))";

                CultureInfo turkishCulture = new CultureInfo("tr-TR");
                string formattedDob = null;

                // ... (existing code for date formatting) ...

                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    // Insert the patient data into the PATIENT table
                    string addPatientData = "INSERT INTO PATIENT (PATIENTID, PATIENTFNAME, PATIENTLNAME, PATIENTADDRESS, PATIENTNUMBER, PATIENTPHONE, PATIENTDATEOFBIRTH, RECORD_DATE, DIAGNOSIS_DATE, UNITID, STAFFID, APPID) VALUES (:PATIENTID, :PATIENTFNAME, :PATIENTLNAME, :PATIENTADDRESS, :PATIENTNUMBER, :PATIENTPHONE, :PATIENTDATEOFBIRTH, :RECORD_DATE, :DIAGNOSIS_DATE, :UNITID, :STAFFID, :APPID)";


                    /*
                    using (OracleCommand patientAddCmd = new OracleCommand(addPatientData, connection))
                    {
                        patientAddCmd.Parameters.Add(":PATIENTID", OracleDbType.Int32).Value = addedPatient.PATIENTID;
                        patientAddCmd.Parameters.Add(":PATIENTFNAME", OracleDbType.Varchar2).Value = addedPatient.PATIENTFNAME;
                        patientAddCmd.Parameters.Add(":PATIENTLNAME", OracleDbType.Varchar2).Value = addedPatient.PATIENTLNAME;
                        patientAddCmd.Parameters.Add(":PATIENTADDRESS", OracleDbType.Varchar2).Value = addedPatient.PATIENTADDRESS;
                        patientAddCmd.Parameters.Add(":PATIENTNUMBER", OracleDbType.Int32).Value = addedPatient.PATIENTNUMBER;
                        patientAddCmd.Parameters.Add(":PATIENTPHONE", OracleDbType.Varchar2).Value = addedPatient.PATIENTPHONE;
                        patientAddCmd.Parameters.Add(":PATIENTDATEOFBIRTH", OracleDbType.Date).Value = addedPatient.PATIENTDATEOFBIRTH;
                        patientAddCmd.Parameters.Add(":RECORD_DATE", OracleDbType.Date).Value = addedPatient.RECORD_DATE;
                        patientAddCmd.Parameters.Add(":DIAGNOSIS_DATE", OracleDbType.Date).Value = addedPatient.DIAGNOSIS_DATE;
                        patientAddCmd.Parameters.Add(":UNITID", OracleDbType.Int32).Value = addedPatient.UNITID;
                        patientAddCmd.Parameters.Add(":STAFFID", OracleDbType.Int32).Value = addedPatient.STAFFID;
                        patientAddCmd.Parameters.Add(":APPID", OracleDbType.Int32).Value = addedPatient.APPID;

                        patientAddCmd.ExecuteNonQuery();
                    }
                }
                */

                    /*
                    OracleConnection connection = new OracleConnection(connectionString);
                    connection.Open();
                    OracleCommand cmd = new OracleCommand(combinedUpdateData, connection);
                    cmd.CommandText = combinedUpdateData;
                    cmd.Connection = connection;
                    cmd.ExecuteNonQuery();
                    */

                    using (OracleConnection connect = new OracleConnection(connectionString))
                    {

                        // Execute the patient add command
                        using (OracleCommand patientAddCmd = new OracleCommand(addPatientData, connection))
                        {
                            patientAddCmd.ExecuteNonQuery();
                        }

                    }



                    // Redirect to the Index2 action after successfully adding the patient information
                    return RedirectToAction("Index2");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                _logger.LogError(ex, "Error occurred while adding a new patient.");
                ViewData["ErrorMessage"] = "An error occurred while adding a new patient.";
            }

            // If there was an error, return to the AddPatient view to allow the user to try again
            return RedirectToAction("AddPatient");
        }












        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            // Replace this with your logic to validate the username and password against your database
            if (username == "YUNUS" && password == "12345")
            {
                // If the login is successful, redirect to the desired page (e.g., Index2)
                return RedirectToAction("Index2");
            }
            /*
            else if (username == "asd" && password == "12345") {
                return RedirectToAction("Index2");

            }
            */
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
                                    APPID = dr.GetInt32(dr.GetOrdinal("APPID")),
                                    PATIENTID = dr.GetInt32(dr.GetOrdinal("PATIENTID")),
                                    UNITID = dr.GetInt32(dr.GetOrdinal("UNITID")),
                                    STAFFID = dr.GetInt32(dr.GetOrdinal("STAFFID")),
                                    DIAGNOSIS_ID = dr.GetInt32(dr.GetOrdinal("DIAGNOSIS_ID")),
                                };

                                applications.Add(application);
                            }
                        }
                    }

                    // Fetch data from the Patient table
                    using (OracleCommand cmd = new OracleCommand("SELECT * FROM PATIENT", conn))
                    {
                        using (OracleDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                // Create a Patient object and populate its properties with the retrieved data
                                Patient patient = new Patient
                                {
                                    PATIENTID = dr.GetInt32(dr.GetOrdinal("PATIENTID")),
                                    PATIENTFNAME = dr.GetString(dr.GetOrdinal("PATIENTFNAME")),
                                    PATIENTLNAME = dr.GetString(dr.GetOrdinal("PATIENTLNAME")),
                                    PATIENTADDRESS = dr.GetString(dr.GetOrdinal("PATIENTADDRESS")),
                                    PATIENTDATEOFBIRTH = dr.GetDateTime(dr.GetOrdinal("PATIENTDATEOFBIRTH")),
                                    PATIENTNUMBER = dr.GetInt32(dr.GetOrdinal("PATIENTNUMBER")),
                                    PATIENTPHONE = dr.GetString(dr.GetOrdinal("PATIENTPHONE")),
                                    RECORD_DATE = dr.GetDateTime(dr.GetOrdinal("RECORD_DATE")),
                                    DIAGNOSIS_DATE = dr.GetDateTime(dr.GetOrdinal("DIAGNOSIS_DATE"))
                                };

                                patients.Add(patient);
                            }
                        }
                    }
                }

                // Generate random IDs and store them in ViewBag
                ViewBag.RandomPatientID = new Random().Next(2, 100);
                ViewBag.RandomStaffID = new Random().Next(2, 100);
                ViewBag.RandomUnitID = new Random().Next(2, 100);
                ViewBag.RandomApplicationID = new Random().Next(2, 100);

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
        public IActionResult EditPatient(int PATIENTID)
        {
            // Fetch the patient data based on the provided PATIENTID
            string connectionString = "Data Source=ORCL;User Id=YUNUS;Password=12345; Connection Timeout=60; Max Pool Size=150; data source= (DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = YUNUS-SAHBAZ.probel.local)(PORT = 1521)) (CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = ORCL)))";

            Patient patient = null;
            Application application = null;

            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    string selectPatientQuery = "SELECT * FROM Patient WHERE PATIENTID = :PATIENTID";
                    using (OracleCommand cmd = new OracleCommand(selectPatientQuery, connection))
                    {
                        cmd.Parameters.Add(new OracleParameter("PATIENTID", OracleDbType.Int32)).Value = PATIENTID;

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Populate the patient object with the retrieved data
                                patient = new Patient
                                {
                                    PATIENTID = reader.GetInt32(reader.GetOrdinal("PATIENTID")),
                                    PATIENTFNAME = reader.GetString(reader.GetOrdinal("PATIENTFNAME")),
                                    PATIENTLNAME = reader.GetString(reader.GetOrdinal("PATIENTLNAME")),
                                    PATIENTADDRESS = reader.GetString(reader.GetOrdinal("PATIENTADDRESS")),
                                    PATIENTDATEOFBIRTH = reader.GetDateTime(reader.GetOrdinal("PATIENTDATEOFBIRTH")),
                                    PATIENTNUMBER = reader.GetInt32(reader.GetOrdinal("PATIENTNUMBER")),
                                    PATIENTPHONE = reader.GetString(reader.GetOrdinal("PATIENTPHONE")),
                                    RECORD_DATE = reader.GetDateTime(reader.GetOrdinal("RECORD_DATE")),
                                    DIAGNOSIS_DATE = reader.GetDateTime(reader.GetOrdinal("DIAGNOSIS_DATE"))
                                };
                            }
                        }
                    }
                    // Fetch the application data based on the provided PATIENTID
                    string selectApplicationQuery = "SELECT * FROM Application WHERE PATIENTID = :PATIENTID";
                    using (OracleCommand cmd = new OracleCommand(selectApplicationQuery, connection))
                    {
                        cmd.Parameters.Add(new OracleParameter("PATIENTID", OracleDbType.Int32)).Value = PATIENTID;

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Populate the application object with the retrieved data
                                application = new Application
                                {
                                    APPID = reader.GetInt32(reader.GetOrdinal("APPID")),
                                    PATIENTID = reader.GetInt32(reader.GetOrdinal("PATIENTID")), // Add this line to set the PATIENTID property
                                    UNITID = reader.GetInt32(reader.GetOrdinal("UNITID")),
                                    STAFFID = reader.GetInt32(reader.GetOrdinal("STAFFID")),
                                    DIAGNOSIS_ID = reader.GetInt32(reader.GetOrdinal("DIAGNOSIS_ID"))
                                    
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

            // Create a tuple of the Patient and Application objects
            var model = new Tuple<Patient, Application>(patient, application);

            // If patient is null, it means the patient with the provided ID does not exist in the database
            if (patient == null)
            {
                ViewData["ErrorMessage"] = "Patient not found.";
                return RedirectToAction("Index2");
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult SavePatientChanges(Patient editedPatient, Application editedApplication)
        {
            //Console.WriteLine(editedPatient.PATIENTID);
            try
            {
                string connectionString = "Data Source=ORCL;User Id=YUNUS;Password=12345; Connection Timeout=60; Max Pool Size=150; data source= (DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = YUNUS-SAHBAZ.probel.local)(PORT = 1521)) (CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = ORCL)))";
                
                CultureInfo turkishCulture = new CultureInfo("tr-TR");
                string dateStr = editedPatient.PATIENTDATEOFBIRTH?.ToString("dd-MMM-yyyy", turkishCulture); //setting the format to Turkish
                DateTime parsedDate = DateTime.ParseExact(dateStr, "dd-MMM-yyyy", turkishCulture);
                string formattedDate = parsedDate.ToString("dd-MMM-yyyy");

                string recDate = editedPatient.RECORD_DATE?.ToString("dd-MMM-yyyy", turkishCulture);
                DateTime parsedRecDate = DateTime.ParseExact(recDate, "dd-MMM-yyyy", turkishCulture);
                string formattedRecDate = parsedRecDate.ToString("dd-MMM-yyyy");

                string diagDate = editedPatient.DIAGNOSIS_DATE?.ToString("dd-MMM-yyyy", turkishCulture);
                DateTime parsedDiagDate = DateTime.ParseExact(diagDate, "dd-MMM-yyyy", turkishCulture);
                string formattedDiagDate = parsedDiagDate.ToString("dd-MMM-yyyy");

                string updatePatientData = "UPDATE PATIENT SET PATIENTFNAME='" + editedPatient.PATIENTFNAME + "', PATIENTLNAME='" + editedPatient.PATIENTLNAME + "', PATIENTADDRESS='" + editedPatient.PATIENTADDRESS + "', PATIENTNUMBER='" + editedPatient.PATIENTNUMBER + "', PATIENTPHONE='" + editedPatient.PATIENTPHONE + "', PATIENTDATEOFBIRTH=TO_DATE('" + formattedDate + "', 'DD-MON-YYYY'), RECORD_DATE=TO_DATE('" + formattedRecDate + "', 'DD-MON-YYYY'), DIAGNOSIS_DATE=TO_DATE('" + formattedDiagDate + "', 'DD-MON-YYYY') WHERE PATIENTID=" + editedPatient.PATIENTID;
                string updateApplicationData = "UPDATE APPLICATION SET APPID=" + editedApplication.APPID + ", DIAGNOSIS_ID=" + editedApplication.DIAGNOSIS_ID + " WHERE PATIENTID=" + editedPatient.PATIENTID;
                
                // string combinedUpdateData = updatePatientData + "; " + updateApplicationData;

                /*
                OracleConnection connection = new OracleConnection(connectionString);
                connection.Open();
                OracleCommand cmd = new OracleCommand(combinedUpdateData, connection);
                cmd.CommandText = combinedUpdateData;
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
                */
                
                
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    // Execute the patient update command
                    using (OracleCommand patientCmd = new OracleCommand(updatePatientData, connection))
                    {
                        patientCmd.ExecuteNonQuery();
                    }

                    // Execute the application update command
                    using (OracleCommand applicationCmd = new OracleCommand(updateApplicationData, connection))
                    {
                        applicationCmd.ExecuteNonQuery();
                    }
                }

                editedPatient.PATIENTFNAME = "";
                editedPatient.PATIENTLNAME = "";
                editedPatient.PATIENTADDRESS = "";
                editedPatient.PATIENTNUMBER = 0;
                editedPatient.PATIENTPHONE = "";
                editedPatient.PATIENTDATEOFBIRTH = DateTime.MinValue;
                editedPatient.RECORD_DATE = DateTime.MinValue;
                editedPatient.DIAGNOSIS_DATE = DateTime.MinValue;
                editedApplication.APPID = 0;
                editedApplication.DIAGNOSIS_ID = 0;

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
            return RedirectToAction("Index2");
        }




        [HttpPost]
        public IActionResult DeletePatient(int PATIENTID)
        {
            try
            {
                // Create the OracleConnection using your connection string
                string connectionString = "Data Source=ORCL;User Id=YUNUS;Password=12345; Connection Timeout=60; Max Pool Size=150; data source= (DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = YUNUS-SAHBAZ.probel.local)(PORT = 1521)) (CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = ORCL)))";

                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    // Check if the patient exists in the database before deleting
                    string checkPatientQuery = "SELECT COUNT(*) FROM Patient WHERE PATIENTID = :PATIENTID";
                    using (OracleCommand checkCmd = new OracleCommand(checkPatientQuery, connection))
                    {
                        checkCmd.Parameters.Add(new OracleParameter("PATIENTID", OracleDbType.Int32)).Value = PATIENTID;

                        int patientCount = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (patientCount == 0)
                        {
                            // Patient with the specified PATIENTID does not exist in the database
                            ViewData["ErrorMessage"] = "Patient not found.";
                            return RedirectToAction("Index2");
                        }
                    }
                    // Delete the patient from the database based on the provided PATIENTID
                    string deletePatientQuery = "DELETE FROM Patient WHERE PATIENTID = :PATIENTID";
                    using (OracleCommand deleteCmd = new OracleCommand(deletePatientQuery, connection))
                    {
                        deleteCmd.Parameters.Add(new OracleParameter("PATIENTID", OracleDbType.Int32)).Value = PATIENTID;
                        deleteCmd.ExecuteNonQuery();
                    }
                    // Redirect to the Index2 action after successfully deleting the patient
                    return RedirectToAction("Index2");
                }
            }
            catch (OracleException ex)
            {
                // Handle any exceptions related to the database connection or query
                ViewData["ErrorMessage"] = "An error occurred while deleting the patient from the database.";
                _logger.LogError(ex, "Error occurred while deleting the patient from the database.");
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                ViewData["ErrorMessage"] = "An unexpected error occurred while deleting the patient.";
                _logger.LogError(ex, "Unexpected error occurred while deleting the patient.");
            }
            // If there was an error, return to the Index2 view without deleting the patient
            return RedirectToAction("Index2");
        }

        // Helper method to get table data (you can use this in other actions)
        private List<object> GetTableData(string tableName)
        {
            // Replace this with your logic to fetch data from the database for the specified table. For demonstration purposes, return an empty list
            return new List<object>();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
