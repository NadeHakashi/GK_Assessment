﻿using GK_Assessment.Utilities;
using OpenQA.Selenium;
using System;
using System.Data;

namespace GK_Assessment.TestClasses
{
    class ProtractorApp_TestClass
    {
        //Page Objects
        private static By UserTable = By.XPath("//table[@class=\"smart-table table table-striped\"]");
        private static By TableCellValue(string cellValue)
        {
            return By.XPath("//td[\"" + cellValue + "\"]");
        }
        private static By SearchInput = By.XPath("//input[@placeholder=\"Search\"]");
        private static By DeleteIcon = By.XPath("//i[@class=\"icon icon-remove\"]");
        private static By ConfirmationDialog_OK = By.XPath("//button[text()=\"OK\"]");
        private static By ConfirmationDialog_Cancel = By.XPath("//button[text()=\"Cancel\"]");
        private static By AddUserButton = By.XPath("//button[@class=\"btn btn-link pull-right\"]");
        private static By FirstNameInput = By.XPath("//input[@name=\"FirstName\"]");
        private static By LastNameInput = By.XPath("//input[@name=\"LastName\"]");
        private static By UserNameInput = By.XPath("//input[@name=\"UserName\"]");
        private static By PasswordInput = By.XPath("//input[@name=\"Password\"]");
        private static By CompanyAAARadioButton = By.XPath("//input[@type=\"radio\"][@value=15]");
        private static By CompanyBBBRadioButton = By.XPath("//input[@type=\"radio\"][@value=16]");
        private static By RoleDropDown(int option)
        {
            return By.XPath("//select[@name=\"RoleId\"]//option[@value=" + option + "]");
        }
        private static By EmailInput = By.XPath("//input[@name=\"Email\"]");
        private static By CellPhoneInput = By.XPath("//input[@name=\"Mobilephone\"]");
        private static By SaveButton = By.XPath("//button[@class=\"btn btn-success\"]");
        private static By CancelButton = By.XPath("//button[@class=\"btn btn-danger\"]");

        public static void NavigateToProtractorApp()
        {
            try
            {
                Selenium_Utilities.NavigateToPage("http://www.way2automation.com/angularjs-protractor/webtables/");

                Selenium_Utilities.ValidateElementExists(UserTable);
            }
            catch (Exception e)
            {
                Console.WriteLine("[ERROR]:\n" + e.Message);
                throw e;
            }
        }

        public static void AddUsers(DataTable UsersTable)
        {
            try
            {
                //Create users:
                foreach (DataRow UserRow in UsersTable.Rows)
                {
                    //Open add user menu:
                    Selenium_Utilities.ClickElement(AddUserButton);

                    //Enter user details:
                    Selenium_Utilities.EnterText(FirstNameInput, UserRow["FirstName"].ToString());
                    Selenium_Utilities.EnterText(LastNameInput, UserRow["LastName"].ToString());

                    //Generate GUID and append first 8 characters to username for a unique username:
                    Guid guid = Guid.NewGuid();
                    Selenium_Utilities.EnterText(UserNameInput, UserRow["UserName"].ToString() + "-" + guid.ToString().Substring(0, 7));
                    Selenium_Utilities.EnterText(PasswordInput, UserRow["Password"].ToString());

                    switch (UserRow["Customer"].ToString().ToLower())
                    {
                        case "company aaa":
                            Selenium_Utilities.ClickElement(CompanyAAARadioButton);
                            break;
                        case "company bbb":
                            Selenium_Utilities.ClickElement(CompanyBBBRadioButton);
                            break;
                        default:
                            throw new Exception("Failed to select customer type");
                    }

                    switch (UserRow["Role"].ToString().ToLower())
                    {
                        case "sales team":
                            Selenium_Utilities.ClickElement(RoleDropDown(0));
                            break;
                        case "customer":
                            Selenium_Utilities.ClickElement(RoleDropDown(1));
                            break;
                        case "admin":
                            Selenium_Utilities.ClickElement(RoleDropDown(2));
                            break;
                        default:
                            throw new Exception("Failed to select Role");
                    }

                    Selenium_Utilities.EnterText(EmailInput, UserRow["EMail"].ToString());
                    Selenium_Utilities.EnterText(CellPhoneInput, UserRow["CellPhone"].ToString());

                    Console.WriteLine("Entered user details.");

                    Selenium_Utilities.ClickElement(SaveButton);
                }

                Console.WriteLine("Created user account(s)");
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to create a user(s)");
                Console.WriteLine("[ERROR]:\n" + e.Message);
                throw e;
            }
        }

        public static void ValidateUsersExist(DataTable UsersTable)
        {
            try
            {
                //Validate user details:
                foreach (DataRow UserRow in UsersTable.Rows)
                {
                    //Search user by username before validation:
                    Selenium_Utilities.EnterText(SearchInput, UserRow["UserName"].ToString());

                    //Validate User:
                    Selenium_Utilities.ValidateElementExists(TableCellValue(UserRow["FirstName"].ToString()));
                    Selenium_Utilities.ValidateElementExists(TableCellValue(UserRow["LastName"].ToString()));
                    Selenium_Utilities.ValidateElementExists(TableCellValue(UserRow["UserName"].ToString()));
                    Selenium_Utilities.ValidateElementExists(TableCellValue(UserRow["Customer"].ToString()));
                    Selenium_Utilities.ValidateElementExists(TableCellValue(UserRow["Role"].ToString()));
                    Selenium_Utilities.ValidateElementExists(TableCellValue(UserRow["EMail"].ToString()));
                    Selenium_Utilities.ValidateElementExists(TableCellValue(UserRow["CellPhone"].ToString()));
                }

                Console.WriteLine("Validated all user(s) have been created.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to validate user(s) exist");
                Console.WriteLine("[ERROR]:\n" + e.Message);
                throw e;
            }
        }

        public static void DeleteUsers(DataTable UsersTable)
        {
            try
            {
                //Delete user details:
                foreach (DataRow UserRow in UsersTable.Rows)
                {
                    //Search user by username before deletion:
                    Selenium_Utilities.EnterText(SearchInput, UserRow["UserName"].ToString());

                    //Delete user:
                    Selenium_Utilities.ClickElement(DeleteIcon);
                    Selenium_Utilities.ClickElement(ConfirmationDialog_OK);
                }

                Console.WriteLine("Deleted all user(s) that has been created.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to delete user(s)");
                Console.WriteLine("[ERROR]:\n" + e.Message);
                throw e;
            }
        }
    }
}
